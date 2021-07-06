using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private WheelCollider[] whellCols;
    [SerializeField] private Transform[] whellMeshs;

    private float nitro;
    public int plHP;

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
        nitro = 100;
        plHP = 5;
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
                _rb.AddForce(transform.forward * 6000f * Time.deltaTime * Input.GetAxis("Vertical"));
                nitro -= 10 * Time.deltaTime;

            }
            isNitroPressed = false;
        }

        if (Input.GetAxis("Vertical") > 0 && !itsAtrap)
        {
            for (int i = 0; i < whellCols.Length; i++)
            {
                if (whellCols[i].motorTorque < 0 && _rb.velocity.magnitude > 0.1)
                {
                    isStops = true;
                    //Debug.Log("Forvard Stop " + whellCols[i].motorTorque + " " + _rb.velocity.magnitude);
                    whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 4000f * Time.deltaTime;
                }
                else
                {
                    isStops = false;
                    //Debug.Log("Forvard " + whellCols[i].motorTorque + " " + _rb.velocity.magnitude);
                    whellCols[i].brakeTorque = 0;
                    whellCols[i].motorTorque = Math.Abs(2000f * Input.GetAxis("Vertical")) * Time.deltaTime;
                }
            }
        }
        else if (Input.GetAxis("Vertical") < 0 && !itsAtrap)
        {
            for (int i = 0; i < whellCols.Length; i++)
            {
                if (whellCols[i].motorTorque > 0 && _rb.velocity.magnitude > 0.1)
                {
                    isStops = true;
                    //Debug.Log("Back Stop " + whellCols[i].motorTorque + " " + _rb.velocity.magnitude);
                    whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 4000f * Time.deltaTime;
                }
                else
                {
                    isStops = false;
                    //Debug.Log("Back " + whellCols[i].motorTorque + " " + _rb.velocity.magnitude);
                    whellCols[i].brakeTorque = 0;
                    whellCols[i].motorTorque = Mathf.Abs(1500f * Input.GetAxis("Vertical") * Time.deltaTime) * -1;
                }
            }
        }
        else
        {
            isStops = true;
            //Debug.Log("----" + " " + _rb.velocity.magnitude);
            for (int i = 0; i < whellCols.Length; i++)
            {
                whellCols[i].brakeTorque = Mathf.Abs(whellCols[i].motorTorque) * 6000f * Time.deltaTime;
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
        yield return null;
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
            Debug.Log("Cannon atack player");
            plHP -= 1;
            Debug.Log("HP " + plHP);
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