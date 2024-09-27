using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MarioController : MonoBehaviour

{
    // exposes the field in the inspector such that it can be assigned (that's what SerializeField does)
    [SerializeField] private Rigidbody2D marioRigidBody;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float climbStrength;

    [FormerlySerializedAs("actionAsset")] [SerializeField]
    private InputActionAsset playerControlsActionAsset;

    [SerializeField] private InputActionAsset userActionsActionAsset;
    [SerializeField] private Animator animator;
    [SerializeField] private float boxCastDistance;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioClip hammerMusic;
    [SerializeField] private Sprite marioClimbSpriteRightArmUp;
    [SerializeField] private Sprite marioClimbSpriteLeftArmUp;
    [SerializeField] private float timeToHammerFor;
    [SerializeField] private GameObject hammerThrowable;
    [SerializeField] private AudioClip levelOneBackgroundMusic;
    [SerializeField] private AudioClip levelTwoBackgroundMusic;
    [SerializeField] private float hammerThrowUpForce;
    [SerializeField] private GameObject scoreEffect;
    [SerializeField] private AudioClip walkMusicClip;
    [SerializeField] private AudioClip jumpSoundEffect;
    [SerializeField] private AudioClip deathSoundEffect;
    [SerializeField] private Sprite[] marioMountTopOfPlatformSprites;
    [SerializeField] private GameObject fireball;
    [SerializeField] private Sprite fireballHammerSprite;
    [SerializeField] private FireballScript FireballScript;


    private GameObject _scoreEffect;
    private UserInterFaceManager _userInterFaceManager;
    private int _currentLiveCount;
    private bool _isAlive;
    private bool _isClimbingLadder;
    private int _isJumpingAnimatorIndex;
    private int _isHammeringAnimatorIndex;
    private bool _isTouchingGround;
    private SpriteRenderer _marioSpriteRenderer;
    private Sprite _marioSprite;
    private GameObject _hammerColliderObject;
    private bool _isFacingLeft;
    private bool _isInMovingLadderCollider;


    private Vector2 _moveDirection;
    private int _xVelocityAnimatorIndex;
    private bool _isHammering;
    private float _hammerTimeLeft;
    private AudioSource _walkAudioSource;
    private AudioSource _backgroundMusic;
    private int _isClimbingAnimatorIndex;

    private bool _isInBottomOfLadderCollider;
    private bool _isInTopOfLadderCollider;
    private bool _isInMiddleOfLadderCollider;
    private int _yVelocityAnimatorIndex;
    private int _marioClimbLadderTopAnimatorIndex;

    public delegate void MarioHammeringAction();

    public delegate void MarioNoLongerHammeringAction();

    public static event MarioHammeringAction OnMarioHammering;
    public static event MarioNoLongerHammeringAction OnMarioNotHammering;


    private void Awake()
    {
        _xVelocityAnimatorIndex = Animator.StringToHash("Speed"); //Assign an index to the animator parameter "Speed"
        _isJumpingAnimatorIndex = Animator.StringToHash("isJumping");
        _isHammeringAnimatorIndex = Animator.StringToHash("isHammering");
        _isClimbingAnimatorIndex = Animator.StringToHash("isClimbing");
        _marioSpriteRenderer = GetComponent<SpriteRenderer>();
        _isAlive = true;
        _userInterFaceManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterFaceManager>();
        _marioClimbLadderTopAnimatorIndex = Animator.StringToHash("isMountingPlatform");
    }

    private void Start()
    {
        _walkAudioSource = AudioManagerScript.Instance.CreateSoundFxAudioSource(walkMusicClip, 0.2f);

        if (SceneManager.GetActiveScene().name == "Level 1") //level 1
        {
            _backgroundMusic = AudioManagerScript.Instance.CreateSoundFxAudioSource(levelOneBackgroundMusic, .2f);
            _backgroundMusic.tag = "BackgroundMusic";
        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            // change this to be unique on each level
            _backgroundMusic = AudioManagerScript.Instance.CreateSoundFxAudioSource(levelTwoBackgroundMusic, .2f);
            _backgroundMusic.tag = "BackgroundMusic";
        }
    }

    private void Update()
    {
        if (_isAlive)
        {
            if (!_isInBottomOfLadderCollider && !_isInMiddleOfLadderCollider && !_isInTopOfLadderCollider &&
                !_isInMovingLadderCollider)

            {
                marioRigidBody.gravityScale = 1f;
                gameObject.layer = LayerMask.NameToLayer("Mario");
            }

            _moveDirection = InputManager.Instance.MoveInput;
            OnMove();


            if (InputManager.Instance.JumpJustPressed && !_isClimbingLadder)
            {
                if (IsGrounded() && _isAlive && !_isHammering)
                {
                    marioRigidBody.velocity = new Vector2(marioRigidBody.velocity.x, jumpStrength);
                    AudioManagerScript.Instance.PlaySoundFxClip(jumpSoundEffect, transform, 0.2f);
                    _walkAudioSource.Stop();
                }
            }


            HandleClimb();

            animator.SetFloat(_xVelocityAnimatorIndex, Mathf.Abs(marioRigidBody.velocity.x));
            animator.SetFloat("yVelocity", marioRigidBody.velocity.y);
            animator.SetBool(_isClimbingAnimatorIndex, _isClimbingLadder);
            if (!InputManager.Instance.ClimbIsPressed &&
                ((marioRigidBody.constraints & RigidbodyConstraints2D.FreezePositionX) == 0)) //sort this out
            {
                animator.SetBool(_isJumpingAnimatorIndex, !IsGrounded());
            }

            animator.SetBool(_isHammeringAnimatorIndex, _isHammering);

            if (marioRigidBody.velocity.x < 0)
            {
                _marioSpriteRenderer.flipX = true;
            }

            else if (marioRigidBody.velocity.x > 0)
            {
                _marioSpriteRenderer.flipX = false;
            }

            if (!_isHammering)
            {
                if (InputManager.Instance.MoveIsPressed)
                {
                    _walkAudioSource.enabled = true;
                }
                else
                {
                    _walkAudioSource.enabled = false;
                }
            }


            if (_isHammering)
            {
                if (OnMarioHammering != null)
                    OnMarioHammering();
                _walkAudioSource.enabled = false;
                _backgroundMusic.Stop();
                if (_hammerTimeLeft >
                    0) //when player collides with the hammer trigger, set the bool isHammering to true, and also set the hammer timer to begin
                {
                    Debug.Log("should reduce time every second");
                    _hammerTimeLeft -= Time.deltaTime;
                    if (InputManager.Instance.JumpJustPressed)
                    {
                        Debug.Log("throw hammer up");
                        _isHammering = false;
                        Destroy(GameObject.FindGameObjectWithTag("hammerAudio"));
                        GameObject hammerThrownUp = Instantiate(hammerThrowable,
                            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y),
                            Quaternion.identity);
                        hammerThrownUp.GetComponent<Rigidbody2D>().velocity =
                            hammerThrownUp.transform.up * hammerThrowUpForce;
                        _backgroundMusic.Play();
                        if (OnMarioNotHammering != null)
                        {
                            OnMarioNotHammering();
                        }


                        _hammerTimeLeft = 0;
                    }
                }
                else
                {
                    _backgroundMusic.Play();
                    _isHammering = false;
                    _hammerTimeLeft = 0;
                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.collider.CompareTag("Barrel") || other.collider.CompareTag("CementTub")) && !_isHammering)
        {
            OnMarioDeath(2f, 1f);
        }
    }

    private void OnDrawGizmos() //when gizmos active performs this function
    {
        Gizmos.DrawWireCube(transform.position - transform.up * boxCastDistance, boxSize);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovingLadder"))
        {
            _isInMovingLadderCollider = true;
        }

        if (other.CompareTag("AboveBarrel") && _isAlive)
        {
            _userInterFaceManager.ChangeMarioScore(100, transform);
        }

        if (other.CompareTag("End Of Level"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.CompareTag("Hammer"))
        {
            AudioManagerScript.Instance.PlaySoundFxClip(hammerMusic, transform, .2f, timeToHammerFor, "hammerAudio");
            Destroy(other.gameObject);
            _isHammering = true;
            _hammerTimeLeft = timeToHammerFor;
        }

        if (other.CompareTag("End Of Level"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.CompareTag("BottomOfLadder"))
        {
            _isInBottomOfLadderCollider = true;
        }

        if (other.CompareTag("TopOfLadder"))
        {
            _isInTopOfLadderCollider = true;
            if ((_isInMiddleOfLadderCollider || _isInMovingLadderCollider) && InputManager.Instance.ClimbIsPressed)
            {
                animator.SetBool(_marioClimbLadderTopAnimatorIndex, true);
                animator.SetBool("isMountingPlatform", true);
            }

            Debug.Log("top of ladder");
        }

        if (other.CompareTag("MiddleOfLadder"))
        {
            _isInMiddleOfLadderCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovingLadder"))
        {
            _isInMovingLadderCollider = false;
        }

        if (other.CompareTag("BottomOfLadder")) //player reaches bottom of ladder and decides to move left and right
        {
            _isClimbingLadder = false;
            _isInBottomOfLadderCollider = false;
        }


        if (other.CompareTag("MiddleOfLadder"))
        {
            _isInMiddleOfLadderCollider = false;
        }

        if (other.CompareTag("TopOfLadder"))
        {
            Debug.Log("leavetop");
            _isClimbingLadder = false;
            _isInTopOfLadderCollider = false;
            animator.SetBool("isMountingPlatform", false);
        }
    }


    private void OnMove()
    {
        marioRigidBody.velocity =
            new Vector2(_moveDirection.x * horizontalSpeed, marioRigidBody.velocity.y);
    }

    private void HandleClimb()
    {
        if (_isClimbingLadder && !InputManager.Instance.ClimbIsPressed &&
            !InputManager.Instance.GoDownLadderIsPressed)
        {
            animator.speed = 0f;
            animator.SetBool(_isJumpingAnimatorIndex, false);
        }
        else
        {
            animator.speed = 1f;
        }

        if (_isClimbingLadder && IsGrounded())
        {
            animator.SetBool(_isClimbingAnimatorIndex, false);
        }


        if ((_isInMiddleOfLadderCollider || _isInMovingLadderCollider) &&
            !(_isInBottomOfLadderCollider || _isInTopOfLadderCollider) &&
            _isClimbingLadder)
        {
            marioRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            animator.SetBool(_isJumpingAnimatorIndex, false);
        }

        const string passableObjects = "Passable Objects";
        if ((_isInMiddleOfLadderCollider || _isInMovingLadderCollider) && !_isHammering)
        {
            if (InputManager.Instance.ClimbIsPressed)
            {
                _isClimbingLadder = true;
                gameObject.layer = LayerMask.NameToLayer(passableObjects);
                //any kind of lower case reference like this, e.g. gameObject, transform references the component of the object that the script is attached to
                marioRigidBody.transform.position += new Vector3(0, climbStrength) * Time.deltaTime;
                marioRigidBody.velocity = new Vector2(0f, 0f);
                marioRigidBody.gravityScale = 0f;
                animator.SetBool(_isClimbingAnimatorIndex, true);
                animator.SetBool(_isJumpingAnimatorIndex, false);
            }

            else if (InputManager.Instance.GoDownLadderIsPressed)
            {
                gameObject.layer = LayerMask.NameToLayer(passableObjects);
                _isClimbingLadder = true;
                marioRigidBody.transform.position += new Vector3(0, -climbStrength) * Time.deltaTime;
                marioRigidBody.velocity = new Vector2(0f, 0f);
                marioRigidBody.gravityScale = 0f;
                animator.SetBool(_isClimbingAnimatorIndex, true);
                animator.SetBool(_isJumpingAnimatorIndex, false);
            }
        }


        if (_isInTopOfLadderCollider && !_isHammering)
        {
            marioRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (InputManager.Instance.ClimbIsPressed)
            {
                _isClimbingLadder = true;
                gameObject.layer = LayerMask.NameToLayer(passableObjects);
                marioRigidBody.transform.position += new Vector3(0, climbStrength) * Time.deltaTime;
                marioRigidBody.velocity = new Vector2(0f, 0f);
                marioRigidBody.gravityScale = 0f;
                animator.SetBool(_isClimbingAnimatorIndex, true);
                animator.SetBool(_isJumpingAnimatorIndex, false);
            }
            else if (InputManager.Instance.GoDownLadderIsPressed)
            {
                gameObject.layer = LayerMask.NameToLayer(passableObjects);
                _isClimbingLadder = true;
                marioRigidBody.transform.position += new Vector3(0, -climbStrength) * Time.deltaTime;
                marioRigidBody.velocity = new Vector2(0f, 0f);
                marioRigidBody.gravityScale = 0f;
                animator.SetBool(_isClimbingAnimatorIndex, true);
                animator.SetBool(_isJumpingAnimatorIndex, false);
            }
        }

        if (_isInBottomOfLadderCollider && !_isHammering)
        {
            marioRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (InputManager.Instance.ClimbIsPressed)
            {
                _isClimbingLadder = true;
                gameObject.layer = LayerMask.NameToLayer("Mario");
                marioRigidBody.transform.position += new Vector3(0, climbStrength) * Time.deltaTime;
                marioRigidBody.velocity = new Vector2(0f, 0f);
                marioRigidBody.gravityScale = 0f;
                animator.SetBool(_isClimbingAnimatorIndex, true);
                animator.SetBool(_isJumpingAnimatorIndex, false);
            }
            else if (InputManager.Instance.GoDownLadderIsPressed)
            {
                gameObject.layer = LayerMask.NameToLayer("Mario");
                _isClimbingLadder = true;
                marioRigidBody.transform.position += new Vector3(0, -climbStrength) * Time.deltaTime;
                marioRigidBody.velocity = new Vector2(0f, 0f);
                marioRigidBody.gravityScale = 0f;
                animator.SetBool(_isClimbingAnimatorIndex, true);
                animator.SetBool(_isJumpingAnimatorIndex, false);
            }
        }
    }

    /*
     * totalTimeToPauseFor: the sum of the time between collision with a barrel, and the time that the death animation will play for
     * startOfDeathAnimationDelay: the length of time in seconds that the game will pause befor starting the death animation
     */
    private void OnMarioDeath(float timeToPauseAfterBarrelImpact, float timeToPlayMarioDeathAnimationFor)
    {
        StartCoroutine(PauseGameKillMarioResetScene(timeToPauseAfterBarrelImpact));
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, boxCastDistance, groundLayer);
    }

    //Game paused for the first parameter in seconds after collision with a barrel, and then will delay restarting the scene after the 
    //second parameter in seconds
    private IEnumerator PauseGameKillMarioResetScene(float timeToPauseAfterBarrelImpact)
    {
        PauseManager.Instance.PauseGame();

        //These timers are used to delay code execution
        var delayAfterBarrelHitTimer = Time.realtimeSinceStartup + timeToPauseAfterBarrelImpact;
        while (Time.realtimeSinceStartup < delayAfterBarrelHitTimer)
        {
            yield return 0;
        }

        animator.SetBool(_isJumpingAnimatorIndex, false);
        marioRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        marioRigidBody.velocity = new Vector2(0, 0);
        _isAlive = false;
        animator.Play("Death");
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        AudioManagerScript.Instance.PlaySoundFxClip(deathSoundEffect, transform, .2f);

        //These timers are used to delay code execution
        var playAnimationBeforeResettingGameTimer = Time.realtimeSinceStartup + deathSoundEffect.length;
        while (Time.realtimeSinceStartup < playAnimationBeforeResettingGameTimer)
        {
            yield return 0;
        }

        _userInterFaceManager.OnPlayerDamage(GameManager.Instance.NumberOfLives,
            _userInterFaceManager.marioLifeIconHolder, _userInterFaceManager.marioLifeIcon);
        var currentScene =
            SceneManager
                .GetActiveScene(); //cannot be done on awake or start, as the scene may change during playtime (player gets to end of level)
        SceneManager.LoadScene(currentScene.name);
        PauseManager.Instance.ResumeGame();
    }
}