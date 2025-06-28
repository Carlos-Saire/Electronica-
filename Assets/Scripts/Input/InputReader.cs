using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public static event Action<Vector2> OnCameraMove;
    public static event Action<Vector2> OnPlayerMove;

    public void ReadCameraInput(InputAction.CallbackContext context)
    {
        OnCameraMove?.Invoke(context.ReadValue<Vector2>());
    }
    public void ReadPlayerInput(InputAction.CallbackContext context)
    {
        OnPlayerMove?.Invoke(context.ReadValue<Vector2>());
    }
}
