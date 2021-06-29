using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private GameObject _heal;

    void Update()
    {

        _heal.transform.Rotate(0, 2.0f, 0);

    }
}
