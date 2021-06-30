using UnityEngine;

//эта строчка гарантирует что наш скрипт не завалитс€ 
//ести на плеере будет отсутствовать компонент Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 300f;

    //что бы эта переменна€ работала добавьте тэг "Ground" на вашу поверхность земли
    private bool _isGrounded;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // обратите внимание что все действи€ с физикой 
    // необходимо обрабатывать в FixedUpdate, а не в Update
    void FixedUpdate()
    {
        _rb.AddForce(0.0f, 0.0f, Input.GetAxis("Vertical") * Speed);

        //Quaternion.RotateTowards(rb.rotation, rotX30, 10.0f * Time.deltaTime)
        //_rb.MoveRotation(Quaternion.RotateTowards(Input.GetAxis("Horizontal") * Speed, 0.0f, 0.0f));

        //_rb.MoveRotation(Quaternion.RotateTowards(_rb.rotation, _rb.rotation, _rb.rotation));
        var angles = _rb.transform.rotation.eulerAngles;
        angles.z += Input.GetAxis("Horizontal") * Speed;
        //transform.rotation = Quaternion.Euler(angles);

        _rb.MoveRotation(Quaternion.Euler(angles));

        //MovementLogic();
        //JumpLogic();
    }

    private void MovementLogic()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");

        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        _rb.AddForce(Input.GetAxis("Horizontal") * Speed, 0.0f, Input.GetAxis("Vertical") * Speed );
        //_rb.AddForce(movement * Speed);
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * JumpForce);

                // ќбратите внимание что € делаю на основе Vector3.up 
                // а не на основе transform.up. ≈сли персонаж упал или 
                // если персонаж -- шар, то его личный "верх" может 
                // любое направление. ¬лево, вправо, вниз...
                // Ќо нам нужен скачек только в абсолютный вверх, 
                // потому и Vector3.up
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
}




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
