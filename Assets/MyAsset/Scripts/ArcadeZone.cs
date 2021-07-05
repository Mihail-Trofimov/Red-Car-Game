using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeZone : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform PointPlayer;
    [SerializeField] private GameObject PrefabBonus;
    [SerializeField] private Transform[] PointsBonus;

    void Start()
    {
        for (int i = 0; i < PointsBonus.Length; i++)
        {
            Instantiate(PrefabBonus, PointsBonus[i].position, PointsBonus[i].rotation);
        }
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        Player.GetComponent<Rigidbody>().freezeRotation = true;

        //m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;

        Player.transform.position = PointPlayer.position;
        Player.transform.rotation = PointPlayer.rotation;

        Player.GetComponent<Rigidbody>().freezeRotation = false;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

    }
}
