using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheele : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private LayerMask _groundLayer;
    [Header("suspension")]
    [SerializeField] private float _restLength;
    [SerializeField] private float _springTravel;
    [SerializeField] private float _springStiffness;

    private float _minLength;
    private float _maxLength;
    private float _springLength;
    private float _springForce;
    private Vector3 _suspensionforce;

    [Header("Wheele")]
    [SerializeField] private float _wheeleRadius;

    private void Start()
    {
        _minLength = _restLength - _springTravel;
        _maxLength = _restLength + _springTravel;
    }

    private void FixedUpdate()
    {

        Debug.DrawRay(transform.position, -transform.up * (_maxLength + _wheeleRadius),Color.red);
        bool hasHit = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, _maxLength + _wheeleRadius, _groundLayer);
        if (hasHit)
        {
            print("asdasds");
            _springLength = hit.distance - _wheeleRadius;

            _springForce = _springStiffness * (_restLength - _springLength);
            _suspensionforce = _springForce * transform.up;
            _rigidBody.AddForceAtPosition(_suspensionforce, hit.point);
        }
    }

}
