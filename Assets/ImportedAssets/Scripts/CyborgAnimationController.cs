using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion.KCC;
using Projectiles;


public class CyborgAnimationController : MonoBehaviour
{
    //[SerializeField]
    //private CustomThirdPersonController _mover;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private KCC _kcc;

    [SerializeField]
    private PlayerAgent _agent;

    private KCCData _kccData;
    private PlayerInput _input;

    private bool _hasAnimator;

    //animation velocities
    private float _velocityX;
    private float _velocityZ;
    private float _velocityYX;
    private float _velocityYZ;
    private float _changingAnimationCoef = 5.0f;
    private float _changingAnimZTimer;
    private float _changingAnimXTimer;

    //animation flags
    private int _animIDVelocityZ;
    private int _animIDVelocityX;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDGrounded;

    private void Start()
    {
        _hasAnimator = _animator != null ? true : false;

        if (_hasAnimator)
        {
            AssignAnimationIDs();
        }

        if (_kcc != null)
        {
            _kccData = _kcc.FixedData;
        }

        if (_agent != null)
        {
            _input = _agent.Owner.Input;
        }
    }

    private void AssignAnimationIDs()
    {
        _animIDVelocityZ = Animator.StringToHash("Velocity Z");
        _animIDVelocityX = Animator.StringToHash("Velocity X");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");

    }

    private void FixedUpdate()
    {
        if (_hasAnimator)
        {
            SetAnimations();
            //Debug.Log(_kccData.InputDirection.ToString() + " " + _kcc.transform.forward);
            //Vector3 res = new Vector3(_kccData.InputDirection.x, _kccData.InputDirection.y, _kccData.InputDirection.z) / _kccData.TransformRotation;
            //Debug.Log(_kccData.RealVelocity.normalized.ToString() + (_kccData.TransformRotation * Vector3.forward).ToString());
            //Debug.Log(Vector3.Dot(_kccData.RealVelocity.normalized, (_kccData.TransformRotation * Vector3.forward)));
            //Debug.Log(Vector3.Angle(_kcc.transform.forward, _kccData.RealVelocity));
            //Debug.Log(Quaternion.FromToRotation(Vector3.up, _kccData.RealVelocity - _kcc.transform.forward).eulerAngles.z);
        }        
    }

    private void SetAnimations()
    {
        //first variant - we dont jump and fall, just running
        if (!_kccData.HasJumped && _kccData.IsGrounded)
        {
            //velocity of falling to 0
            //_velocityYX = 0;
            //_velocityYZ = 0;
            _velocityYZ = ChangeVelocitySmoothly(_velocityZ, 0.0f, ref _changingAnimZTimer);
            _velocityYX = ChangeVelocitySmoothly(_velocityX, 0.0f, ref _changingAnimXTimer);

            //we move on Z axis
            if (_input.RenderInput.MoveDirection.y != 0)
            {
                _velocityZ += (_input.RenderInput.MoveDirection.y / Mathf.Abs(_input.RenderInput.MoveDirection.y)) * Time.deltaTime * _changingAnimationCoef;
                //clamp between lowest animation velocity and current maximum speed
                //_velocityZ = Mathf.Clamp(_velocityZ, -1, Mathf.Lerp(1, 2, (_mover.CurrentSpeed - _mover.MoveSpeed) / (_mover.SprintSpeed - _mover.MoveSpeed)));
                _velocityZ = Mathf.Clamp(_velocityZ, -1, 1);
            }
            else // we dont move on Z axis, so slowly decrease Z velocity while its not near 0
            {
                if (Mathf.Abs(_velocityZ) > 0.05f)
                {
                    _velocityZ -= Time.deltaTime * _changingAnimationCoef * _velocityZ;
                }
                else
                {
                    _velocityZ = 0;
                }
            }

            //we move on X axis
            if (_input.RenderInput.MoveDirection.x != 0)
            {
                _velocityX += (_input.RenderInput.MoveDirection.x / Mathf.Abs(_input.RenderInput.MoveDirection.x)) * Time.deltaTime * _changingAnimationCoef;
                //clamp between lowest and hightest animation velocity 
                _velocityX = Mathf.Clamp(_velocityX, -1, 1);
            }
            else // we dont move on X axis, so slowly decrease X velocity while its not near 0
            {
                if (Mathf.Abs(_velocityX) > 0.05f)
                {
                    _velocityX -= Time.deltaTime * _changingAnimationCoef * _velocityX;
                }
                else
                {
                    _velocityX = 0;
                }
            }
        }

        _animator.SetBool(_animIDGrounded, _kccData.IsGrounded);

        if (_kccData.IsGrounded)
        {
            _animator.SetBool(_animIDJump, false);
            _animator.SetBool(_animIDFreeFall, false);
        }

        if (_kccData.HasJumped)
        {
            Debug.Log("’‡È");
            _animator.SetBool(_animIDJump, true);
        }

        if (!_kccData.IsGrounded && !_kccData.HasJumped)
        {
            _animator.SetBool(_animIDFreeFall, true);
        }                      

        _animator.SetFloat(_animIDVelocityZ, _velocityZ);
        _animator.SetFloat(_animIDVelocityX, _velocityX);
        //_animator.SetFloat("Velocity YZ", _velocityYZ);
        //_animator.SetFloat("Velocity YX", _velocityYX);


    }

    private float LerpToValue(float whatIsLerping, float targetValue, float timer)
    {
        if (Mathf.Abs(whatIsLerping - targetValue) < 0.1f)
        {
            return targetValue;
        }
        else
        {
            float lerper = Mathf.Lerp(whatIsLerping, targetValue, timer);
            return lerper;
        }
    }

    private float ChangeVelocitySmoothly(float changingVelocity, float targetVelocity, ref float timer)
    {
        if (Mathf.Abs(changingVelocity - targetVelocity) > 0.1f)
        {
            timer += Time.deltaTime;
            return LerpToValue(changingVelocity, targetVelocity, timer);
        }
        else
        {
            timer = 0;
            return targetVelocity;
        }
    }
}
