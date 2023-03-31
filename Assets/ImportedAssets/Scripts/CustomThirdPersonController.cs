using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using StarterAssets;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */



[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
[RequireComponent(typeof(PlayerInput))]
#endif
public class CustomThirdPersonController : MonoBehaviour
{
//    [Header("Player")]
//    [Tooltip("Move speed of the character in m/s")]
//    public float MoveSpeed = 2.0f;

//    [Tooltip("Sprint speed of the character in m/s")]
//    public float SprintSpeed = 5.335f;

//    [Tooltip("How fast the character turns to face movement direction")]
//    [Range(0.0f, 0.3f)]
//    public float RotationSmoothTime = 0.12f;

//    [Tooltip("Acceleration and deceleration")]
//    public float SpeedChangeRate = 10.0f;

//    public AudioClip LandingAudioClip;
//    public AudioClip[] FootstepAudioClips;
//    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

//    [Space(10)]
//    [Tooltip("The height the player can jump")]
//    public float JumpHeight = 1.2f;

//    [Space(10)]
//    [Tooltip("The width the player can tackle")]
//    public float TackleWidth = 2.0f;

//    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
//    public float Gravity = -15.0f;

//    [Space(10)]
//    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
//    public float JumpTimeout = 0.50f;

//    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
//    public float FallTimeout = 0.15f;

//    [Space(10)]
//    [Tooltip("Time required to pass before being able to tackle again. Set to 0f to instantly tackle again")]
//    public float TackleTimeout = 0.50f;

//    [Header("Player Grounded")]
//    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
//    public bool Grounded = true;

//    [Tooltip("Useful for rough ground")]
//    public float GroundedOffset = -0.14f;

//    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
//    public float GroundedRadius = 0.28f;

//    [Tooltip("What layers the character uses as ground")]
//    public LayerMask GroundLayers;

//    [Header("Cinemachine")]
//    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
//    public GameObject CinemachineCameraTarget;

//    [Tooltip("How far in degrees can you move the camera up")]
//    public float TopClamp = 70.0f;

//    [Tooltip("How far in degrees can you move the camera down")]
//    public float BottomClamp = -30.0f;

//    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
//    public float CameraAngleOverride = 0.0f;

//    [Tooltip("For locking the camera position on all axis")]
//    public bool LockCameraPosition = false;

//    // cinemachine
//    private float _cinemachineTargetYaw;
//    private float _cinemachineTargetPitch;

//    // player
//    private float _speed;
//    public float CurrentSpeed => _speed;
//    private float _animationBlend;
//    private float _targetRotation = 0.0f;
//    private float _rotationVelocity;
//    private float _verticalVelocity;
//    private float _terminalVelocity = 53.0f;

//    // timeout deltatime
//    private float _jumpTimeoutDelta;
//    private float _fallTimeoutDelta;
//    private float _tackleTimeoutDelta;

//    // flags for other components
//    private int _x;
//    public int X => _x;
//    private int _z;
//    public int Z => _z;
//    private bool _sprint;
//    public bool Sprint => _sprint;
//    private bool _fall;
//    public bool Fall => _fall;
//    private bool _jump;
//    public bool Jump => _jump;
//    private bool _tackling;
//    public bool Tackling => _tackling;


//#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
//    private PlayerInput _playerInput;
//#endif
//    private CharacterController _controller;
//    private StarterAssetsInputs _input;
//    public StarterAssetsInputs InputSystem => _input;
//    private GameObject _mainCamera;

//    private const float _threshold = 0.01f;

//    private bool IsCurrentDeviceMouse
//    {
//        get
//        {
//#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
//            return _playerInput.currentControlScheme == "KeyboardMouse";
//#else
//				return false;
//#endif
//        }
//    }


//    private void Awake()
//    {
//        // get a reference to our main camera
//        if (_mainCamera == null)
//        {
//            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
//        }
//    }

//    private void Start()
//    {
//        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

//        _controller = GetComponent<CharacterController>();
//        _input = GetComponent<StarterAssetsInputs>();
//#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
//        _playerInput = GetComponent<PlayerInput>();
//#else
//			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
//#endif
//        // reset our timeouts on start
//        _jumpTimeoutDelta = JumpTimeout;
//        _fallTimeoutDelta = FallTimeout;
//        _tackleTimeoutDelta = TackleTimeout;
//    }

//    private void Update()
//    {
//        JumpAndGravity();
//        GroundedCheck();
//        Move();
//        Tackle();
//    }

//    private void LateUpdate()
//    {
//        CameraRotation();
//    }   

//    private void GroundedCheck()
//    {
//        // set sphere position, with offset
//        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
//            transform.position.z);
//        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
//            QueryTriggerInteraction.Ignore);    
//    }

//    private void CameraRotation()
//    {
//        // if there is an input and camera position is not fixed
//        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
//        {
//            //Don't multiply mouse input by Time.deltaTime;
//            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

//            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
//            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
//        }

//        // clamp our rotations so our values are limited 360 degrees
//        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
//        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

//        // Cinemachine will follow this target
//        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
//            _cinemachineTargetYaw, 0.0f);
//    }

//    private void Move()
//    {
//        // set target speed based on move speed, sprint speed and if sprint is pressed            
//        float targetSpeed = 0;
//        if (_input.sprint && _input.move.y > 0)
//        {
//            targetSpeed = SprintSpeed;
//            _sprint = Grounded;
//        }
//        else
//        {
//            targetSpeed = MoveSpeed;
//            _sprint = false;
//        }

//        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

//        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
//        // if there is no input, set the target speed to 0
//        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

//        // a reference to the players current horizontal velocity
//        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

//        float speedOffset = 0.1f;
//        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

//        if (Grounded)
//        {
//            // accelerate or decelerate to target speed
//            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
//                currentHorizontalSpeed > targetSpeed + speedOffset)
//            {
//                // creates curved result rather than a linear one giving a more organic speed change
//                // note T in Lerp is clamped, so we don't need to clamp our speed
//                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
//                    Time.deltaTime * SpeedChangeRate);

//                // round speed to 3 decimal places
//                _speed = Mathf.Round(_speed * 1000f) / 1000f;
//            }
//            else
//            {
//                _speed = targetSpeed;
//            }

//            //flags of flying
//            _fall = false;
//            _jump = false;
//        }

//        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
//        if (_animationBlend < 0.01f) _animationBlend = 0f;

//        // normalise input direction
//        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

//        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
//        // if there is a move input rotate player when the player is moving

//        _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
//                          _mainCamera.transform.eulerAngles.y;

//        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _mainCamera.transform.eulerAngles.y, ref _rotationVelocity,
//            RotationSmoothTime);

//        // rotate to face input direction relative to camera position
//        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

//        _x = (int)_input.move.x;
//        _z = (int)_input.move.y;

//        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

//        // move the player
//        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
//                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
//    }

//    private void JumpAndGravity()
//    {
//        if (Grounded)
//        {
//            // reset the fall timeout timer
//            _fallTimeoutDelta = FallTimeout;

//            // stop our velocity dropping infinitely when grounded
//            if (_verticalVelocity < 0.0f)
//            {
//                _verticalVelocity = -2f;
//            }

//            // Jump
//            if (_input.jump && _jumpTimeoutDelta <= 0.0f && _tackling == false)
//            {
//                // the square root of H * -2 * G = how much velocity needed to reach desired height
//                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

//                _jump = true;
//            }
//            else
//            {
//                _input.jump = false;
//            }

//            // jump timeout
//            if (_jumpTimeoutDelta >= 0.0f)
//            {
//                _jumpTimeoutDelta -= Time.deltaTime;
//            }
//        }
//        else
//        {
//            // reset the jump timeout timer
//            _jumpTimeoutDelta = JumpTimeout;

//            // fall timeout
//            if (_fallTimeoutDelta >= 0.0f)
//            {
//                _fallTimeoutDelta -= Time.deltaTime;
//            }

//            // if we are not grounded, do not jump
//            _input.jump = false;

//            if (_verticalVelocity > 0)
//            {
//                _jump = true;
//                _fall = false;
//            }
//            else
//            {
//                _jump = false;
//                _fall = true;
//            }
//        }

//        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
//        if (_verticalVelocity < _terminalVelocity)
//        {
//            _verticalVelocity += Gravity * Time.deltaTime;
//        }

//    }

//    private void Tackle()
//    {
//        if (_input.tackle && Grounded && _sprint == true && _tackling == false && _tackleTimeoutDelta <= 0.0f)
//        {
//            _input.tackle = false;
//            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
//            _tackling = true;
//            _sprint = false;
//            _controller.Move(targetDirection.normalized * TackleWidth);
//        }
//        else
//        {
//            _input.tackle = false;
//            if (_tackleTimeoutDelta >= 0.0f)
//            {
//                _tackleTimeoutDelta -= Time.deltaTime;
//            }

//            if (_tackling == true)
//            {
//                float tackleOffset = 0.5f;
//                if (_controller.velocity.magnitude < SprintSpeed + tackleOffset)
//                {
//                    _tackling = false;
//                    _tackleTimeoutDelta = TackleTimeout;
//                }
//            }
//        }
//    }

//    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
//    {
//        if (lfAngle < -360f) lfAngle += 360f;
//        if (lfAngle > 360f) lfAngle -= 360f;
//        return Mathf.Clamp(lfAngle, lfMin, lfMax);
//    }    

//    private void OnDrawGizmosSelected()
//    {
//        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
//        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

//        if (Grounded) Gizmos.color = transparentGreen;
//        else Gizmos.color = transparentRed;

//        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
//        Gizmos.DrawSphere(
//            new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
//            GroundedRadius);
//    }

//    private void OnFootstep(AnimationEvent animationEvent)
//    {
//        if (animationEvent.animatorClipInfo.weight > 0.5f)
//        {
//            if (FootstepAudioClips.Length > 0)
//            {
//                var index = Random.Range(0, FootstepAudioClips.Length);
//                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
//            }
//        }
//    }

//    private void OnLand(AnimationEvent animationEvent)
//    {
//        if (animationEvent.animatorClipInfo.weight > 0.5f)
//        {
//            AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
//        }
//    }
}

