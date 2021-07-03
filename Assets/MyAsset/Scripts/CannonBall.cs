using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level")
        {
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
