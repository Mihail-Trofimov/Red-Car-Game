using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeZone : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform PointPlayer;
    [SerializeField] private Transform PointExit;
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
        Player.transform.position = PointPlayer.position;
        Player.transform.rotation = PointPlayer.rotation;
        Player.GetComponent<Rigidbody>().freezeRotation = false;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    void End()
    {
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        Player.GetComponent<Rigidbody>().freezeRotation = true;
        Player.transform.position = PointExit.position;
        Player.transform.rotation = PointExit.rotation;
        Player.GetComponent<Rigidbody>().freezeRotation = false;
        Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            End();
        }
    }
}
