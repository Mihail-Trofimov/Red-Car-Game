using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone1End : MonoBehaviour
{
    [SerializeField] private Transform _lattice;
    float _pos;
    void Start()
    {
        _pos = _lattice.transform.localPosition.y;
        _lattice.transform.localPosition = new Vector3(_lattice.transform.localPosition.x, _lattice.transform.localPosition.y - 20f, _lattice.transform.localPosition.z);
    }

    IEnumerator Up()
    {

        while (_pos > _lattice.transform.localPosition.y)
        {
            _lattice.transform.localPosition = new Vector3(_lattice.transform.localPosition.x, _lattice.transform.localPosition.y + 5f * Time.deltaTime, _lattice.transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
        Destroy(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Up());
        }
    }
}
