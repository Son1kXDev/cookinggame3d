using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceScript : MonoBehaviour
{
    [SerializeField] private float placeHeight = 0.5f;
    [SerializeField] private InteractableObject currentPlacedObject;
    [SerializeField] private bool isEmpty = true;

    private void Place()
    {
        if (!isEmpty) return;
        isEmpty = false;
        currentPlacedObject.GetComponent<Transform>().SetParent(transform.parent);
        currentPlacedObject.GetComponent<Rigidbody>().isKinematic = true;
        currentPlacedObject.gameObject.transform.localPosition = new Vector3(0, placeHeight, 0);
        currentPlacedObject.gameObject.transform.localEulerAngles = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable" && isEmpty)
        {
            currentPlacedObject = other.GetComponent<InteractableObject>();
            if (!currentPlacedObject.isPickedUp) Place();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractableObject>() == currentPlacedObject)
        {
            isEmpty = true;
            currentPlacedObject = null;
        }
    }
}