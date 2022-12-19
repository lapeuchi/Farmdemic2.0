using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    private Transform _rightPoint, _leftPoint, _birdModel;
    private float _moveSpeed;
    private Vector3 _destination;

    void Start()
    {
        _moveSpeed = 5f;
        _destination = _rightPoint.position;
    }

    void Update()
    {
        if (true)
        {

        }

    }
}
