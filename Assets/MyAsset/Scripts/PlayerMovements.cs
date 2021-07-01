using System;
using UnityEngine;

//эта строчка гарантирует что наш скрипт не завалится 
//ести на плеере будет отсутствовать компонент Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    //public float Speed = 10f;
    //public float JumpForce = 300f;

    ////что бы эта переменная работала добавьте тэг "Ground" на вашу поверхность земли
    //private bool _isGrounded;
    private Rigidbody _rb;

    [Header("Assigned")]
    [SerializeField] private WheelCollider[] whellCols;
    //[SerializeField] private GameObject[] whells;
    [SerializeField] private Transform[] whellMeshs;

    
    //void whellSteer(float angel)
    //{
    //    whellCols[0].steerAngle = angel;
    //    whellCols[1].steerAngle = angel;
    //}
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //whellCols[0].steerAngle = 30f * Input.GetAxis("Horizontal");
        //whellCols[1].steerAngle = 30f * Input.GetAxis("Horizontal");
        //whellSteer(30f * Input.GetAxis("Horizontal"));

        whellCols[0].steerAngle = 30f * Input.GetAxis("Horizontal");
        whellCols[1].steerAngle = 30f * Input.GetAxis("Horizontal");

        //whellMeshs[0].rotation = Quaternion.Euler(90f, 0f, 30f * Input.GetAxis("Horizontal"));
        //whellMeshs[1].rotation = Quaternion.Euler(90f, 0f, 30f * Input.GetAxis("Horizontal"));

        //whellMeshs[0].rotation = Quaternion.Euler(90f, 0f, 0f);
        //whellMeshs[1].rotation = Quaternion.Euler(90f, 0f, 0f);



        //whellMeshs[0].rotation = Quaternion.Euler(90f, 0f, 90f - whellCols[0].steerAngle);
        //whellMeshs[1].rotation = Quaternion.Euler(90f, 0f, 90f - whellCols[1].steerAngle);



        //Debug.Log(_rb.velocity.magnitude.ToString());
        


        if (Input.GetAxis("Vertical") > 0)
        {
            if (whellCols[0].motorTorque < 0 || whellCols[1].motorTorque < 0 || whellCols[2].motorTorque < 0 || whellCols[3].motorTorque < 0)
            {
                Debug.Log("Forvard Stop");
                whellCols[0].brakeTorque = 1000f * Time.deltaTime;
                whellCols[1].brakeTorque = 1000f * Time.deltaTime;
                whellCols[2].brakeTorque = 1000f * Time.deltaTime;
                whellCols[3].brakeTorque = 1000f * Time.deltaTime;
            }
            else
            {
                Debug.Log("Forvard");
                whellCols[0].motorTorque = Math.Abs(600f * Input.GetAxis("Vertical")) * Time.deltaTime;
                whellCols[1].motorTorque = Math.Abs(600f * Input.GetAxis("Vertical")) * Time.deltaTime;
                whellCols[2].motorTorque = Math.Abs(600f * Input.GetAxis("Vertical")) * Time.deltaTime;
                whellCols[3].motorTorque = Math.Abs(600f * Input.GetAxis("Vertical")) * Time.deltaTime;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (whellCols[0].motorTorque > 0 || whellCols[1].motorTorque > 0 || whellCols[2].motorTorque > 0 || whellCols[3].motorTorque > 0)
            {
                Debug.Log("Back Stop");
                whellCols[0].brakeTorque = 1000f * Time.deltaTime;
                whellCols[1].brakeTorque = 1000f * Time.deltaTime;
                whellCols[2].brakeTorque = 1000f * Time.deltaTime;
                whellCols[3].brakeTorque = 1000f * Time.deltaTime;
            }
            else
            {
                Debug.Log("Back");
                whellCols[0].motorTorque = -1 * Math.Abs(500f * Input.GetAxis("Vertical") * Time.deltaTime);
                whellCols[1].motorTorque = -1 * Math.Abs(500f * Input.GetAxis("Vertical") * Time.deltaTime);
                whellCols[2].motorTorque = -1 * Math.Abs(500f * Input.GetAxis("Vertical") * Time.deltaTime);
                whellCols[3].motorTorque = -1 * Math.Abs(500f * Input.GetAxis("Vertical") * Time.deltaTime);
            }
        }
        else
        {
            Debug.Log(_rb.velocity.magnitude.ToString());
            Debug.Log("----");
            //whellCols[0].brakeTorque = 1f * Time.deltaTime;
            //whellCols[1].brakeTorque = 1f * Time.deltaTime;
            //whellCols[2].brakeTorque = 1f * Time.deltaTime;
            //whellCols[3].brakeTorque = 1f * Time.deltaTime;
        }
        //else
        //{

        //if (Input.GetAxis("Vertical") < 0)
        //{
        //    Debug.Log("Revers");
        //    //whellCols[0].motorTorque = 5f * Input.GetAxis("Vertical");
        //    //whellCols[1].motorTorque = 5f * Input.GetAxis("Vertical");
        //    whellCols[2].motorTorque = -15f * Input.GetAxis("Vertical");
        //    whellCols[3].motorTorque = -15f * Input.GetAxis("Vertical");
        //}
        //else
        //{
        //Debug.Log("Stop");
        //whellCols[0].motorTorque = 0f;
        //whellCols[1].motorTorque = 0f;
        //whellCols[2].motorTorque = 0f;
        //whellCols[3].motorTorque = 0f;

        //whellCols[0].brakeTorque = 15f;
        //whellCols[1].brakeTorque = 15f;
        //whellCols[2].brakeTorque = 15f;
        //whellCols[3].brakeTorque = 15f;
        //}

        //}

    }

    



}
//_rb.AddForce(0.0f, 0.0f, Input.GetAxis("Vertical") * Speed);

