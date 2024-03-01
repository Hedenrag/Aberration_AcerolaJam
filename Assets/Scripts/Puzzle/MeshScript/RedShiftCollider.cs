using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShiftCollider : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Colliding");
        List<ContactPoint> contacts = new();
        collision.GetContacts(contacts);

        for (int i = 1; i < contacts.Count; i++)
        {
            Debug.DrawLine(contacts[i].point, contacts[i - 1].point, Color.red);
        }
    }
}
