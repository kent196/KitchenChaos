using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions _actions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private void Awake()
    {
        _actions = new PlayerInputActions();
        _actions.Player.Enable();
        _actions.Player.Interact.performed += Interact_performed;
        _actions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)

    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetGameInputNormalized()
    {
        Vector2 inputVector = _actions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
