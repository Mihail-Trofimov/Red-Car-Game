using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Truck : MonoBehaviour
{
    [SerializeField] private Transform _wpsObj;
    [SerializeField] private Transform _unlWPsObj;
    [SerializeField] public GameObject _sand;
    public bool isLoaded;
    public bool isUnloading;

    private Transform[] _pathWPs;
    private Transform[] _unlWPs;
    private NavMeshAgent _trackNav;
    private int currentWp;
    private Transform[] _target;

    void Start()
    {
        isUnloading = false;
        isLoaded = false;
        ChildGet(_wpsObj, ref _pathWPs);
        ChildGet(_unlWPsObj, ref _unlWPs);
        _target = _pathWPs;
        currentWp = 0;
        _trackNav = GetComponent<NavMeshAgent>();
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
        if (!_trackNav.hasPath && !isUnloading)
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
            }
            else
            {
                _trackNav.speed = 0f;
            }
        }
    }
}