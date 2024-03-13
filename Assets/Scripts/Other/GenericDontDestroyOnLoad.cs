using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericDontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] string key;
    static Dictionary<string, GameObject> instances = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (instances.ContainsKey(key)) { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
        instances.Add(key, gameObject);

    }
    private void OnDestroy()
    {
        instances.Remove(key);
    }
}
