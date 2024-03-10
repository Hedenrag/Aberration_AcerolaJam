using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hedenrag.ExVar
{
    [System.Serializable]
    public struct ScriptableObjectReferencer<T> where T : ScriptableObject
    {
        public T Value => _scriptableObject;
        [SerializeField] T _scriptableObject;
    }
}

