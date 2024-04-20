using UnityEngine;
using UnityEngine.InputSystem;


public class UserInput: MonoBehaviour
{
    public static UserInput Instance {  get; private set; }

    public PlayerInput playerInput;
    public Vector2 MoveInput {  get; private set; }
    public bool IsThrowPressed {  get; private set; }
    public bool IsMenuPressed { get; private set; }

    private InputAction _moveAction;
    private InputAction _throwAction;
    private InputAction _menuAction;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        playerInput = GetComponent<PlayerInput>();
        _moveAction = playerInput.actions["Move"];
        _throwAction = playerInput.actions["Throw"];
        _menuAction = playerInput.actions["Menu"];
    }

    private void Update()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
        IsThrowPressed = _throwAction.WasPressedThisFrame();
        IsMenuPressed = _menuAction.WasPressedThisFrame();
    }
}
