using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushpin : MonoBehaviour
{
    GameObject _pushpin;
    void Start()
    {
        _pushpin = gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DestroyPushpin();
        }
        else if (other.tag == "Enemy")
        {
            Debug.Log("Pushpin Enemy");
            DestroyPushpin();
        }
        else if (other.tag == "CannonBall")
        {
            Destroy(other.gameObject);
            DestroyPushpin();
        }
    }
    void DestroyPushpin()
    {
        Destroy(_pushpin);
        Destroy(this);
    }
}
