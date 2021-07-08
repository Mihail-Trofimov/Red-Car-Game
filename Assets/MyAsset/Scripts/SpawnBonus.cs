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

        for (int i = 0; i < _objMaxCount; i++)
        {
            Spawn();
        }
        InvokeRepeating("UpdateList", 0f, 5f);
    }
    void Spawn()
    {
        GameObject _prefab = _prefabs[UnityEngine.Random.Range(0, _prefabs.Length)];
        bool ok = false;
        do
        {
            int i = UnityEngine.Random.Range(0, _spawnPoints.Length);
            if (!_spawnObjects.ContainsValue(i))
            {
                GameObject _newObj = Instantiate(_prefab, _spawnPoints[i].position, _spawnPoints[i].rotation);
                _spawnObjects.Add(_newObj, i);
                ok = true;
            }
        }
        while (!ok);
    }
    void UpdateList()
    {
        //Debug.Log("SpawnBonus UpdateList");
        Stack<GameObject> _delete = new Stack<GameObject>();
        foreach (KeyValuePair<GameObject, int> keyValue in _spawnObjects)
        {
            if (keyValue.Key == null)
            {
                _delete.Push(keyValue.Key);
            }
        }
        if (_delete.Count > 0)
        {
            foreach (GameObject _nullObj in _delete)
            {
                Spawn();
                _spawnObjects.Remove(_nullObj);
            }
        }
    }
}
