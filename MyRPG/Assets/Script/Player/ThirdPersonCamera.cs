using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private Transform orientation;
    [SerializeField] private Rigidbody rb;

    [Header("Attributes")]
    [SerializeField] private float rotationSpeed = 7f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        float horizontalInput = gameInput.GetHorizontalNormalized();
        float verticalInput = gameInput.GetVerticalNormalized();

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            playerVisual.forward = Vector3.Slerp(playerVisual.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

    }
}