using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private Transform _body;
    private GameObject _heal;
    void Start()
    {
        _heal = gameObject;
        _body = _heal.transform.Find("Body").transform;
    }
    void FixedUpdate()
    {
        _body.transform.Rotate(0, 100.0f * Time.deltaTime, 0);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            DestroyHeal();
        }
        else if (other.tag == "CannonBall")
        {
            Destroy(other.gameObject);
            DestroyHeal();
        }
    }
    void DestroyHeal()
    {
        Destroy(_heal);
        Destroy(this);
    }
}
