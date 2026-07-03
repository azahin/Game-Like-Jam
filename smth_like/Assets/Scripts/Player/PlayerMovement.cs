using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private CharacterController controller;

    [Header("Input References")]
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference lookInput;

    [Header("Movement Variables")]
    private Vector3 moveDirection;
    [SerializeField] private float walkSpeed;

    // --- Connections --- //
    void Start() {
        controller = GetComponent<CharacterController>();
    }

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

    // --- Movement Logic --- //
    void Update() {
        Vector3 velocity = walkSpeed * moveDirection;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HorizontalMovement(InputAction.CallbackContext ctx) {
        Vector2 rawInput = ctx.ReadValue<Vector2>();
        moveDirection = transform.forward * rawInput.y + transform.right * rawInput.x;
        moveDirection.y = 0.0f;
    }

    private void CancelMovement(InputAction.CallbackContext ctx) {
        moveDirection = Vector3.zero;
    }

    private void CameraMovement(InputAction.CallbackContext ctx) {
        // look = ctx.ReadValue<Vector2>();
    }

    private void CancelLooking(InputAction.CallbackContext ctx) {
        // look = Vector2.zero;
    }
}
