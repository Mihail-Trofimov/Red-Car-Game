using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    GameObject _bonus;
    void Start()
    {
        _bonus = gameObject;
    }
    void FixedUpdate()
    {
        _bonus.transform.Rotate(0, 100.0f * Time.deltaTime, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            //Debug.Log("BONUS Player");
            DestroyBonus();
        }
        else if (other.tag == "CannonBall")
        {
            Destroy(other.gameObject);
            DestroyBonus();
        }
        //else if (other.tag == "Enemy")
        //{
        //    Debug.Log("BONUS Enemy");
        //    DestroyBonus();
        //}
    }
    void DestroyBonus()
    {
        Destroy(_bonus);
        Destroy(this);
    }
}
