using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    bool isSeeYou;
    bool isDeadZone;
    Transform _player;
    Vector3 _x;
    void Start()
    {
        isSeeYou = false;
    }
    void FixedUpdate()
    {
        if (!isSeeYou || isDeadZone)
        {
            transform.Rotate(0f, 20f * Time.deltaTime, 0f);
            if (isDeadZone)
            {
                if (Vector3.Distance(transform.position, _player.position) > 20)
                {
                    isDeadZone = false;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, _player.position) > 20)
            {
                isDeadZone = false;
                _x = new Vector3(_player.position.x, transform.position.y, _player.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_x - transform.position), Time.deltaTime * 8);
            }
            else
            {
                isDeadZone = true;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                //Debug.Log("Dead Zone");
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("I SEE YOU");
            isSeeYou = true;
            _player = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("I don't SEE YOU");
            isSeeYou = false;
        }
    }
}
