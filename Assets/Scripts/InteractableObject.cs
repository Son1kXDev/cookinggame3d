using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public bool isPickedUp;
    public bool isPlaced;
    public bool isCutted = false;

    [SerializeField] private float cutSteps;
    [SerializeField] private Slider stepSlider;
    [SerializeField] private GameObject cuttedModel;
    [SerializeField] private GameObject mainModel;

    public void ChangeColliderState(bool state)
    {
        cuttedModel.GetComponentInChildren<MeshCollider>().enabled = state;
        mainModel.GetComponentInChildren<MeshCollider>().enabled = state;
    }

    public void CutObj(PlayerController currentPlayer)
    {
        StartCoroutine(cuttingProcess(currentPlayer));
    }

    private IEnumerator cuttingProcess(PlayerController currentPlayer)
    {
        currentPlayer.waitForCut = true;
        stepSlider.gameObject.SetActive(true);
        while (cutSteps >= 0)
        {
            stepSlider.value = cutSteps;
            cutSteps--;
            yield return new WaitForSecondsRealtime(1);
        }
        isCutted = true;
        mainModel.SetActive(false);
        cuttedModel.SetActive(true);
        stepSlider.gameObject.SetActive(false);
        currentPlayer.waitForCut = false;
    }
}