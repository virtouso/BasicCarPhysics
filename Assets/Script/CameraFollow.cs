using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform _car;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _distance;
    private void LateUpdate()
    {
        transform.LookAt(_car.position);
        transform.position = _car.position + (-_car.forward * _distance) + _offset;
    }

}
