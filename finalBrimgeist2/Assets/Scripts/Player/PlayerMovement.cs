using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    public Vector2 MoveInput { get; private set; } = Vector2.zero;

    InputActions _input = null;
    

    public void OnEnable()
    {
        _input = new InputActions();
        _input.Movement.Enable();
        _input.Movement.Move.canceled += SetMove;
        _input.Movement.Move.performed += SetMove;

        _input.Extras.CharacterPanel.Enable();
        _input.Extras.InventoryPanel.Enable();
        _input.Extras.Pause.Enable();
    }

    public void OnDisable()
    {
        _input.Movement.Move.performed -= SetMove;
        _input.Movement.Move.canceled -= SetMove;

        _input.Movement.Disable();

        _input.Extras.CharacterPanel.Disable();
        _input.Extras.InventoryPanel.Disable();
        _input.Extras.Pause.Disable();
    }

    void SetMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

}
