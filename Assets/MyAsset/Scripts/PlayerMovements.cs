using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private WheelCollider[] whellCols;
    [SerializeField] private Transform[] whellMeshs;

    [SerializeField] private float nitro = 100;
    [SerializeField] public int plHP = 5;

    private bool isNitroPressed;
    private bool isJumpPressed;
    private bool flying;
    private bool itsAtrap;
    private bool isStops;

    void Start()
    {
        isStops = false;
        flying = false;
        itsAtrap = false;
        _rb = GetComponent<Rigidbody>();
        
    }
    void Update()
    {

        if (Input.GetAxis("Jump") > 0)
        {
            isJumpPressed = true;
        }
        if (Input.GetAxis("Fire3") > 0)
        {
            isNitroPressed = true;
        }
    }

    void FixedUpdate()
    {
        //Debug.Log("HP " + plHP + "\nnitro " + nitro);
        //Debug.Log("motorTorque " + whellCols[0].motorTorque + " nitro" + nitro);
        //Debug.Log("_rb.velocity.magnitude " + _rb.velocity.magnitude + " nitro" + nitro);

        whellCols[0].steerAngle = 30f * Input.GetAxis("Horizontal");
        whellCols[1].steerAngle = 30f * Input.GetAxis("Horizontal");

        whellMeshs[0].rotation = Quaternion.Euler(whellMeshs[2].rotation.eulerAngles.x, whellMeshs[2].rotation.eulerAngles.y, whellMeshs[2].rotation.eulerAngles.z - 30f * Input.GetAxis("Horizontal"));
        whellMeshs[1].rotation = Quaternion.Euler(whellMeshs[2].rotation.eulerAngles.x, whellMeshs[2].rotation.eulerAngles.y, whellMeshs[2].rotation.eulerAngles.z - 30f * Input.GetAxis("Horizontal"));

        if (isJumpPressed && !flying && !itsAtrap)
        {
            _rb.AddForce(Vector3.up * 100000f * Time.deltaTime);
            _rb.MoveRotation(Quaternion.Euler(_rb.rotation.eulerAngles.x, _rb.rotation.eulerAngles.y, 0));
            isJumpPressed = false;
        }
        if (isNitroPressed && !flying && !itsAtrap && !isStops)
        {
            if (nitro > 0)
            {
                _rb.AddForce(transform.forward * 10000f * Time.deltaTime * Input.GetAxis("Vertical"));
                nitro -= 10 * Time.deltaTime;

            }
            isNitroPressed = false;
        }
        if (Input.GetAxis("Vertical") > 0 && !itsAtrap || Input.GetAxis("Vertical") < 0 && !itsAtrap && !flying)
        {
            for (int i = 0; i < whellCols.Length; i++)
            {
                    isStops = false;
                    whellCols[i].brakeTorque = 0;
                    whellCols[i].motorTorque = 50f * Input.GetAxis("Vertical") * Time.deltaTime;
                    _rb.AddForce(transform.forward * 1000f * Time.deltaTime * Input.GetAxis("Vertical"));
            }
        }
        else
        {
            isStops = true;
            for (int i = 0; i < whellCols.Length; i++)
            {
                whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 1000f;
            }
        }
    }
    IEnumerator Trap()
    {
        _rb.AddForce(Vector3.up * 50000f * Time.deltaTime);
        _rb.MoveRotation(Quaternion.Euler(_rb.rotation.eulerAngles.x, _rb.rotation.eulerAngles.y, 0));
        itsAtrap = true;
        while (_rb.velocity.magnitude>1f)
        {
            for (int i = 0; i < whellCols.Length; i++)
            {
                whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 10000f * Time.deltaTime;
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < whellCols.Length; i++)
        {
            whellCols[i].brakeTorque = 0;
        }
        flying = false;
        itsAtrap = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bonus")
        {
            Debug.Log("BONUS Player take " + nitro);
            nitro += 200;
        }
        else if (other.tag == "Heal")
        {
            //Debug.Log("Heal player take");
            plHP += 1;
            Debug.Log("HP " + plHP);
        }
        else if (other.tag == "Pushpin")
        {
            StartCoroutine(Trap());
        }
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy atack player");
            //plHP -= 1;
            _rb.GetComponent<Rigidbody>().AddForce(Vector3.up * 70f);
            _rb.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(transform.position, other.transform.position, 20f) * -100f);

            Debug.Log("HP " + plHP);
        }
        if (other.tag == "CannonBall")
        {
            Debug.Log("Cannon atack player HP " + plHP);
            plHP -= 1;
            Destroy(other.gameObject);
        } 
        //if (other.tag == "Level")
        //{
        //    //Debug.Log("Player NOT flying");
        //    flying = false;
        //}
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Level")
        {
            flying = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Level")
        {
            //Debug.Log("Player flying");
            flying = true;
        }
    }

}