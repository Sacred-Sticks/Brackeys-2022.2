using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun : MonoBehaviour
{
    [SerializeField] private float minMovement;
    [Space]
    [SerializeField] private float wallCheckDistance;
    [Space]
    [SerializeField] private float forwardForce;
    [SerializeField] private float sidewardForce;
    [SerializeField] private float upwardForce;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float rotationTime;

    private Rigidbody body;
    private Transform cameraTransform;

    private Vector2 movementInput;
    private int direction;
    private float lerpValue;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        cameraTransform = FindObjectOfType<Camera>().transform;
    }

    private void FixedUpdate()
    {
        movementInput = PlayerInputs.Instance.GetMovement();

        //Check to make sure moving either left or right as well as forward
        if (-minMovement < movementInput.x && movementInput.x < minMovement) 
        {
            ResetCamera();
            return;
        }
        if (movementInput.y < minMovement)
        {
            ResetCamera();
            Debug.Log(movementInput);
            return;
        }

        if (PlayerMovement.Instance.CheckGrounded())
        {
            ResetCamera();
            return;
        }

        direction = PlayerMovement.Instance.CheckSide(wallCheckDistance);
        if (direction == 0)
        {
            RotateCamera();
            return;
        }

        lerpValue += Time.fixedDeltaTime * rotationTime;
        lerpValue = Mathf.Clamp(lerpValue, 0, 1);
        //RotateCamera();
        body.velocity += transform.up * upwardForce + transform.forward * forwardForce + transform.right * sidewardForce * direction;
    }

    private void RotateCamera()
    {
        float rotateTo = Mathf.Lerp(0, rotationAngle, lerpValue);
        Quaternion rotation = Quaternion.Euler(cameraTransform.rotation.x, cameraTransform.rotation.y, rotateTo * direction);

        cameraTransform.rotation = rotation;
        //Debug.Log("Rotated Camera to " + rotateTo);
    }

    private void ResetCamera()
    {
        lerpValue = 0;
        RotateCamera();
    }
}
