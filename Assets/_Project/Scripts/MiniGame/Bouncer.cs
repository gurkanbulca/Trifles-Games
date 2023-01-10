using System;
using UnityEngine;

public abstract class Bouncer : MonoBehaviour
{
    [SerializeField] protected float horizontalMultiplier;
    [SerializeField] protected float verticalMultiplier;
    [SerializeField] private Vector2 verticalClamp;
    [SerializeField] private Vector2 horizontalClamp;


    private Vector3 _startPosition;
    protected Shaker Shaker;
    protected Rigidbody Rigidbody;

    private const string ShakerTag = "Shaker";

    protected abstract Vector3 direction { get; }


    private void Awake()
    {
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
        _startPosition = transform.position;
    }

    protected virtual void Bounce()
    {
        var force = Shaker.velocity;
        force.x *= horizontalMultiplier * direction.x;
        force.y *= verticalMultiplier * direction.y;
        force.y = Mathf.Clamp(force.y, verticalClamp.x, verticalClamp.y);
        force.x = Mathf.Clamp(force.x, horizontalClamp.x, horizontalClamp.y);
        Rigidbody.velocity = force;
    }

    private void ReturnToStartPosition()
    {
        transform.position = _startPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(ShakerTag))
            Bounce();
    }
}