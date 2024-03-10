using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hedenrag
{
    namespace ExVar
    {
        [System.Serializable]
        public struct ExtraLayers : ISerializationCallbackReceiver
        {
            [SerializeField, HideInInspector] ExLayer _layer;
            [SerializeField, HideInInspector] int _value;
            public ExLayer Layer => _layer;
            public int Value => _value;

            void OnValidate() { }
            void ISerializationCallbackReceiver.OnBeforeSerialize() => OnValidate();
            void ISerializationCallbackReceiver.OnAfterDeserialize() { }

            public ExtraLayers(ExLayer layer, int val)
            {
                this._layer = layer;
                _value = val;
            }

            public bool GetLayer(int index)
            {
                return (_value & (1 << index)) != 0;
            }
            public void SetLayer(int index, bool val)
            {
                _value |= (1 << index);
            }

            public bool this[int index]
            {
                get => GetLayer(index);
                set => SetLayer(index, value);
            }

            public static ExtraLayers operator &(ExtraLayers a, ExtraLayers b)
            {
                return new ExtraLayers(a._layer, a._value & b._value);
            }
            public static ExtraLayers operator |(ExtraLayers a, ExtraLayers b)
            {
                return new ExtraLayers(a._layer, a._value | b._value);
            }

            public static ExtraLayers operator +(ExtraLayers a, ExtraLayers b)
            {
                return new ExtraLayers(a._layer, a._value | b._value);
            }
            public static ExtraLayers operator -(ExtraLayers a, ExtraLayers b)
            {
                return new ExtraLayers(a._layer, a._value & (~b._value));
            }

            public static ExtraLayers operator +(ExtraLayers a, ExtraLayer val)
            {
                return new ExtraLayers(a._layer, a._value | val.Value);
            }
            public static ExtraLayers operator -(ExtraLayers a, ExtraLayer val)
            {
                return new ExtraLayers(a._layer, a._value & (~val.Value));
            }

            public bool Contains(ExtraLayer val)
            {
                int res = val.Value & _value;
                return res != 0;
            }
        }
    }
}