////Quaternion.RotateTowards(rb.rotation, rotX30, 10.0f * Time.deltaTime)
////_rb.MoveRotation(Quaternion.RotateTowards(Input.GetAxis("Horizontal") * Speed, 0.0f, 0.0f));

////_rb.MoveRotation(Quaternion.RotateTowards(_rb.rotation, _rb.rotation, _rb.rotation));
//var angles = _rb.transform.rotation.eulerAngles;
//angles.z += Input.GetAxis("Horizontal") * Speed;
////transform.rotation = Quaternion.Euler(angles);

//_rb.MoveRotation(Quaternion.Euler(angles));

////MovementLogic();
////JumpLogic();
///


//    private void MovementLogic()
//    {
//        //float moveHorizontal = Input.GetAxis("Horizontal");

//        //float moveVertical = Input.GetAxis("Vertical");

//        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

//        _rb.AddForce(Input.GetAxis("Horizontal") * Speed, 0.0f, Input.GetAxis("Vertical") * Speed );
//        //_rb.AddForce(movement * Speed);
//    }

//    private void JumpLogic()
//    {
//        if (Input.GetAxis("Jump") > 0)
//        {
//            if (_isGrounded)
//            {
//                _rb.AddForce(Vector3.up * JumpForce);

//                // Обратите внимание что я делаю на основе Vector3.up 
//                // а не на основе transform.up. Если персонаж упал или 
//                // если персонаж -- шар, то его личный "верх" может 
//                // любое направление. Влево, вправо, вниз...
//                // Но нам нужен скачек только в абсолютный вверх, 
//                // потому и Vector3.up
//            }
//        }
//    }

//    void OnCollisionEnter(Collision collision)
//    {
//        IsGroundedUpate(collision, true);
//    }

//    void OnCollisionExit(Collision collision)
//    {
//        IsGroundedUpate(collision, false);
//    }

//    private void IsGroundedUpate(Collision collision, bool value)
//    {
//        if (collision.gameObject.tag == ("Ground"))
//        {
//            _isGrounded = value;
//        }
//    }
//}




////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class PlayerMovements : MonoBehaviour
////{
////    [SerializeField] private float _speed = 5f;
////    //[SerializeField] private float _speedRotation = 5f;

////    private Vector3 _direction;

////    //public int count = 0;
////    //float x;
////    //float z;

////    private void Awake()
////    {
////        _direction = Vector3.zero;
////    }

////    void Update()
////    {
////        _direction.x = Input.GetAxis("Horizontal");
////        _direction.z = Input.GetAxis("Vertical");
////        //x = Input.GetAxis("Horizontal");
////        //z = Input.GetAxis("Vertical");

////    }

////    private void FixedUpdate()
////    {

////        Rigidbody.AddForce((_direction.x * _speed), 0, (_direction.z * _speed), ForceMode.Force);

////        //Rigidbody.AddForce(x, 0, z, ForceMode.VelocityChange);

////        //var speed = _direction.normalized * _speed * Time.fixedDeltaTime;
////        //transform.Translate(speed);
////        //if (Input.GetKey(Input.GetAxis("Vertical")))
////        //{
////        //    ForwardMove(_speed, _speedRotation);
////        //}

////        //GetComponent<Rigidbody>().velocity = new Vector3(x, y);

////        //var speed = _direction * _speed * Time.fixedDeltaTime;
////        //transform.Translate(speed);
////        ////transform.Rotate(Vector3.up * _speedRotation * Input.GetAxis("Mouse X") * Time.deltaTime);
////        //transform.Rotate(Vector3.up * _speedRotation * Input.GetAxis("Horizontal") * Time.deltaTime);
////    }


////    //void ForwardMove(float _Speed, float _Turn)
////    //{
////    //    if (Time.timeScale != 0)
////    //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y + _Turn, 0), 0.2f);
////    //    transform.position += transform.forward * _Speed * Time.deltaTime;
////    //}


////}
