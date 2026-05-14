using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandeler : BaseInputHandeler,IPlayerInputService
{
    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action InteractEvent;
    public event Action SubmitEvent;
    public event Action SprintEvent;

    private void Awake()
    {
        ServiceLocator.Register<IPlayerInputService>(this);
    }

    public override void BindInputs()
    {
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Look.canceled += OnLook;
        inputActions.Player.Interact.performed += OnInteract;
        inputActions.Player.Submit.performed += OnSubmit;
        inputActions.Player.Sprint.performed += OnSprint;
    }


    public override void UnBindInputs()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Look.canceled -= OnLook;
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Submit.performed -= OnSubmit;
        inputActions.Player.Sprint.performed -= OnSprint;
    }

    private void OnMove(InputAction.CallbackContext callbackContext)
    {
        MoveEvent?.Invoke(callbackContext.ReadValue<Vector2>());
    }

    private void OnLook(InputAction.CallbackContext callbackContext)
    {
        LookEvent?.Invoke(callbackContext.ReadValue<Vector2>());
    }

    private void OnInteract(InputAction.CallbackContext callbackContext)
    {
        InteractEvent?.Invoke();
    } 

    private void OnSubmit(InputAction.CallbackContext callbackContext)
    {
        SubmitEvent?.Invoke();
    }

    private void OnSprint(InputAction.CallbackContext callbackContext)
    {
        SprintEvent?.Invoke();
    } 
}
