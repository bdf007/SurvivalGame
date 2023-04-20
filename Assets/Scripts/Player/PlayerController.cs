using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXLook;
    public float lookSensitivity;
    public bool invertY;

    private Vector2 mousDelta;

    // components
    private Rigidbody playerRig;

    private void Awake()
    {
        // get the components
        playerRig = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move ()
    {
        // calculate the move direction relative to where we're facing.
        Vector3 moveDir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        moveDir *= moveSpeed;
        moveDir.y = playerRig.velocity.y;

        // aasign our rigidbody velocity to the move direction
        playerRig.velocity = moveDir;
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void CameraLook ()
    {
        // rotate the camera container up and down
        camCurXLook -= mousDelta.y * lookSensitivity;
        // clamp the camera rotation
        camCurXLook = Mathf.Clamp(camCurXLook, minXLook, maxXLook);
        // apply the rotation
        if (invertY)
        {
            cameraContainer.localEulerAngles = new Vector3(-camCurXLook, 0, 0);
        }
        else
        {
            cameraContainer.localEulerAngles = new Vector3(camCurXLook, 0, 0);
        }
        // rotate the player left and right
        transform.eulerAngles += new Vector3(0, mousDelta.x * lookSensitivity, 0);
    }

    // called when we move our mouse - managed by the Input System
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mousDelta = context.ReadValue<Vector2>();
    }

    // called when we press WASD - managed by the Input System
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // are we holding down a movement button?
        if (context.phase == InputActionPhase.Performed)
        {
            // get the movement input
            curMovementInput = context.ReadValue<Vector2>();
        }
        // are we releasing a movement button?
        else if (context.phase == InputActionPhase.Canceled)
        {
            // reset the movement input
            curMovementInput = Vector2.zero;
        }
    }

    // called when we press down on the spacebar - managed by the Input System
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // is this the first frame we're pressing the button?
        if (context.phase == InputActionPhase.Started)
        {
            // are we grounded?
            if (IsGrounded())
            {
                // jump
                playerRig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down)
        };
        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    // show the ray in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f) + (Vector3.up * 0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down);
    }
}
