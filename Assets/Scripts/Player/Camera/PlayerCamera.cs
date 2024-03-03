using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform m_camera;
    [SerializeField] float sensitivity = 1.0f;

    [Space(10f)]
    [SerializeField] float maxLookUp;
    [SerializeField] float maxLookDown;

    float currentCameraRotation = 0f;

    public bool blockCamera;

    public void MouseInput(InputAction.CallbackContext context)
    {
        if (blockCamera) return;

        Vector2 mouseInput = context.ReadValue<Vector2>();

        mouseInput *= sensitivity;

        float vertical = mouseInput.y;
        float horizontal = mouseInput.x;

        if (vertical + currentCameraRotation > maxLookUp)
        {
            vertical = maxLookUp - currentCameraRotation;
        }else if(vertical + currentCameraRotation < maxLookDown)
        {
            vertical = maxLookDown - currentCameraRotation;
        }
        currentCameraRotation += vertical;
        m_camera.Rotate(Vector3.left * vertical, Space.Self);
        transform.Rotate(transform.up * horizontal, Space.World);
    }

}
