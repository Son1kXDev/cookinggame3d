using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            player.inTrigger = true;
            player.tempTrigger = other.GetComponent<InteractableObject>();
        }
        if (other.CompareTag("Spawner"))
        {
            player.inSpawner = true;
            player.spawner = other.GetComponent<ItemSpawner>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            player.inTrigger = false;
            player.tempTrigger = null;
        }
        if (player.pickedObject != null && other.GetComponent<InteractableObject>() == player.pickedObject)
        {
            player.inTrigger = true;
        }
        if (other.CompareTag("Spawner"))
        {
            player.inSpawner = false;
            player.spawner = null;
        }
    }
}