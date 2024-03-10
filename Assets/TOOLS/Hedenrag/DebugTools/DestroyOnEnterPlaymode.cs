using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnterPlaymode : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject);
    }
}
