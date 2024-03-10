using Hedenrag.ExVar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHolder : MonoBehaviour
{
    static Optional<CoroutineHolder> instance = new (null, false);

    public static CoroutineHolder Instance
    {
        get
        {
            if (instance)
            {
                return instance.Value;
            }
            instance = new(new GameObject("CoroutineHolder").AddComponent<CoroutineHolder>(), true);
            DontDestroyOnLoad(instance.Value);
            return instance.Value;
        }
    }
}
