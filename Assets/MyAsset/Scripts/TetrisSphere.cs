using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSphere : MonoBehaviour
{
    void FixedUpdate()
    {
        gameObject.transform.Rotate(0f, 0f, 100.0f * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("TP act");
            gameObject.SetActive(false);
        }
    }
}
