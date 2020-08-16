using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Mouse Look")]
    [SerializeField]
    private float _MouseSensitivity;
    [SerializeField]
    private Transform _Camera;
    private float _MouseX;
    private float _MouseY;
    private float _XRot;

    [Header("Player Physics")]
    [SerializeField]
    private Rigidbody _RB;
    [SerializeField]
    private float _Force;
    [SerializeField]
    private float _MaxVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookWithMouse();
    }

    private void LookWithMouse()
    {
        _MouseX = Input.GetAxis("Mouse X") * _MouseSensitivity * Time.deltaTime;
        _MouseY = Input.GetAxis("Mouse Y") * _MouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * _MouseX);
        _XRot -= _MouseY;
        _XRot = Mathf.Clamp(_XRot, -90, 90);
        _Camera.localRotation = Quaternion.Euler(_XRot, 0, 0);

    }
}
