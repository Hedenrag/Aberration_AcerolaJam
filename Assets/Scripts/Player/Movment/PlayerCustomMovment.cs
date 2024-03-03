using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
[RequireComponent(typeof(CharacterControllerV2))]
public class PlayerCustomMovment : MonoBehaviour
{
    [SerializeField, HideInNormalInspector] GroundDetection _groundDetect;

    //Variables 
    [SerializeField] float _acceleration = 12f;
    [SerializeField] float _targetVelocity = 10f;

    [SerializeField, Range(0f, 1f)] float _turnaroundStrength;

    private void OnValidate()
    {
        _groundDetect = GetComponent<GroundDetection>();
    }

    /// <summary>
    /// Logic for the horizontal movment
    /// </summary>
    /// <param name="currentVelocity">Velocity from the player before beeing modified</param>
    /// <param name="input">Target direction for the player</param>
    /// <returns>new Velocity for the player</returns>
    public Vector2 Movment(Vector2 currentVelocity, Vector2 input)
    {
        //Example

        //reduction of velocity by change of direction
        currentVelocity *= VelocityRemaining(currentVelocity.normalized, input);

        //acceleration
        float targetVelSq = _targetVelocity * _targetVelocity;
        if (currentVelocity.sqrMagnitude < targetVelSq)
        {
            currentVelocity += (input * _acceleration * Time.fixedDeltaTime);
            if (currentVelocity.sqrMagnitude > targetVelSq)
            {
                float mag = Mathf.Clamp(currentVelocity.magnitude, 0f, _targetVelocity);
                currentVelocity = mag * currentVelocity.normalized;
            }
        }

        //slide off
        if (_groundDetect.Sliding)
        {
            //TODO sliding off logic
        }

        return currentVelocity;
    }

    //can be deleted only used for the example
    float VelocityRemaining(Vector3 currentVelocity, Vector3 input)
    {
        float dot = Vector2.Dot(currentVelocity.normalized, input.normalized);

        dot += 1f + _turnaroundStrength;
        dot /= 2f + _turnaroundStrength;
        dot = Mathf.Pow(dot, 1f - _turnaroundStrength);
        return dot;
    }
}
