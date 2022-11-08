using System;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    private PlayerControls _inputActions;
    
    public Action OnReset;
    public Action OnQuit;
    
    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerControls();
            _inputActions.UI.Reset.performed += i => OnReset?.Invoke();
            _inputActions.UI.Quit.performed += i => OnQuit?.Invoke();

        }
        _inputActions.Enable();
    }
    
    private void Start()
    {
        OnReset += LevelManager.Instance.ResetLevel;
        OnQuit += LevelManager.Instance.QuitGame;
    }
    
    private void OnDisable()
    {
        _inputActions.Disable();
        OnReset -= LevelManager.Instance.ResetLevel;
        OnQuit -= LevelManager.Instance.QuitGame;
    } 
}
