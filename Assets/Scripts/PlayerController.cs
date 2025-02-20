using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    private float _vInput;
    private float _hInput;

    // Update is called once per frame
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        transform.Translate(Vector3.forward* _vInput * Time.deltaTime);
        transform.Rotate(Vector3.up* _hInput * Time.deltaTime);
    }
}
