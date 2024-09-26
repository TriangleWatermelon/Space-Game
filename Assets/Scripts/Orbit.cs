using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField]
    [Min(0.1f)]
    float speed = 1;

    void Start(){
        speed = (115 - Vector3.Distance(transform.position, Vector3.zero)/10);
    }

    void Update(){
        transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
    }
}
