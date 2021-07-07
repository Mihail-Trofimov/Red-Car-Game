using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mound : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _truckNMA;
    [SerializeField] private GameObject _mound_1;
    [SerializeField] private GameObject _mound_2;
    [SerializeField] private GameObject _mound_3;
    [SerializeField] private GameObject _heap;
    [SerializeField] private GameObject _sand;
    [SerializeField] private Transform _dump;
    private bool _unlDump;
    private bool _dumpUp;
    private bool _dumpDown;
    private bool _pour;
    private float _o;
    void Start()
    {
        _pour = false;
        _unlDump = false;
        _dumpUp = false;
        _o = _dump.rotation.eulerAngles.z;
        _heap.SetActive(false);
        _mound_1.SetActive(false);
        _mound_2.SetActive(false);
        _mound_3.SetActive(false);
    }

    void Update()
    {
        if (_unlDump)
        {
            if (_dumpUp && !_dumpDown && !_pour)
            {
                if ((_o + 22.5f) > _dump.localRotation.y)
                {
                    _dump.localRotation = Quaternion.Euler(_dump.localRotation.eulerAngles.x, _dump.localRotation.eulerAngles.y, _dump.localRotation.eulerAngles.z + 10f * Time.deltaTime);
                }
                else
                {
                    _dumpUp = false;
                }
            }
            else if (!_dumpUp && !_dumpDown && _pour)
            {
                if (_sand.transform.localScale.x > 0)
                {
                    _sand.transform.localScale = new Vector3(_sand.transform.localScale.x - 1f * Time.deltaTime, _sand.transform.localScale.y, _sand.transform.localScale.z);
                }
                if (_sand.transform.localScale.y > 0)
                {
                    _sand.transform.localScale = new Vector3(_sand.transform.localScale.x, _sand.transform.localScale.y - 1f * Time.deltaTime, _sand.transform.localScale.z);
                }
                if (_sand.transform.localScale.z > 0)
                {
                    _sand.transform.localScale = new Vector3(_sand.transform.localScale.x, _sand.transform.localScale.y, _sand.transform.localScale.z - 1f * Time.deltaTime);
                }

                if (_heap.transform.localScale.x < 5)
                {
                    _heap.transform.localScale = new Vector3(_heap.transform.localScale.x - 1f * Time.deltaTime, _heap.transform.localScale.y, _heap.transform.localScale.z);
                }
                if (_heap.transform.localScale.y < 4)
                {
                    _heap.transform.localScale = new Vector3(_heap.transform.localScale.x, _heap.transform.localScale.y - 1f * Time.deltaTime, _heap.transform.localScale.z);
                }
                if (_heap.transform.localScale.z < 5)
                {
                    _heap.transform.localScale = new Vector3(_heap.transform.localScale.x, _heap.transform.localScale.y, _heap.transform.localScale.z - 1f * Time.deltaTime);
                }

                if ((_heap.transform.localScale.z >= 5) && (_heap.transform.localScale.y >= 4) && (_heap.transform.localScale.x >= 5) && (_sand.transform.localScale.z <= 0) && (_sand.transform.localScale.y <= 0) && (_sand.transform.localScale.x <= 0))
                {
                    _pour = false;
                }
            }
            else if (!_dumpUp && _dumpDown && !_pour)
            {
                if (_o <= _dump.localRotation.y)
                {
                    _dump.localRotation = Quaternion.Euler(_dump.localRotation.eulerAngles.x, _dump.localRotation.eulerAngles.y, _dump.localRotation.eulerAngles.z - 10f * Time.deltaTime);
                }
                else
                {
                    _dumpDown = false;
                }
            }
        }
    }
    IEnumerator LoadingIE(Collider other)
    {
        while(!other.gameObject.GetComponent<Truck>().isUnloading)
        {
            yield return new WaitForSeconds(0.5f);
        }
        _heap.SetActive(true);
        _heap.transform.localScale = new Vector3(0f, 0f, 0f);
        float _speed = _truckNMA.speed;
        _truckNMA.speed = 0f;
        _unlDump = true;
        _dumpUp = true;
        _dumpDown = false;
        _pour = false;
        while (_dumpUp)
        {
            yield return new WaitForSeconds(0.1f);
        }
        _pour = true;
        while (_pour)
        {
            yield return new WaitForSeconds(0.1f);
        }
        _dumpDown = true;
        while (_dumpDown)
        {
            yield return new WaitForSeconds(0.1f);
        }
        _truckNMA.speed = _speed;
        other.gameObject.GetComponent<Truck>().isLoaded = false;
        other.gameObject.GetComponent<Truck>()._sand.SetActive(false);
        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Truck" && other.gameObject.GetComponent<Truck>().isLoaded && !_heap.activeSelf)
        {
            StartCoroutine(LoadingIE(other));
        }
    }
}
