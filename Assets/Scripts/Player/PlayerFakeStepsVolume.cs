using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFakeStepsVolume : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] Rigidbody rb;

    [SerializeField] float maxSpeed;

    private void Update()
    {
        float speed = new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude;
        audioSource.volume = speed / maxSpeed;
    }
}
