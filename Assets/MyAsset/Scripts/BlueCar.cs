using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BlueCar : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _loopWPSObj;
    private Transform[] _loopWPS;
    private NavMeshAgent _enemy;
    private Rigidbody _rb;
    private bool atack;
    private int currentWP;
    private Vector3 _target;

    void Start()
    {
        _loopWPS = new Transform[_loopWPSObj.childCount];
        for (int i = 0; i < _loopWPS.Length; i++)
        {
            _loopWPS[i] = _loopWPSObj.GetChild(i);
        }
        currentWP = 0;
        atack = false;
        _enemy = GetComponent<NavMeshAgent>();
        _target = _loopWPS[currentWP].position;
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) >= 30f && !atack && !_enemy.hasPath)
        {
            _enemy.speed = 8f;
            currentWP += 1;
            if (currentWP >= _loopWPS.Length)
            {
                currentWP = 0;
            }
            _target = _loopWPS[currentWP].position;
        }
        else if(Vector3.Distance(transform.position, _player.transform.position) < 30f && !atack)
        {
            _enemy.speed = 8f + 30f / Vector3.Distance(transform.position, _player.transform.position);
            _target = _player.transform.position;
        }
        _enemy.SetDestination(_target);
    }

    IEnumerator AtackIE()
    {
        atack = true;
        yield return new WaitForSeconds(8f);
        atack = false;
        yield return null;
    }
    IEnumerator NitroIE()
    {
        _enemy.speed += 20f;
        yield return new WaitForSeconds(8f);
        _enemy.speed -= 20f;
        yield return null;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!atack)
            {
                _player.GetComponent<PlayerMovements>().plHP -= 1;
                StartCoroutine(AtackIE());
            }
        }
        if (other.tag == "Bonus")
        {
            StartCoroutine(NitroIE());
        }
    }
}
