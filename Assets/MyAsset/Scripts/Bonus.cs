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
        if (other.tag == "Player")
        {
            Debug.Log("BONUS Player");
            DestroyBonus();
        }
        else if (other.tag == "Enemy")
        {
            Debug.Log("BONUS Enemy");
            other.GetComponent<Rigidbody>().AddForce(Vector3.forward * 1000f);
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
        Destroy(_bonus);
        Destroy(this);
    }
}
