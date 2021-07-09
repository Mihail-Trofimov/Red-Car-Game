using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Truck : MonoBehaviour
{
    [SerializeField] private Transform _wpsObj;
    [SerializeField] private Transform _unlWPsObj;
    [SerializeField] public GameObject _sand;
    [SerializeField] public GameObject _heap;
    [SerializeField] private GameObject _notch1;
    [SerializeField] private GameObject _notch2;
    [SerializeField] private GameObject _notch3;

    public bool isLoaded;
    public bool isLoading;
    public bool isUnloading;

    private Transform[] _pathWPs;
    private Transform[] _unlWPs;
    private NavMeshAgent _trackNav;
    private int currentWp;
    private Transform[] _target;
    private bool _truckMove;

    void Start()
    {
        _truckMove = false;
        isUnloading = false;
        isLoaded = false;
        isLoading = false;
        ChildGet(_wpsObj, ref _pathWPs);
        ChildGet(_unlWPsObj, ref _unlWPs);
        _target = _pathWPs;
        currentWp = 0;
        _trackNav = GetComponent<NavMeshAgent>();
        _sand.transform.localScale = new Vector3(0f, 0f, 0f);
        _sand.SetActive(false);
        
    }
    void ChildGet(Transform _obj, ref Transform[] _wps)
    {
        _wps = new Transform[_obj.childCount];
        for (int i = 0; i < _wps.Length; i++)
        {
            _wps[i] = _obj.GetChild(i);
        }
    }
    void Update()
    {
        if (!_trackNav.hasPath && !isUnloading && _truckMove)
        {
            currentWp += 1;
            if (currentWp >= _target.Length && !isLoaded)
            {
                currentWp = 0;
                _target = _pathWPs;
                _trackNav.updateRotation = true;
            }
            else if (_target != _unlWPs && currentWp >= _target.Length && isLoaded)
            {
                currentWp = 0;
                _target = _unlWPs;
            }
            else if (_target == _unlWPs && currentWp == 1 && isLoaded)
            {
                _trackNav.updateRotation = false;
            }
            else if (_target == _unlWPs && currentWp >= _target.Length && isLoaded)
            {
                isUnloading = true;
            }

            if (currentWp < _target.Length)
            {
                _trackNav.speed = 3.5f;
                _trackNav.SetDestination(_target[currentWp].position);
                if (currentWp == 1 && _target == _pathWPs)
                {
                    if (_heap.activeSelf || !_notch1.activeSelf && !_notch2.activeSelf && !_notch3.activeSelf)
                    {
                        _truckMove = false;
                        _trackNav.speed = 0f;
                    }
                }
            }
            else
            {
                _trackNav.speed = 0f;
            }
        }
        else if(_trackNav.hasPath && _trackNav.speed < 1f && _truckMove && !isLoading && !isUnloading)
        {
            _trackNav.speed = 3.5f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CannonBall")
        {
            Destroy(other.gameObject);
            if (!_heap.activeSelf)
            {
                if (_notch1.activeSelf || _notch2.activeSelf || _notch3.activeSelf)
                {
                    _truckMove = true;

                }
            }
        }
    }
}