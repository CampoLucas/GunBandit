using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

/// <summary>
/// Use a gamepad as a cursor
/// </summary>
public class GamepadCursor : MonoBehaviour
{
    //Todo: Find a way to work with a custom Input Script
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private RectTransform cursorRectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private float cursorSpeed = 1000f;
    [SerializeField] private float padding = 35f;

    private bool _previousMouseState;
    private Mouse _virtualMouse;
    private Mouse _currentMouse;
    private Camera _mainCamera;

    private string _previousControlScheme = "";
    private const string GamepadScheme = "Gamepad";
    private const string MouseScheme = "Keyboard&Mouse";

    private void OnEnable()
    {
        _mainCamera = Camera.main;
        _currentMouse = Mouse.current;

        var virtualMouseInputDevice = InputSystem.GetDevice("VirtualMouse");
        if(virtualMouseInputDevice == null)
            _virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        else if (!virtualMouseInputDevice.added)
            _virtualMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
        else
            _virtualMouse = (Mouse)virtualMouseInputDevice;
        
        // // Pair the device to the user to use the PlayerInput component with the Event System & the Virtual Mouse.
        // //Todo: Find a to replace PlayerInput with a custom Input Script
        InputUser.PerformPairingWithDevice(_virtualMouse, playerInput.user);
        if (cursorRectTransform != null)
        {
            var position = cursorRectTransform.anchoredPosition;
            InputState.Change(_virtualMouse.position, position);
        }
        playerInput.onControlsChanged += OnControlsChanged;
        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDisable()
    {
        if(_virtualMouse != null && _virtualMouse.added) InputSystem.RemoveDevice(_virtualMouse);
        playerInput.onControlsChanged -= OnControlsChanged;
        InputSystem.onAfterUpdate -= UpdateMotion;
    }

    private void UpdateMotion()
    {
        if(_virtualMouse == null || Gamepad.current == null) return;
        // Delta
        var deltaValue = Gamepad.current.rightStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;

        var currentPos = _virtualMouse.position.ReadValue();
        var newPos = currentPos + deltaValue;

        newPos.x = Mathf.Clamp(newPos.x, padding, Screen.width - padding);
        newPos.y = Mathf.Clamp(newPos.y, padding, Screen.height - padding);
        
        InputState.Change(_virtualMouse.position, newPos);
        InputState.Change(_virtualMouse.delta, deltaValue);

        var aButtonIsPressed = Gamepad.current.aButton.IsPressed();
        if (_previousMouseState != aButtonIsPressed)
        {
            _virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(_virtualMouse, mouseState);
            _previousMouseState = aButtonIsPressed;
        }

        AnchorCursor(newPos);
    }

    private void AnchorCursor(in Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode 
            == RenderMode.ScreenSpaceOverlay ? null : _mainCamera, out var anchoredPosition);
        cursorRectTransform.anchoredPosition = anchoredPosition;
    }

    private void OnControlsChanged(PlayerInput input)
    {
        if (playerInput.currentControlScheme == MouseScheme && _previousControlScheme != MouseScheme)
        {
            Cursor.visible = true;
            cursorRectTransform.gameObject.SetActive(false);
            _currentMouse.WarpCursorPosition(_virtualMouse.position.ReadValue());
            _previousControlScheme = MouseScheme;
        }
        else if (playerInput.currentControlScheme == GamepadScheme && _previousControlScheme != GamepadScheme)
        {
            Cursor.visible = false;
            cursorRectTransform.gameObject.SetActive(true);
            InputState.Change(_virtualMouse.position, _currentMouse.position.ReadValue());
            AnchorCursor(_currentMouse.position.ReadValue());
            _previousControlScheme = GamepadScheme;
        }
    }

    // private void Update()
    // {
    //     if(_previousControlScheme != playerInput.currentControlScheme)
    //         OnControlsChanged(null);
    //     _previousControlScheme = playerInput.currentControlScheme;
    // }
}
