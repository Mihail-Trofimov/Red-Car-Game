using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BlueCar : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private NavMeshAgent _enemy;

    void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _enemy.SetDestination(_player.position);
    }

    void Update()
    {



    }

    void OnTriggerEnter()
    {

    }

    void OnTriggerExit()
    {

    }
}
