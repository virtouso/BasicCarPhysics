using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Wheele : MonoBehaviour
{
    public bool FrontRight;
    public bool FrontLeft;
    public bool RearRight;
    public bool RearLeft;

    public float SteerAngle;




    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private LayerMask _groundLayer;
    [Header("suspension")]
    [SerializeField] private float _restLength;
    [SerializeField] private float _springTravel;
    [SerializeField] private float _springStiffness;
    [SerializeField] private float _damperStiffness;
    [SerializeField] private float _steerTime;


    private float _minLength;
    private float _maxLength;
    private float _lastLength;
    private float _springLength;
    private float _springForce;
    private float _springVelocity;
    private Vector3 _suspensionForce;
    private float _damperForce;

    private Vector3 _wheeleVelocityLocalSpace;
    private float Fx, Fy;



    [Header("Wheele")]
    [SerializeField] private float _wheeleRadius;


    private void Start()
    {
        _minLength = _restLength - _springTravel;
        _maxLength = _restLength + _springTravel;
    }


    private float _wheeleAngle;
    private void Update()
    {
        _wheeleAngle = Mathf.Lerp(_wheeleAngle, SteerAngle, _steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * _wheeleAngle);
    }

    private void FixedUpdate()
    {

        Debug.DrawRay(transform.position, -transform.up * (_maxLength + _wheeleRadius), Color.red);
        bool hasHit = Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, _maxLength + _wheeleRadius, _groundLayer);
        if (hasHit)
        {
            _lastLength = _springLength;
            _springLength = hit.distance - _wheeleRadius;
            _springLength = Mathf.Clamp(_springLength, _minLength, _maxLength);
            _springVelocity = (_lastLength - _springLength) / Time.deltaTime;

            _springForce = _springStiffness * (_restLength - _springLength);
            _damperForce = _damperStiffness * _springVelocity;
            _suspensionForce = (_springForce + _damperForce) * transform.up;

            _wheeleVelocityLocalSpace = transform.InverseTransformDirection(_rigidBody.GetPointVelocity(hit.point));
            Fx = Input.GetAxis("Vertical") * 0.5f * _springForce;
            Fy = _wheeleVelocityLocalSpace.x * _springForce;


            _rigidBody.AddForceAtPosition(new Vector3(0, _suspensionForce.y, 0) + (Fx * transform.forward) /*+ (Fy * transform.right)*/, hit.point);
        }
    }

}
