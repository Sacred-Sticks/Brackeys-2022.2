using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public static PlayerInputs Instance;

    [SerializeField] private InputActionAsset playerInputs;
    [SerializeField] private string playerActionMap;
    [Space]
    [SerializeField] private string movementActionStr;
    [SerializeField] private string aimingActionStr;
    [SerializeField] private string shootingActionStr;

    private InputAction movementAction;
    private InputAction aimingAction;
    private InputAction shootingAction;

    private Vector2 movementValue;
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
        aimingAction = actionMap.FindAction(aimingActionStr);
        shootingAction = actionMap.FindAction(shootingActionStr);

        movementAction.performed += OnMovementChanged;
        movementAction.canceled += OnMovementChanged;
        movementAction.Enable();

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

    public Vector2 GetAim()
    {
        return aimingValue;
    }

    public float GetShooting()
    {
        return shootingValue;
    }
}
