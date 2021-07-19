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
        if (other.tag == "CannonBall")
        {
            Destroy(other.gameObject);
            Destroy(_bonus);
            Destroy(this);
        }
    }
}
