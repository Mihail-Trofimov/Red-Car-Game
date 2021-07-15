using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone2Green : MonoBehaviour
{
    public bool greenBox;
    void Start()
    {
        greenBox = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zone2GreenBox")
        {
            greenBox = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zone2GreenBox")
        {
            greenBox = false;
        }
    }
}

