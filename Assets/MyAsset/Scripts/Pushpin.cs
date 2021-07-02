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
            Debug.Log("Pushpin Player");
            DestroyBonus();
        }
        else if (other.tag == "Enemy")
        {
            Debug.Log("Pushpin Enemy");
            DestroyBonus();
        }
        else if (other.tag == "CannonBall")
        {
            Destroy(other.GetComponent<GameObject>());
            DestroyBonus();
        }
    }
    void DestroyBonus()
    {
        Destroy(_pushpin);
        Destroy(this);
    }
}
