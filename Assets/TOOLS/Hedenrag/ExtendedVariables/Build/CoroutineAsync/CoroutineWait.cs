using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CoroutineWait
{
    static ulong LastID = 0;

    static Dictionary<GameObject, Dictionary<MonoBehaviour, List<Coroutine>>> dictionaryForGameObjects = new Dictionary<GameObject, Dictionary<MonoBehaviour, List<Coroutine>>>();
    static Dictionary<ulong,Coroutine> coroutineId = new Dictionary<ulong,Coroutine>();
    static Dictionary<Coroutine, ulong> idCoroutine = new Dictionary<Coroutine, ulong>();

    public static Coroutine DoAfterSeconds(MonoBehaviour monoBehaviour, Action action, float time)
    {
        return BeginCorroutine(monoBehaviour, action, time);
    }

    public static Coroutine DoAfterSeconds(Action action, float time)
    {
        return BeginCorroutine(CoroutineHolder.Instance, action, time);
    }

    static Coroutine BeginCorroutine(MonoBehaviour monoBehaviour, Action action, float time)
    {
        LastID++;
        Coroutine coroutine = monoBehaviour.StartCoroutine(WaitAsyncSecs(monoBehaviour, action, time, LastID));

        GameObject g = monoBehaviour.gameObject;
        dictionaryForGameObjects.TryAdd(g, new Dictionary<MonoBehaviour, List<Coroutine>>());
        if (!dictionaryForGameObjects[g].ContainsKey(monoBehaviour))
        {
            dictionaryForGameObjects[g].Add(monoBehaviour, new List<Coroutine>());
        }
        dictionaryForGameObjects[g][monoBehaviour].Add(coroutine);

        coroutineId.Add(LastID, coroutine);
        idCoroutine.Add(coroutine, LastID);

        return coroutine;
    }
    
    static IEnumerator WaitAsyncSecs(MonoBehaviour monoBehaviour, Action action, float time, ulong Id)
    {
        yield return new WaitForSeconds(time);

        action();
        RemoveCoroutine(monoBehaviour, Id);
    }

    private static void RemoveCoroutine(MonoBehaviour monoBehaviour, ulong Id)
    {
        Coroutine removal = coroutineId[Id];

        dictionaryForGameObjects[monoBehaviour.gameObject][monoBehaviour].Remove(removal);
        if (dictionaryForGameObjects[monoBehaviour.gameObject][monoBehaviour].Count <= 0) 
        { 
            dictionaryForGameObjects[monoBehaviour.gameObject].Remove(monoBehaviour);
        }
        if (dictionaryForGameObjects[monoBehaviour.gameObject].Count <= 0) { dictionaryForGameObjects.Remove(monoBehaviour.gameObject); }

        idCoroutine.Remove(removal);
        coroutineId.Remove(Id);

    }

    static void CancelCoroutine(MonoBehaviour caller, Coroutine coroutine)
    {
        RemoveCoroutine(caller, idCoroutine[coroutine]);
        caller.StopCoroutine(coroutine);
    }

    public static void CancelCoroutine(CoroutineFunction coroutineFunction)
    {
        coroutineFunction.CancelCoroutine();
    }

    public static void CancelCoroutine(GameObject gameObject)
    {
        foreach(List<Coroutine> coroutines in dictionaryForGameObjects[gameObject].Values)
        {
            foreach(Coroutine coroutine in coroutines)
            {
                coroutineId.Remove(idCoroutine[coroutine]);
                idCoroutine.Remove(coroutine);
            }
        }
        foreach(MonoBehaviour behavour in dictionaryForGameObjects[gameObject].Keys)
        {
            behavour.StopAllCoroutines();
        }
        dictionaryForGameObjects.Remove(gameObject);
    }

    public static void CancelCoroutine(MonoBehaviour caller)
    {
        foreach(Coroutine coroutine in dictionaryForGameObjects[caller.gameObject][caller])
        {
            coroutineId.Remove(idCoroutine[coroutine]);
            idCoroutine.Remove(coroutine);

            caller.StopCoroutine(coroutine);
        }
        dictionaryForGameObjects[caller.gameObject].Remove(caller);
    }

    public class CoroutineFunction
    {
        private Coroutine coroutine;
        private ulong Id;
        private MonoBehaviour caller;

        CoroutineFunction(Coroutine coroutine, ulong id, MonoBehaviour caller)
        {
            this.coroutine = coroutine;
            Id = id;
            this.caller = caller;
        }

        public void CancelCoroutine()
        {
            CoroutineWait.CancelCoroutine(caller, coroutine);
        }
    }
}

