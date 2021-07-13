using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bulldozer : MonoBehaviour
{
    [SerializeField] public GameObject _heap;
    [SerializeField] private GameObject _mound1;
    [SerializeField] private GameObject _mound2;
    [SerializeField] private GameObject _mound3;
    [SerializeField] private Transform _wpsObj;
    [SerializeField] private PlayerMovements playerScript;

    private Transform[] _pathWPs;
    private NavMeshAgent _bulldNav;
    private bool _playerIn;

    void Start()
    {
        _pathWPs = new Transform[_wpsObj.childCount];
        for (int i = 0; i < _pathWPs.Length; i++)
        {
            _pathWPs[i] = _wpsObj.GetChild(i);
        }
        _playerIn = false;
        _bulldNav = GetComponent<NavMeshAgent>();
        StartCoroutine(BeepBeep());
    }




    IEnumerator BeepBeep()
    {
        //не забудь добавить в условие!
        while (this != null)
        {
            yield return new WaitUntil(() => playerScript.beepBeep && _playerIn);
            playerScript.beepBeep = false;

            Debug.Log("Bulldozer BeepBeep");

            if (!_bulldNav.hasPath && _heap.activeSelf)
            {
                bool _flag = false;
                while (!_flag)
                {


                    if ()
                    {
                        _flag = true;
                    }
                }
            }
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIn = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIn = false;
        }
    }
}
