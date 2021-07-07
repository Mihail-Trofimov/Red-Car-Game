using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Excavator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private NavMeshAgent _truckNMA;
    //private Collider _coll;
    //void Start()
    //{
    //    Debug.Log("Start");
    //    _coll = GetComponent<Collider>();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    IEnumerator LoadingIE(Collider other)
    {
        float _speed = _truckNMA.speed;
        _truckNMA.speed = 0f;
        yield return new WaitForSeconds(8f);
        other.gameObject.GetComponent<Truck>().isLoaded = true;
        other.gameObject.GetComponent<Truck>()._sand.SetActive(true);
        _truckNMA.speed = _speed;
        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Truck" && !other.gameObject.GetComponent<Truck>().isLoaded)
        {
            
            StartCoroutine(LoadingIE(other));
            //_coll.isTrigger = false;

        }
    }
}
