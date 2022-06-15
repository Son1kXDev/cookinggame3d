using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            if (!other.GetComponent<InteractableObject>().isPickedUp) Destroy(other.gameObject);
        }
    }
}