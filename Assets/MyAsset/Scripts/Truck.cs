using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Rigidbody))]
public class Truck : MonoBehaviour
{
    [SerializeField] private Transform[] _wps;
    //private Rigidbody _trackRb;
    private NavMeshAgent _trackNav;
    private int currentWp;


    void Start()
    {
        
        currentWp = 0;
        _trackNav = GetComponent<NavMeshAgent>();
        //_trackRb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        if (!_trackNav.hasPath)
        {
            currentWp += 1;
            if (currentWp >= _wps.Length)
            {
                currentWp = 0;
            }
            _trackNav.SetDestination(_wps[currentWp].position);
        }

        //if (Vector3.Distance(transform.position, _wps[currentWp].position) < 8)
        //{
        //    currentWp += 1;
        //    if (currentWp > _wps.Length)
        //    {
        //        currentWp = 0;
        //    }
        //}
        //else
        //{
        //    Move();
        //}
    }


    //void Move()
    //{
    //    transform.LookAt(_wps[currentWp].position);

    //    Vector3 direction = (_wps[currentWp].position - transform.position).normalized;
    //    _trackRb.MovePosition(transform.position + direction * 5f * Time.deltaTime);
    //}

}