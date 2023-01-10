using UnityEngine;

public abstract class Bouncer : MonoBehaviour
{
    [Header("------ Bouncer -------")] [SerializeField]
    protected float horizontalMultiplier;

    [SerializeField] protected float verticalMultiplier;
    [SerializeField] private Vector2 verticalClamp;
    [SerializeField] private Vector2 horizontalClamp;


    private Transform _transform;
    private Vector3 _startPosition;
    protected Shaker Shaker;
    protected Rigidbody Rigidbody;

    private const string ShakerTag = "Shaker";

    protected abstract Vector3 direction { get; }


    protected virtual void Awake()
    {
        _transform = transform;
        Shaker = FindObjectOfType<Shaker>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Initialize();
    }


    private void OnDisable()
    {
        ReturnToStartPosition();
    }

    private void Initialize()
    {
        _startPosition = _transform.position;
    }

    protected virtual void Bounce()
    {
        var force = Shaker.velocity;
        force.x *= horizontalMultiplier * direction.x;
        force.y *= verticalMultiplier * direction.y;
        force.y = Mathf.Clamp(force.y, verticalClamp.x, verticalClamp.y);
        force.x = Mathf.Clamp(force.x + Rigidbody.velocity.x, horizontalClamp.x, horizontalClamp.y);
        Rigidbody.velocity = force;
    }

    private void ReturnToStartPosition()
    {
        _transform.position = _startPosition;
        _transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ShakerTag))
            Bounce();
    }
}