using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovements : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private WheelCollider[] whellCols;
    [SerializeField] private Transform[] whellMeshs;

    public int plHP = 5;
    public float nitro = 100;
    public bool beepBeep;

    private bool _beep;
    private bool beepDown;
    private bool isNitroPressed;
    private bool isJumpPressed;
    private bool flying;
    private bool itsAtrap;
    private bool isStops;

    private bool inBonus;
    private bool inHeal;
    private bool inPushpin;
    private bool inEnemy;
    private bool inCannonBall;

    void Start()
    {
        inBonus = false;
        inHeal = false;
        inPushpin = false;
        inEnemy = false;
        inCannonBall = false;

        _beep = false;
        beepBeep = false;
        beepDown = false;

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
        if (Input.GetMouseButtonDown(0))
        {
            beepDown = true;
            if (!_beep)
            {
                StartCoroutine(BeepBeep());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            beepDown = false;
        }
    }




    void FixedUpdate()
    {
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
    IEnumerator BeepBeep()
    {
        Debug.Log("beepBeep начинается");
        _beep = true;
        int _time = 0;
        while (beepDown && _time <= 150)
        {
            _time += 1;
            yield return new WaitForSeconds(0.01f);
        }
        if (_time < 40 || _time > 150)
        {
            Debug.Log("beepBeep time break 1: " + _time);
            _beep = false;
            yield break;
        }
        Debug.Log("beepBeep продолжается 1");
        _time = 0;
        while (!beepDown && _time <= 60)
        {
            _time += 1;
            yield return new WaitForSeconds(0.01f);
        }
        if (_time < 5 || _time > 60)
        {
            Debug.Log("beepBeep time break 2: " + _time);
            _beep = false;
            yield break;
        }
        Debug.Log("beepBeep продолжается 2");
        _time = 0;
        while (beepDown && _time <= 150)
        {
            _time += 1;
            yield return new WaitForSeconds(0.01f);
        }
        if (_time < 40 || _time > 150)
        {
            Debug.Log("beepBeep time break 3: " + _time);
            _beep = false;
            yield break;
        }
        beepBeep = true;
        Debug.Log("beepBeep успешно");
        yield return new WaitForSeconds(1f);
        if (beepBeep)
        {
            beepBeep = false;
            Debug.Log("beepBeep 2: " + beepBeep);
        }
        _beep = false;
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
    IEnumerator boolBonus()
    {
        yield return new WaitForFixedUpdate();
        inBonus = false;
    }
    IEnumerator boolHeal()
    {
        yield return new WaitForFixedUpdate();
        inHeal = false;
    }
    IEnumerator boolPushpin()
    {
        yield return new WaitForFixedUpdate();
        inPushpin = false;
    }
    IEnumerator boolEnemy()
    {
        yield return new WaitForFixedUpdate();
        inEnemy = false;
    }
    IEnumerator boolCannonBall()
    {
        yield return new WaitForFixedUpdate();
        inCannonBall = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bonus" && !inBonus)
        {
            inBonus = true;
            Destroy(other.gameObject);
            nitro += 200;
            StartCoroutine(boolBonus());
        }
        else if (other.tag == "Heal" && !inHeal)
        {
            inHeal = true;
            Destroy(other.gameObject);
            plHP++;
            StartCoroutine(boolHeal());
        }
        else if (other.tag == "Pushpin" && !inPushpin)
        {
            inPushpin = true;
            Destroy(other.gameObject);
            StartCoroutine(Trap());
            StartCoroutine(boolPushpin());
        }
        if (other.tag == "Enemy" && !inEnemy)
        {
            inEnemy = true;
            _rb.GetComponent<Rigidbody>().AddForce(Vector3.up * 70f);
            _rb.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(transform.position, other.transform.position, 20f) * -100f);
            StartCoroutine(boolEnemy());
        }
        if (other.tag == "CannonBall" && !inCannonBall)
        {
            inCannonBall = true;
            Destroy(other.gameObject);
            plHP--;
            StartCoroutine(boolCannonBall());
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