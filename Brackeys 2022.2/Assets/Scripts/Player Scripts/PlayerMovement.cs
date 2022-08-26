using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody body;

    private Vector2 movementInputs;
    private Vector3 playerVelocity;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        movementInputs = PlayerInputs.Instance.GetMovement();

        playerVelocity = transform.forward * movementInputs.y + transform.right * movementInputs.x;
        playerVelocity *= movementSpeed;

        body.velocity = playerVelocity;
    }
}
