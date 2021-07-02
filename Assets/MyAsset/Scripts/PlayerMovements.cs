using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private WheelCollider[] whellCols;
    [SerializeField] private Transform[] whellMeshs;

    bool isJumpPressed;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {

        if (Input.GetAxis("Jump") > 0)
        {
            isJumpPressed = true;
            Debug.Log("Update Jump");
        }
    }
    void FixedUpdate()
    {
        whellCols[0].steerAngle = 30f * Input.GetAxis("Horizontal");
        whellCols[1].steerAngle = 30f * Input.GetAxis("Horizontal");

        whellMeshs[0].rotation = Quaternion.Euler(whellMeshs[2].rotation.eulerAngles.x, whellMeshs[2].rotation.eulerAngles.y, whellMeshs[2].rotation.eulerAngles.z - 30f * Input.GetAxis("Horizontal"));
        whellMeshs[1].rotation = Quaternion.Euler(whellMeshs[2].rotation.eulerAngles.x, whellMeshs[2].rotation.eulerAngles.y, whellMeshs[2].rotation.eulerAngles.z - 30f * Input.GetAxis("Horizontal"));


        if (isJumpPressed)
        {
            Debug.Log("FixedUpdate Jump");
            _rb.AddForce(Vector3.up * 2000f);
            _rb.MoveRotation(Quaternion.Euler(_rb.rotation.eulerAngles.x, _rb.rotation.eulerAngles.y, 0));
            isJumpPressed = false;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            for (int i = 0; i < whellCols.Length; i++)
            {
                if (whellCols[i].motorTorque < 0 && _rb.velocity.magnitude > 1)
                {
                    //Debug.Log("Forvard Stop " + whellCols[i].motorTorque + " " + _rb.velocity.magnitude);
                    whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 4000f * Time.deltaTime;
                }
                else
                {
                    //Debug.Log("Forvard " + whellCols[i].motorTorque + " " + _rb.velocity.magnitude);
                    whellCols[i].brakeTorque = 0;
                    whellCols[i].motorTorque = Math.Abs(2000f * Input.GetAxis("Vertical")) * Time.deltaTime;
                }
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            for (int i = 0; i < whellCols.Length; i++)
            {
                if (whellCols[i].motorTorque > 1 && _rb.velocity.magnitude > 1)
                {
                    //Debug.Log("Back Stop " + whellCols[i].motorTorque + " " + _rb.velocity.magnitude);
                    whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 4000f * Time.deltaTime;
                }
                else
                {
                    //Debug.Log("Back " + whellCols[i].motorTorque + " " + _rb.velocity.magnitude);
                    whellCols[i].brakeTorque = 0;
                    whellCols[i].motorTorque = Mathf.Abs(1500f * Input.GetAxis("Vertical") * Time.deltaTime) * -1;
                }
            }
        }
        else
        {
            //Debug.Log("----" + " " + _rb.velocity.magnitude);
            for (int i = 0; i < whellCols.Length; i++)
            {
                whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 4000f * Time.deltaTime;
            }
        }

    }
}