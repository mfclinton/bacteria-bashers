using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // State
    public Vector2 PointerPosition { get; private set; }
    public Vector2 PointerWorldPosition { get; private set; }
    
    // Events
    public delegate void OnPausePressedHandler();
    public event OnPausePressedHandler OnPausePressed;
    
    // References
    private InputSystem_Actions inputActions;
    private Camera mainCamera;

    #region Unity Callbacks

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Register Callbacks
        inputActions.Player.Cursor.performed += OnPointPerformed;
        inputActions.Player.Cursor.canceled += OnPointCanceled;
        inputActions.Player.Pause.performed += OnPausePerformed;
        
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        // UnRegister Callbacks
        inputActions.Player.Cursor.performed -= OnPointPerformed;
        inputActions.Player.Cursor.canceled -= OnPointCanceled;
        inputActions.Player.Pause.performed -= OnPausePerformed;
        
        inputActions.Player.Disable();
    }

    #endregion

    #region Input Callbacks

    private void OnPointPerformed(InputAction.CallbackContext context)
    {
        Vector2 pointerPosition = context.ReadValue<Vector2>();
        SetPointerPosition(pointerPosition);
    }

    private void OnPointCanceled(InputAction.CallbackContext context)
    {
        Vector2 pointerPosition = context.ReadValue<Vector2>();
        SetPointerPosition(pointerPosition);
    }
    
    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        OnPausePressed?.Invoke();
    }

    #endregion

    #region Helpers

    private void SetPointerPosition(Vector2 pointerPosition)
    {
        PointerPosition = pointerPosition;
        PointerWorldPosition = mainCamera.ScreenToWorldPoint(PointerPosition); // TODO: Better way to set this
    }

    #endregion
}