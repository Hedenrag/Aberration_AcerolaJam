using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hedenrag
{
    namespace ExVar
    {
        [System.Serializable]
        public struct ExtraLayer : ISerializationCallbackReceiver, IEquatable<ExtraLayer>
        {
            [SerializeField, HideInInspector] ExLayer _layer;
            [SerializeField, HideInInspector] int _value;

            public int Value => _value;

            void OnValidate() { }
            void ISerializationCallbackReceiver.OnBeforeSerialize() => OnValidate();
            void ISerializationCallbackReceiver.OnAfterDeserialize() { }

            public ExtraLayer(ExLayer layer, int val)
            {
                this._layer = layer;
                _value = val;
            }
            public ExtraLayer(ExLayer layer)
            {
                this._layer = layer;
                _value = 0;
            }

            public ExtraLayer(ExtraLayer layer)
            {
                this._layer = layer._layer;
                this._value = layer._value;
            }

            public bool IsIn(ExtraLayers layers)
            {
                int res = layers.Value & _value;
                return res != 0;
            }

            public override bool Equals(object obj)
            {
                return obj is ExtraLayer layer && Equals(layer);
            }

            public bool Equals(ExtraLayer other)
            {
                return EqualityComparer<ExLayer>.Default.Equals(_layer, other._layer) &&
                       _value == other._value &&
                       Value == other.Value;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(_layer, _value, Value);
            }

            public static bool operator ==(ExtraLayer left, ExtraLayer right) { return (left._layer == right._layer && left._value == right._value); }
            public static bool operator !=(ExtraLayer left, ExtraLayer right) { return !(left == right); }
        }
    }
}