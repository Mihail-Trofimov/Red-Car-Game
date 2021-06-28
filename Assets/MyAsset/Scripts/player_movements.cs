using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movements : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Vector3 _direction;

    public int count = 0;

    private void Awake()
    {
        _direction = Vector3.zero;
    }

    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        var speed = _direction.normalized * _speed * Time.fixedDeltaTime;
        transform.Translate(speed);
    }
}
