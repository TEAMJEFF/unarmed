using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    private const float yAngleMin = 0.0f;
    private const float yAngleMax = 30.0f;

    public Transform lookAt;
    public Transform camTransform;
    private Rigidbody rb;

    private Camera cam;

    private RaycastHit hit;

    private float distance = 7.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensX = 4.0f;
    private float sensY = 1.0f;

    private float momentum;
    public float speed;
    private float step;

    private void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //currentX += Input.GetAxis("Mouse X");
        //currentY += Input.GetAxis("Mouse Y");

        //currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 3, -distance);
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }

    void Shake()
    {
        Debug.Log("Gets here");
        CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 0.5f);
    }

}
