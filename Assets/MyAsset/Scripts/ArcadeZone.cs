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
    private Rigidbody _plRb;

    void Start()
    {
        _plRb = Player.GetComponent<Rigidbody>();
        for (int i = 0; i < PointsBonus.Length; i++)
        {
            Instantiate(PrefabBonus, PointsBonus[i].position, PointsBonus[i].rotation);
        }
        _plRb.constraints = RigidbodyConstraints.FreezePosition;
        _plRb.freezeRotation = true;
        Player.transform.position = PointPlayer.position;
        Player.transform.rotation = PointPlayer.rotation;
        _plRb.freezeRotation = false;
        _plRb.constraints = RigidbodyConstraints.None;
    }

    void End()
    {
        _plRb.constraints = RigidbodyConstraints.FreezePosition;
        _plRb.freezeRotation = true;
        Player.transform.position = PointExit.position;
        Player.transform.rotation = PointExit.rotation;
        _plRb.freezeRotation = false;
        _plRb.constraints = RigidbodyConstraints.None;
        _plRb.AddForce(Vector3.up * 100000f * Time.deltaTime);
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
