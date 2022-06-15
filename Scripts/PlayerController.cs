using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Управление")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotateTime = 0.1f;
    private float smoothRotate;
    private CharacterController controller;

    [Header("Взаимодействие")]
    [SerializeField] private Transform hands;
    [SerializeField] private Transform defaultTransform;
    [SerializeField] private InteractableObject pickedObject;
    [SerializeField] private bool isInteracting;
    [SerializeField] private Transform dropPos;

    public bool inTrigger = false;
    public InteractableObject tempTrigger;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Moving();
        PickNDrop();
    }

    private void Moving()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothRotate, rotateTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(moveDirection * speed * Time.deltaTime);
        }
    }

    private void PickNDrop()
    {
        if (!inTrigger) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (isInteracting)
            {
                case true:
                    Drop();
                    break;

                case false:
                    PickUp();
                    break;
            }
        }
    }

    public void PickUp()
    {
        if (isInteracting || tempTrigger.isPickedUp) return;

        pickedObject = tempTrigger;
        pickedObject.isPickedUp = true;
        pickedObject.GetComponent<Rigidbody>().isKinematic = true;
        pickedObject.gameObject.transform.SetParent(hands);
        pickedObject.gameObject.transform.localPosition = Vector3.zero;
        pickedObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        isInteracting = true;
    }

    private void Drop()
    {
        if (pickedObject == null) return;

        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.isPickedUp = false;
        pickedObject.gameObject.transform.SetParent(null);
        isInteracting = false;
        Vector3 dir = BallisticVel(50);
        pickedObject.GetComponent<Rigidbody>().velocity = dir;
        pickedObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        pickedObject = null;
    }

    private Vector3 BallisticVel(float angle)
    {
        Vector3 dir = dropPos.position - pickedObject.gameObject.transform.position;
        float h = dir.y;
        dir.y = 0;
        float dist = dir.magnitude;
        float a = angle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += h / Mathf.Tan(a);

        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }
}