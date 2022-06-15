using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private bool isReadyToSpawn = true;

    public void Spawn()
    {
        if (!isReadyToSpawn) return;
        Vector3 spawnPos = transform.position + Vector3.up;
        Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
        isReadyToSpawn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            isReadyToSpawn = true;
        }
    }
}