using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Mouse Look")]
    [SerializeField]
    private float _MouseSensitivity;
    [SerializeField]
    private Camera _Camera;
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
    private float _Horizontal;
    private float _Vertical;
    private float _Thrusters;
    private bool _Brake;
    private float _Roll;
    private Vector3 _Direction;
    private Vector3 _CameraDirection;

    void Update()
    {
        LookWithMouse();
        PlayerMovementInput();
    }
    void FixedUpdate()
    {
        ApplyPlayerPhysics();
    }

    private void LookWithMouse()
    {
        Vector3 mouse = Input.mousePosition;
        _CameraDirection = _Camera.ScreenToWorldPoint(mouse);
        _MouseX = Input.GetAxis("Mouse X") * _MouseSensitivity * Time.deltaTime;
        _MouseY = Input.GetAxis("Mouse Y") * _MouseSensitivity * Time.deltaTime;

        //transform.rotation = (Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_CameraDirection),0.01f));

        transform.Rotate(0, Input.GetAxis("Mouse X") * _MouseSensitivity * Time.deltaTime, 0);
        transform.Rotate(-Input.GetAxis("Mouse Y") * _MouseSensitivity *Time.deltaTime, 0, 0);
        transform.Rotate(0, 0, -Input.GetAxis("Roll") * 45 * Time.deltaTime);


        //transform.Rotate(Vector3.up * _MouseX);
        //_XRot -= _MouseY;
        //_XRot = Mathf.Clamp(_XRot, -90, 90);
        //_Camera.transform.localRotation = Quaternion.Euler(_XRot, _MouseX, 0);
        //transform.LookAt(_Camera.transform.forward);
    }

    private void PlayerMovementInput()
    {
        _Horizontal = Input.GetAxis("Horizontal");
        _Vertical = Input.GetAxis("Vertical");
        _Thrusters = Input.GetAxis("Jump");
        _Brake = Input.GetKey(KeyCode.LeftControl);
        _Roll = Input.GetAxis("Roll");
    }

    private void ApplyPlayerPhysics()
    {
        if(_RB.velocity.magnitude<_MaxVelocity)
        {
            _Direction = new Vector3(_Horizontal, _Thrusters, _Vertical);
            _RB.AddForce(transform.right*_Horizontal * _Force);
            _RB.AddForce(transform.up * _Thrusters * _Force);
            _RB.AddForce(transform.forward * _Vertical * _Force);
        }
        if(_Brake)
        {
            _RB.velocity *= 0.9f;
        }
    }
}
