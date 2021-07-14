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
    [SerializeField] private Truck truckScript;
    [SerializeField] private GameObject _endGame;

    private Transform[] _pathWPs;
    private NavMeshAgent _bulldNav;
    private bool _playerIn;
    private bool _done;

    private int _currentWp;

    void Start()
    {
        _pathWPs = new Transform[_wpsObj.childCount];
        for (int i = 0; i < _pathWPs.Length; i++)
        {
            _pathWPs[i] = _wpsObj.GetChild(i);
        }
        _playerIn = false;
        _done = false;
        _currentWp = 0;
        _bulldNav = GetComponent<NavMeshAgent>();
        _bulldNav.SetDestination(_pathWPs[0].position);
        StartCoroutine(Work());
    }




    IEnumerator Work()
    {
        while (this != null && !_done)
        {
            yield return new WaitUntil(() => playerScript.beepBeep && _playerIn && _heap.activeSelf && !truckScript.isUnloading || _done);
            Debug.Log("Work начало");
            if (!_done)
            {
                playerScript.beepBeep = false;
                Debug.Log("Work BeepBeep");
                StartCoroutine(Embankment());
                _currentWp = -1;
                bool _flag = false;
                while (!_flag)
                {
                    yield return new WaitUntil(() => !_bulldNav.hasPath);
                    _currentWp += 1;
                    if (_currentWp >= _pathWPs.Length)
                    {
                        _flag = true;
                        _currentWp = 0;
                        _pathWPs[_pathWPs.Length-1].transform.position = new Vector3(_pathWPs[_pathWPs.Length - 1].transform.position.x - 4f, _pathWPs[_pathWPs.Length - 1].transform.position.y, _pathWPs[_pathWPs.Length - 1].transform.position.z);
                    }
                    _bulldNav.SetDestination(_pathWPs[_currentWp].position);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        Debug.Log("Work конец");
        _endGame.SetActive(true);
    }

    IEnumerator Embankment()
    {
        yield return new WaitUntil(() => Vector3.Distance(transform.position, _heap.transform.position) < 4);
        StartCoroutine(Heap());
        if (!_mound1.activeSelf && !_mound2.activeSelf && !_mound3.activeSelf)
        {
            StartCoroutine(Mound(_mound3));
            Debug.Log("Work Mound 3");
        }
        else if (!_mound1.activeSelf && !_mound2.activeSelf && _mound3.activeSelf)
        {
            StartCoroutine(Mound(_mound2));
            Debug.Log("Work Mound 2");
        }
        else if (!_mound1.activeSelf && _mound2.activeSelf && _mound3.activeSelf)
        {
            StartCoroutine(Mound(_mound1));
            _done = true;
            Debug.Log("Work Mound 1");
        }
    }

    IEnumerator Heap()
    {
        Debug.Log("Work Heap");
        while (_heap.transform.localScale.z > 0 || _heap.transform.localScale.y > 0 || _heap.transform.localScale.x > 0)
        {
            if (_heap.transform.localScale.x > 0)
            {
                _heap.transform.localScale = new Vector3(_heap.transform.localScale.x - 2.5f * Time.deltaTime, _heap.transform.localScale.y, _heap.transform.localScale.z);
            }
            if (_heap.transform.localScale.y > 0)
            {
                _heap.transform.localScale = new Vector3(_heap.transform.localScale.x, _heap.transform.localScale.y - 2.5f * Time.deltaTime, _heap.transform.localScale.z);
            }
            if (_heap.transform.localScale.z > 0)
            {
                _heap.transform.localScale = new Vector3(_heap.transform.localScale.x, _heap.transform.localScale.y, _heap.transform.localScale.z - 2.5f * Time.deltaTime);
            }
            yield return new WaitForFixedUpdate();
        }
        _heap.SetActive(false);
    }
    IEnumerator Mound(GameObject _mound)
    {
        _mound.SetActive(true);
        float _pos = _mound.transform.position.y;
        _mound.transform.position = new Vector3(_mound.transform.position.x, _mound.transform.position.y - 2f, _mound.transform.position.z);
        while (_pos >= _mound.transform.position.y)
        {
            _mound.transform.position = new Vector3(_mound.transform.position.x, _mound.transform.position.y + 0.25f * Time.deltaTime, _mound.transform.position.z);
            yield return new WaitForFixedUpdate();
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
