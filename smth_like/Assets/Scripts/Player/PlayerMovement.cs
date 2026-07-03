using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour {
    private CharacterController controller;
    private Transform pivot;
    private Vector3 moveDirection;
    private float pan;
    private float tilt;

    [Header("Input References")]
    [SerializeField] private InputActionReference moveInput;
    [SerializeField] private InputActionReference lookInput;

    [Header("Movement Variables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private Vector2 viewClamp;

    // --- Connections --- //
    void Start() {
        controller = GetComponent<CharacterController>();
        pivot = GetComponentInChildren<CinemachineCamera>().transform;
        pan = transform.eulerAngles.y;
        tilt = pivot.localEulerAngles.x;
    }

    void OnEnable() {
        moveInput.action.performed += HorizontalMovement;
        moveInput.action.canceled += CancelMovement;
        lookInput.action.performed += CameraMovement;
    }

    void OnDisable() {
        moveInput.action.performed -= HorizontalMovement;
        moveInput.action.canceled -= CancelMovement;
        lookInput.action.performed -= CameraMovement;
    }

    // --- Movement Logic --- //
    void Update() {
        Vector3 velocity;
        velocity = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        velocity.y = 0.0f;
        velocity = walkSpeed * velocity.normalized;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HorizontalMovement(InputAction.CallbackContext ctx) {
        moveDirection = ctx.ReadValue<Vector2>();
    }

    private void CancelMovement(InputAction.CallbackContext ctx) {
        moveDirection = Vector3.zero;
    }

    private void CameraMovement(InputAction.CallbackContext ctx) {
        Vector2 rawInput = ctx.ReadValue<Vector2>();
        pan += InputManager.Instance.Sensitivity * rawInput.x;
        tilt -= InputManager.Instance.Sensitivity * rawInput.y;
        tilt = Mathf.Clamp(tilt, viewClamp.x, viewClamp.y);

        transform.rotation = Quaternion.Euler(0.0f, pan, 0.0f);
        pivot.localRotation = Quaternion.Euler(tilt, 0.0f, 0.0f);
    }
}
