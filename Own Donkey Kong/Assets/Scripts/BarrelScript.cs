using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D barrelRigidBody;
    [SerializeField] private float barrelInitialHorizontalSpeed;
    [SerializeField] private float barrelDescendLadderSpeed;
    [SerializeField] private float barrelGravityScale;
    [SerializeField] private Vector2 barrelSpawnPoint;
    [SerializeField] private Animator barrelAnimator;

    private int _funBarrelValue; //Undertale reference I love you toby fox <3
    private bool _isAboveLadder;
    private float _barrelHorizontalSpeed;
    private bool _isMovingLeft;
    private bool _isMovingRight;
    private int _isMovingLeftAnimatorIndex;
    private int _isMovingRightAnimatorIndex;
    private int _isDescendingAnimatorIndex;

    private void Awake()
    {
        _barrelHorizontalSpeed = barrelInitialHorizontalSpeed;
        barrelRigidBody.gravityScale = barrelGravityScale;
        _funBarrelValue = Random.Range(1, 5);
        _isMovingLeftAnimatorIndex = Animator.StringToHash("IsMovingLeft");
        _isMovingRightAnimatorIndex = Animator.StringToHash("IsMovingRight");
        _isDescendingAnimatorIndex = Animator.StringToHash("isDescending");
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log(_funBarrelValue);
    }

    // Update is called once per frame
    private void Update()
    {
        barrelRigidBody.velocity = new Vector2(_barrelHorizontalSpeed, -barrelDescendLadderSpeed);
        barrelAnimator.SetBool(_isMovingRightAnimatorIndex, _isMovingRight);
        barrelAnimator.SetBool(_isMovingLeftAnimatorIndex, _isMovingLeft);
        barrelAnimator.SetBool(_isDescendingAnimatorIndex, Mathf.Abs(1 - barrelDescendLadderSpeed) < Mathf.Epsilon);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Above Ladder (MUST DESCEND)"))
        {
            gameObject.layer = LayerMask.NameToLayer("Passable Objects");
            _barrelHorizontalSpeed = 0;
            barrelDescendLadderSpeed = 1;
        }

        if (other.CompareTag("BarrelLeft"))
        {
            gameObject.layer = LayerMask.NameToLayer("Barrel");
            _barrelHorizontalSpeed = -barrelInitialHorizontalSpeed;
            barrelDescendLadderSpeed = 0;
            _isMovingLeft = true;
            _isMovingRight = false;
        }

        if (other.CompareTag("BarrelRight"))
        {
            gameObject.layer = LayerMask.NameToLayer("Barrel");
            _barrelHorizontalSpeed = barrelInitialHorizontalSpeed;
            barrelDescendLadderSpeed = 0;
            _isMovingLeft = false;
            _isMovingRight = true;
        }

        if (other.CompareTag("Above Ladder") && _funBarrelValue == 2)
        {
            gameObject.layer = LayerMask.NameToLayer("Passable Objects");
            _barrelHorizontalSpeed = 0;
            barrelDescendLadderSpeed = 1;
        }
    }
}