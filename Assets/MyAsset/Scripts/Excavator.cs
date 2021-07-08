using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Excavator : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _truckNMA;
    [SerializeField] private Transform _hingle1;
    [SerializeField] private Transform _hingle3;
    [SerializeField] private Transform _bucket;
    [SerializeField] private Transform _corps;
    [SerializeField] private Transform _sand;
    [SerializeField] private GameObject _notch1;
    [SerializeField] private GameObject _notch2;
    [SerializeField] private GameObject _notch3;
    private GameObject _notch;

    void Start()
    {
        _notch = _notch1;
    }

    IEnumerator LoadingIE(Collider other)
    {
        other.gameObject.GetComponent<Truck>().isLoading = true;
        float _speed = _truckNMA.speed;
        _truckNMA.speed = 0f;
        yield return StartCoroutine(HingleDown(_hingle1, 300f));
        yield return StartCoroutine(HingleDown(_hingle3, 300f));
        StartCoroutine(Notch(_notch.transform.localPosition.y));
        yield return StartCoroutine(BucketDown());
        yield return StartCoroutine(HingleUp(_hingle1, 350f));
        yield return StartCoroutine(HingleUp(_hingle3, 335f));
        yield return StartCoroutine(CorpsPlus());
        other.gameObject.GetComponent<Truck>()._sand.SetActive(true);
        yield return StartCoroutine(BucketUp());
        other.gameObject.GetComponent<Truck>().isLoaded = true;
        yield return StartCoroutine(CorpsMinus());
        other.gameObject.GetComponent<Truck>().isLoading = false;
        _truckNMA.speed = _speed;
    }
    IEnumerator Notch(float _pos)
    {
        while (_pos - 1.5f < _notch.transform.localPosition.y)
        {
            _notch.transform.localPosition = new Vector3(_notch.transform.localPosition.x, _notch.transform.localPosition.y - 0.25f * Time.deltaTime, _notch.transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
        _notch.SetActive(false);
    }
    IEnumerator HingleUp(Transform _hingle, float _angle)
    {
        while (_angle > _hingle.localRotation.eulerAngles.z)
        {
            _hingle.localRotation = Quaternion.Euler(_hingle.localRotation.eulerAngles.x, _hingle.localRotation.eulerAngles.y, _hingle.localRotation.eulerAngles.z + 20f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator HingleDown(Transform _hingle, float _angle)
    {
        while (_angle <= _hingle.localRotation.eulerAngles.z)
        {
            _hingle.localRotation = Quaternion.Euler(_hingle.localRotation.eulerAngles.x, _hingle.localRotation.eulerAngles.y, _hingle.localRotation.eulerAngles.z - 20f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

    }
    IEnumerator BucketUp()
    {
        while (_bucket.localRotation.eulerAngles.z < 59f || _bucket.localRotation.eulerAngles.z > 61f)
        {
            if (_sand.transform.localScale.x < 4.8)
            {
                _sand.transform.localScale = new Vector3(_sand.transform.localScale.x + 1f * Time.deltaTime, _sand.transform.localScale.y, _sand.transform.localScale.z);
            }
            if (_sand.transform.localScale.y < 2)
            {
                _sand.transform.localScale = new Vector3(_sand.transform.localScale.x, _sand.transform.localScale.y + 1f * Time.deltaTime, _sand.transform.localScale.z);
            }
            if (_sand.transform.localScale.z < 2.6)
            {
                _sand.transform.localScale = new Vector3(_sand.transform.localScale.x, _sand.transform.localScale.y, _sand.transform.localScale.z + 1f * Time.deltaTime);
            }
            _bucket.localRotation = Quaternion.Euler(_bucket.localRotation.eulerAngles.x, _bucket.localRotation.eulerAngles.y, _bucket.localRotation.eulerAngles.z + 20f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator BucketDown()
    {
        while (_bucket.localRotation.eulerAngles.z < 299f || _bucket.localRotation.eulerAngles.z > 301f)
        {
            _bucket.localRotation = Quaternion.Euler(_bucket.localRotation.eulerAngles.x, _bucket.localRotation.eulerAngles.y, _bucket.localRotation.eulerAngles.z - 20f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

    }

    IEnumerator CorpsPlus()
    {
        while (_corps.localRotation.eulerAngles.y < 89f || _corps.localRotation.eulerAngles.y > 90f)
        {
            _corps.localRotation = Quaternion.Euler(_corps.localRotation.eulerAngles.x, _corps.localRotation.eulerAngles.y + 30f * Time.deltaTime, _corps.localRotation.eulerAngles.z);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator CorpsMinus()
    {
        while (_corps.localRotation.eulerAngles.y < 269f || _corps.localRotation.eulerAngles.y > 271f)
        {
            _corps.localRotation = Quaternion.Euler(_corps.localRotation.eulerAngles.x, _corps.localRotation.eulerAngles.y - 40f * Time.deltaTime, _corps.localRotation.eulerAngles.z);
            yield return new WaitForFixedUpdate();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Truck" && !other.gameObject.GetComponent<Truck>().isLoaded && !other.gameObject.GetComponent<Truck>().isLoading)
        {
            if(_notch1.activeSelf)
            { 
                _notch = _notch1;
                StartCoroutine(LoadingIE(other));
            }
            else if(!_notch1.activeSelf && _notch2.activeSelf)
            { 
                _notch = _notch2;
                StartCoroutine(LoadingIE(other));
            }
            else if(!_notch1.activeSelf && !_notch2.activeSelf)
            { 
                _notch = _notch3;
                StartCoroutine(LoadingIE(other));
            }
        }
    }
}
