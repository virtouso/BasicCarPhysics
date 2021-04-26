using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Wheele[] _wheeles;

    [Header("Config")]
    [SerializeField] private float _wheeleBase;
    [SerializeField] private float _rearTrack;
    [SerializeField] private float _turnRadius;



    private float _steerInput;
    private float _angleLeft;
    private float _angleRight;

    private void Update()
    {

        _steerInput = Input.GetAxis("Horizontal");
        if (_steerInput > 0)
        {
            _angleLeft = Mathf.Rad2Deg * Mathf.Atan(_wheeleBase / (_turnRadius + (_rearTrack / 2))) * _steerInput;
            _angleRight = Mathf.Rad2Deg * Mathf.Atan(_wheeleBase / (_turnRadius - (_rearTrack / 2))) * _steerInput;

        }
        else if (_steerInput < 0)
        {

            _angleLeft = Mathf.Rad2Deg * Mathf.Atan(_wheeleBase / (_turnRadius - (_rearTrack / 2))) * _steerInput;
            _angleRight = Mathf.Rad2Deg * Mathf.Atan(_wheeleBase / (_turnRadius + (_rearTrack / 2))) * _steerInput;
        }
        else
        {
            _angleLeft = 0;
            _angleRight = 0;
        }

        for (int i = 0; i < _wheeles.Length; i++)
        {
            if (_wheeles[i].FrontLeft)
            {
                _wheeles[i].SteerAngle = _angleLeft;
            }
            else if (_wheeles[i].FrontRight)
            {
                _wheeles[i].SteerAngle = _angleRight;
               // _rigidBody.velocity = Vector3.zero;
            }
        }

    }










}
