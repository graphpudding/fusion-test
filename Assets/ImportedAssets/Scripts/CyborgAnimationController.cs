using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion.KCC;


public class CyborgAnimationController : MonoBehaviour
{
    //[SerializeField]
    //private CustomThirdPersonController _mover;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private KCC _kcc;

    private KCCData _kccData;

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
            _kccData = _kcc.RenderData;
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
        }

        Debug.Log(_kccData.JumpImpulse);
    }

    private void SetAnimations()
    {
        ////first variant - we dont jump and fall, just running
        //if (!_mover.Jump && !_mover.Fall)
        //{
        //    //velocity of falling to 0
        //    //_velocityYX = 0;
        //    //_velocityYZ = 0;
        //    _velocityYZ = ChangeVelocitySmoothly(_velocityZ, 0.0f, ref _changingAnimZTimer);
        //    _velocityYX = ChangeVelocitySmoothly(_velocityX, 0.0f, ref _changingAnimXTimer);

        //    //we move on Z axis
        //    if (_mover.InputSystem.move.y != 0)
        //    {
        //        _velocityZ += _mover.InputSystem.move.y * Time.deltaTime * _changingAnimationCoef;
        //        //clamp between lowest animation velocity and current maximum speed
        //        _velocityZ = Mathf.Clamp(_velocityZ, -1, Mathf.Lerp(1, 2, (_mover.CurrentSpeed - _mover.MoveSpeed) / (_mover.SprintSpeed - _mover.MoveSpeed)));
        //    }
        //    else // we dont move on Z axis, so slowly decrease Z velocity while its not near 0
        //    {
        //        if (Mathf.Abs(_velocityZ) > 0.05f)
        //        {
        //            _velocityZ -= Time.deltaTime * _changingAnimationCoef * _velocityZ;
        //        }
        //        else
        //        {
        //            _velocityZ = 0;
        //        }
        //    }

        //    //we move on X axis
        //    if (_mover.InputSystem.move.x != 0)
        //    {
        //        _velocityX += _mover.InputSystem.move.x * Time.deltaTime * _changingAnimationCoef;
        //        //clamp between lowest and hightest animation velocity 
        //        _velocityX = Mathf.Clamp(_velocityX, -1, 1);
        //    }
        //    else // we dont move on X axis, so slowly decrease X velocity while its not near 0
        //    {
        //        if (Mathf.Abs(_velocityX) > 0.05f)
        //        {
        //            _velocityX -= Time.deltaTime * _changingAnimationCoef * _velocityX;
        //        }
        //        else
        //        {
        //            _velocityX = 0;
        //        }
        //    }
        //}


        //_animator.SetBool(_animIDGrounded, _mover.Grounded);
        //if (_mover.Grounded)
        //{
        //    _animator.SetBool(_animIDJump, false);
        //    _animator.SetBool(_animIDFreeFall, false);
        //}

        //if (_mover.Jump)
        //{
        //    _animator.SetBool(_animIDJump, true);
        //}

        //if (_mover.Fall)
        //{
        //    _animator.SetBool(_animIDFreeFall, true);
        //}


        //if (_mover.Jump)
        //{
        //    //only start jump
        //    if (_velocityYX < 1)
        //    {
        //        _velocityYX += Time.deltaTime;
        //        _velocityYX = Mathf.Clamp(_velocityYX, 0, 1);
        //    }
        //    else
        //    {
        //        _velocityYX -= Time.deltaTime;
        //        _velocityYX = Mathf.Clamp(_velocityYX, 0, 1);

        //        _velocityYZ += Time.deltaTime;
        //        _velocityYZ = Mathf.Clamp(_velocityYZ, 0, 1);
        //    }

        //    _velocityZ = ChangeVelocitySmoothly(_velocityZ, 0.2f, ref _changingAnimZTimer);
        //    _velocityX = ChangeVelocitySmoothly(_velocityX, 0.0f, ref _changingAnimXTimer);

        //}
        //if (_mover.Fall)
        //{
        //    if (_velocityYX > 0)
        //    {
        //        _velocityYX -= Time.deltaTime;
        //        _velocityYX = Mathf.Clamp(_velocityYX, 0, 1);
        //    }

        //    _velocityYZ -= Time.deltaTime;

        //    Vector3 legsPos = new Vector3(transform.position.x, transform.position.y - _mover.GroundedOffset - _mover.GroundedRadius,
        //        transform.position.z);
        //    RaycastHit hit;
        //    if (Physics.Raycast(legsPos, Vector3.down, out hit))
        //    {
        //        if (Vector3.Distance(legsPos, hit.point) < 2.0f)
        //        {
        //            _velocityYZ = Mathf.Clamp(_velocityYZ, 0, 1);
        //        }
        //        else
        //        {
        //            _velocityYZ = Mathf.Clamp(_velocityYZ, 0.75f, 1);
        //        }
        //    }
        //    //else
        //    //{
        //    //    _velocityYZ = Mathf.Clamp(_velocityYZ, 0.5f, 1);
        //    //}


        //    _velocityZ = ChangeVelocitySmoothly(_velocityZ, 0.2f, ref _changingAnimZTimer);
        //    _velocityX = ChangeVelocitySmoothly(_velocityX, 0.0f, ref _changingAnimXTimer);

        //}

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
