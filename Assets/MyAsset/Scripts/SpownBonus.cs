using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    [SerializeField] private int _objMaxCount = 3;
    [SerializeField] private GameObject[] _prefabs;
    Transform[] _spawnPoints;
    Dictionary<GameObject, int> _spawnObjects;
    void Start()
    {
        _spawnObjects = new Dictionary<GameObject, int>();
        _spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i] = transform.GetChild(i);
        }
        //if (_spawnObjects.Count < _objMaxCount)
        for (int i = 0; i < _objMaxCount; i++)
        {
            Spawn(_prefabs[UnityEngine.Random.Range(0, _prefabs.Length)]);
        }
    }
    void Update()
    {
        //Будет задержка на UpdateList
        UpdateList();

    }
    void Spawn(GameObject _prefab)
    {
        Debug.Log("3");
        bool ok = false;
        //for (int i = 0; i < _spawnPoints.Length && !ok; i++)
        do
        {
            int i = UnityEngine.Random.Range(0, _spawnPoints.Length);
            //Debug.Log("2");
            //foreach (KeyValuePair<GameObject, int> keyValue in _spawnObjects)
            //{
            Debug.Log("1");
            //if (keyValue.Value != Array.IndexOf(_spawnPoints, _spawnPoints[i]))
            if (!_spawnObjects.ContainsValue(i))
            {
                Debug.Log("Create obj");
                GameObject _newObj = Instantiate(_prefab, _spawnPoints[i].position, _spawnPoints[i].rotation);
                _spawnObjects.Add(_newObj, Array.IndexOf(_prefabs, _prefab));
                ok = true;
                break;
            }
            //}
        }
        while(!ok);
    }
    void UpdateList()
    {
        Stack<GameObject> _delete = new Stack<GameObject>();
        foreach (KeyValuePair<GameObject, int> keyValue in _spawnObjects)
        {
            Debug.Log("null");
            if (keyValue.Key == null)
            {
                Debug.Log("Remove obj");
                _delete.Push(keyValue.Key);
            }
        }
        if(_delete.Count>0)
        {
            foreach (GameObject _nullObj in _delete)
            {
                Spawn(_prefabs[UnityEngine.Random.Range(0, _prefabs.Length)]);
                _spawnObjects.Remove(_nullObj);
            }
        }
        if (_spawnObjects.Count < _objMaxCount)
        {
            Spawn(_prefabs[UnityEngine.Random.Range(0, _prefabs.Length)]);
        }
        //else if(_spawnObjects.ContainsKey(null))
        //{
        //    Debug.Log("null");
        //    foreach (KeyValuePair<GameObject, int> keyValue in _spawnObjects)
        //    {
        //        Debug.Log("4");
        //        if (keyValue.Key == null)
        //        {
        //            Debug.Log("Remove obj");
        //            _spawnObjects.Remove(keyValue.Key);
        //        }
        //    }
        //}
    }
}
