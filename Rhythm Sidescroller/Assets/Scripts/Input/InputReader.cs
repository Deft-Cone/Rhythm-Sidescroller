using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]

public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
{
    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();

            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);
            SetGameplay();
        }
    }

    public void SetGameplay()
    {
        _gameInput.Gameplay.Enable();
        _gameInput.UI.Disable();
    }
    
    public void SetUI()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.UI.Enable();
    }

    public event Action<Vector2> MoveEvent;
    public event Action<Vector2> LookEvent;
    public event Action AimEvent;
    public event Action ShootEvent;
    public event Action JumpEvent;
    public event Action JumpCancelledEvent;

    public event Action PauseEvent;
    public event Action ResumeEvent;

    void GameInput.IGameplayActions.OnMovement(InputAction.CallbackContext context)
    {
        //Debug.Log("Phase:" + context.phase + " Value: " + context.ReadValue<Vector2>());
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    void GameInput.IGameplayActions.OnLook(InputAction.CallbackContext context)
    {
        LookEvent?.Invoke(context.ReadValue<Vector2>());
    }

    void GameInput.IGameplayActions.OnAim(InputAction.CallbackContext context)
    {
        AimEvent?.Invoke();
    }

    void GameInput.IGameplayActions.OnShoot(InputAction.CallbackContext context)
    {
        ShootEvent?.Invoke();
    }

    void GameInput.IGameplayActions.OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) 
        {
            JumpEvent?.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            JumpCancelledEvent?.Invoke();
        }
    }

    void GameInput.IGameplayActions.OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            SetUI();
        }
    }

    void GameInput.IUIActions.OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            SetGameplay();
        }
    }

    void GameInput.IGameplayActions.OnRun(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    void GameInput.IGameplayActions.OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
