using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float lookSensitivity;
    [SerializeField] private float lookRange;

    private Transform playerObject;

    private Vector2 lookInputs;
    float xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        lookInputs = PlayerInputs.Instance.GetAim() * lookSensitivity * Time.deltaTime;

        xRotation -= lookInputs.y;
        xRotation = Mathf.Clamp(xRotation, -lookRange, lookRange);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.parent.Rotate(Vector3.up * lookInputs.x);
    }
}
