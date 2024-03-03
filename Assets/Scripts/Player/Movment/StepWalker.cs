using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroundDetection))]
[RequireComponent(typeof(CharacterControllerV2))]
public class StepWalker : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float initialStepHeight = 0.1f;
    [SerializeField, Range(0f, 1f)] float initialStepDepth = 0.6f;
    [SerializeField, Range(0f, 1f)] float secondStepHeight = 0.3f;
    [SerializeField, Range(0f, 1f)] float minStepSize = 0.18f;

    [SerializeField,Range(0f, 100f)] float stepUpSpeed = 2f;
    [SerializeField,Range(0f, 90f)] float raySeparation = 45f;

    [SerializeField, HideInNormalInspector] CharacterControllerV2 controller;
    [SerializeField, HideInNormalInspector] GroundDetection groundDetection;



    private void OnValidate()
    {
        controller = GetComponent<CharacterControllerV2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 origin = transform.position + transform.up * initialStepHeight;

        Vector3 dir = controller.CurrentInputDir;

        if (dir.magnitude < 0.2) return;

        dir = dir.normalized;

        StepUpFunction(origin, dir);
        dir = Quaternion.AngleAxis(raySeparation, transform.up) * dir;
        StepUpFunction(origin, dir);
        dir = Quaternion.AngleAxis(-(raySeparation * 2f), transform.up) * dir;
        StepUpFunction(origin, dir);
    }

    private void StepUpFunction(Vector3 origin, Vector3 dir)
    {
        if (Physics.Raycast(origin, dir, initialStepDepth, groundDetection.GroundMask))
        {
            if (!Physics.Raycast(origin + transform.up * secondStepHeight, dir, initialStepDepth + minStepSize, groundDetection.GroundMask))
            {
                Debug.LogWarning("WalikingUp");
                transform.position += stepUpSpeed * Time.fixedDeltaTime * transform.up;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position + transform.up * initialStepHeight;
        Vector3 dir;

        if (Application.isPlaying)
        {
            dir = controller.CurrentInputDir;
            if (dir.magnitude < 0.05)
            {
                dir = transform.forward;
            }
        }
        else
        {
            dir = transform.forward;
        }
        dir = dir.normalized;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, dir * initialStepDepth);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin + transform.up * secondStepHeight, dir * (initialStepDepth + minStepSize));

        dir = Quaternion.AngleAxis(raySeparation, transform.up) * dir;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, dir * initialStepDepth);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin + transform.up * secondStepHeight, dir * (initialStepDepth + minStepSize));

        dir = Quaternion.AngleAxis(-(raySeparation * 2f), transform.up) * dir;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, dir * initialStepDepth);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin + transform.up * secondStepHeight, dir * (initialStepDepth + minStepSize));
    }

}
