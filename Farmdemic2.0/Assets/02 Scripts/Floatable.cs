using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatable : MonoBehaviour
{
    [SerializeField] float speed = 2;
    [SerializeField] Vector3 originPos;
    
    // floating point
    float delta = 0.2f; 

    void Start()
    {
        originPos = transform.position;
    }

    void Update()
    {
        Vector3 vec;
        vec.x = transform.position.x;
        vec.z = transform.position.z;
        vec.y = delta * Mathf.Sin(speed * Time.time) + originPos.y;
        transform.position = vec;
    }

}