using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [Space]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDistance;

    private Rigidbody body;

    private Vector2 movementInputs;
    private float jumpInput;

    private float _verticalVelocity = 0;
    private Vector3 _playerVelocity;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ReadInputs();
        if (CheckGrounded())
        {
            _verticalVelocity = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
        } else
        {
            _verticalVelocity = 0;
        }

        movementInputs *= movementSpeed;

        _playerVelocity = transform.forward * movementInputs.y + transform.up * body.velocity.y + transform.right * movementInputs.x;
        _playerVelocity += transform.up * _verticalVelocity * jumpInput;

        body.velocity = _playerVelocity;
    }

    private void ReadInputs()
    {
        movementInputs = PlayerInputs.Instance.GetMovement();
        jumpInput = PlayerInputs.Instance.GetJump();
    }

    private bool CheckGrounded()
    {
        return Physics.CheckSphere(transform.position, groundDistance, groundLayer);
    }
}
