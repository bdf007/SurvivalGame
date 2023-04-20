using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXLook;
    public float lookSensitivity;
    public bool invertY;

    private Vector2 mousDelta;

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
}
