﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Gets a movement vector
    public void Move (Vector3 _velocity)
    {
        velocity = _velocity;
    }


    // Gets a rotational vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    // Gets a rotational vector for the camera
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * 300);
    }

    // Run every physics iteration
    void FixedUpdate ()
    {
        PerformMovement();
        PerformRotation();
    }

    // Perform movement based on velocity variable
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            //cam.transform.Rotate(-cameraRotation);
            // clamp the rotation
            Vector3 eulerAngles = cam.transform.localEulerAngles;
            eulerAngles.x -= cameraRotation.x;
            if (eulerAngles.x > 90 && eulerAngles.x < 180)
                eulerAngles.x = 90;
            else if (eulerAngles.x >= 180 && eulerAngles.x < 270)
                eulerAngles.x = 270;
            cam.transform.localEulerAngles = eulerAngles;
        }
    }
}
