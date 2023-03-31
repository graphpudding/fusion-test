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
        }        
    }

    private void SetAnimations()
    {
        //first variant - we dont jump and fall, just running
        if (!_kccData.HasJumped && _kccData.IsGrounded)
        {
            float angle = Vector3.Angle(_kcc.transform.forward, _kccData.RealVelocity);
            float resultAngle = Vector3.Angle(_kcc.transform.right, _kccData.RealVelocity) > 90f ? 360f - angle - 0.1f : angle + 0.1f;

            if (_kccData.RealSpeed == 0)
            {
                resultAngle = -1;
            }
            Vector2 inputVector = new Vector2();
            
            if (resultAngle >= 314 || (resultAngle <= 46 && resultAngle >= 0f))
            {
                inputVector.y = 1;
            }
            else if (resultAngle <= 226 && resultAngle >= 134)
            {
                inputVector.y = -1;
            }
            else
            {
                inputVector.y = 0;
            }

            if (resultAngle <= 316 && resultAngle >= 224)
            {
                inputVector.x = -1;
            }
            else if (resultAngle >= 44 && resultAngle <= 136)
            {
                inputVector.x = 1;
            }
            else
            {
                inputVector.x = 0;
            }           

            _velocityYZ = ChangeVelocitySmoothly(_velocityZ, 0.0f, ref _changingAnimZTimer);
            _velocityYX = ChangeVelocitySmoothly(_velocityX, 0.0f, ref _changingAnimXTimer);

            //we move on Z axis
            if (inputVector.y != 0)
            {
                _velocityZ += (inputVector.y / Mathf.Abs(inputVector.y)) * Time.deltaTime * _changingAnimationCoef;               
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
            if (inputVector.x != 0)
            {
                _velocityX += (inputVector.x / Mathf.Abs(inputVector.x)) * Time.deltaTime * _changingAnimationCoef;
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
            _animator.SetBool(_animIDJump, true);
        }

        if (!_kccData.IsGrounded && !_kccData.HasJumped)
        {
            _animator.SetBool(_animIDFreeFall, true);
        }                      

        _animator.SetFloat(_animIDVelocityZ, _velocityZ);
        _animator.SetFloat(_animIDVelocityX, _velocityX);


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
