using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAroundPlayer : MonoBehaviour
{
    public bool lockCursor;
    //create sensivity of the mouse and the distance btw camera and player
    public float mouseSensitivity = 10;
    public Transform player;
    public float dstFromPlayer = 7;

    // set max and min Rotation
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    // set variables for a smooth rotation
    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    //set Rotation Axis
    float yaw;
    float pitch;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Set the Inputs
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        // add max and min pitch Rotation
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        //Create smooth Rotation of the camera around the player
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = player.position - transform.forward * dstFromPlayer;

        //running camera
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dstFromPlayer = 9;
        }

        else
        {
            dstFromPlayer = 7;
        }
    }

}
