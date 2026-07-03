using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private Vector2 move;
    [SerializeField] private Vector2 look;

    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference lookInput;

    void OnEnable() {
        moveInput.action.performed += HorizontalMovement;
        moveInput.action.canceled += CancelMovement;
        lookInput.action.performed += CameraMovement;
        lookInput.action.canceled += CancelLooking;
    }

    void OnDisable() {
        moveInput.action.performed -= HorizontalMovement;
        moveInput.action.canceled -= CancelMovement;
        lookInput.action.performed -= CameraMovement;
        lookInput.action.canceled -= CancelLooking;
    }

    private void HorizontalMovement(InputAction.CallbackContext ctx) {
        move = ctx.ReadValue<Vector2>();
    }

    private void CancelMovement(InputAction.CallbackContext ctx) {
        move = Vector2.zero;
    }

    private void CameraMovement(InputAction.CallbackContext ctx) {
        look = ctx.ReadValue<Vector2>();
    }

    private void CancelLooking(InputAction.CallbackContext ctx) {
        look = Vector2.zero;
    }
}
