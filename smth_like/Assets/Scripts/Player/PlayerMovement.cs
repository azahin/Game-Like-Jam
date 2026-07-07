using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour {
    private CharacterController controller;
    private Transform pivot;
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

    // --- Movement Logic --- //
    void Update() {
        CameraMovement(lookInput.action.ReadValue<Vector2>());

        Vector3 totalVelocity = Vector3.zero;
        totalVelocity += GetHorizontalVelocity(moveInput.action.ReadValue<Vector2>());
        controller.Move(totalVelocity * Time.deltaTime);
    }

    private Vector3 GetHorizontalVelocity(Vector2 moveDirection) {
        Vector3 velocity = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        velocity.y = 0.0f;
        velocity = walkSpeed * velocity.normalized;
        return velocity;
    }

    private void CameraMovement(Vector2 lookInput) {
        pan += InputManager.Instance.Sensitivity * lookInput.x;
        tilt -= InputManager.Instance.Sensitivity * lookInput.y;
        tilt = Mathf.Clamp(tilt, viewClamp.x, viewClamp.y);

        transform.rotation = Quaternion.Euler(0.0f, pan, 0.0f);
        pivot.localRotation = Quaternion.Euler(tilt, 0.0f, 0.0f);
    }
}
