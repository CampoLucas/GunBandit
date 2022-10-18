using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls _inputActions;

    private Vector2 _movementInput;
    private Vector2 _mousePos;
    private float _mouseScrollY;
    private bool _isHolding;
    private bool _fireInput;
    
    /// <summary>
    /// Gets horizontal and vertical input
    /// </summary>
    public Vector2 MovementInput => _movementInput;
    public Vector2 MousePosition => _mousePos;
    public Action OnFire;
    public Action OnThrow;
    public Action OnReload;
    public Action OnInteractPerformed;
    public Action OnInteractStarted;
    public Action OnInteractCancelled;
    public Action OnScrollUp;
    public Action OnScrollDown;

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerControls();
            _inputActions.Player.Movement.performed += inputActions => _movementInput = inputActions.ReadValue<Vector2>();
            _inputActions.Player.Point.performed += i => _mousePos = i.ReadValue<Vector2>();
            _inputActions.Player.MouseScrollY.performed += i => _mouseScrollY = i.ReadValue<float>();
            
            _inputActions.Player.Fire.started += i => _fireInput = true;
            _inputActions.Player.Fire.canceled += i => _fireInput = false;
            
            _inputActions.Player.Throw.performed += i => OnThrow?.Invoke();
            _inputActions.Player.Reload.performed += i => OnReload?.Invoke();
            
            _inputActions.Player.Interact.performed += i => OnInteractPerformed?.Invoke();
            _inputActions.Player.Interact.started += i => OnInteractStarted?.Invoke();
            _inputActions.Player.Interact.canceled += i => OnInteractCancelled?.Invoke();
            
            
            
        }
        _inputActions.Enable();
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void LateUpdate()
    {
        if(_isHolding) return;
        _fireInput = false;
    }

    private void OnDisable() => _inputActions.Disable();

    private void HandleAllInputs()
    {
        HandleFireInput();
        HandleScrollInput();
    }

    private void HandleFireInput()
    {
        if(_fireInput)
            OnFire?.Invoke();
    }
    private void HandleScrollInput()
    {
        switch (_mouseScrollY)
        {
            case > 0:
                OnScrollUp?.Invoke();
                break;
            case < 0:
                OnScrollDown?.Invoke();
                break;
        }
    }

    public void SetHolding(in bool isHolding) => _isHolding = isHolding;

}
