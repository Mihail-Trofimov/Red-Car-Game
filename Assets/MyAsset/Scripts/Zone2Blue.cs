using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone2Blue : MonoBehaviour
{
    public bool blueBox;
    void Start()
    {
        blueBox = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zone2BlueBox")
        {
            blueBox = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zone2BlueBox")
        {
            blueBox = false;
        }
    }
}
