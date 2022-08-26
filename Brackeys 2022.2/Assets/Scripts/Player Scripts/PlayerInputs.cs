using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public static PlayerInputs Instance;

    [SerializeField] private InputActionAsset playerInputs;
    [SerializeField] private string playerActionMap;
    [Space]
    [SerializeField] private string movementActionStr;
    [SerializeField] private string jumpingActionStr;
    [SerializeField] private string aimingActionStr;
    [SerializeField] private string shootingActionStr;

    private InputAction movementAction;
    private InputAction jumpingAction;
    private InputAction aimingAction;
    private InputAction shootingAction;

    private Vector2 movementValue;
    private float jumpingValue;
    private Vector2 aimingValue;
    private float shootingValue;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }

        var actionMap = playerInputs.FindActionMap(playerActionMap);

        movementAction = actionMap.FindAction(movementActionStr);
        jumpingAction = actionMap.FindAction(jumpingActionStr);
        aimingAction = actionMap.FindAction(aimingActionStr);
        shootingAction = actionMap.FindAction(shootingActionStr);

        movementAction.performed += OnMovementChanged;
        movementAction.canceled += OnMovementChanged;
        movementAction.Enable();

        jumpingAction.performed += OnJumpChanged;
        jumpingAction.canceled += OnJumpChanged;
        jumpingAction.Enable();

        aimingAction.performed += OnAimingChanged;
        aimingAction.canceled += OnAimingChanged;
        aimingAction.Enable();

        shootingAction.performed += OnShootingChanged;
        shootingAction.canceled += OnShootingChanged;
        shootingAction.Enable();
    }

    private void OnMovementChanged(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
    }

    private void OnJumpChanged(InputAction.CallbackContext context)
    {
        jumpingValue = context.ReadValue<float>();
    }

    private void OnAimingChanged(InputAction.CallbackContext context)
    {
        aimingValue = context.ReadValue<Vector2>();
    }

    private void OnShootingChanged(InputAction.CallbackContext context)
    {
        shootingValue = context.ReadValue<float>();
    }

    public Vector2 GetMovement()
    {
        return movementValue;
    }

    public float GetJump()
    {
        return jumpingValue;
    }

    public Vector2 GetAim()
    {
        return aimingValue;
    }

    public float GetShooting()
    {
        return shootingValue;
    }
}
