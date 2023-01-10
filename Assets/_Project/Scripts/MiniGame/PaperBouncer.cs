using UnityEngine;

public class PaperBouncer : Bouncer
{
    [SerializeField] private float torqueLimit;
    [SerializeField] private float torqueMultiplier;


    private bool _isFalling;
    private float _angularVelocity;

    protected override Vector3 direction { get; } = new(1, 1, 0);

    private Vector3 torqueDirection { get; } = Vector3.forward;

    private void FixedUpdate()
    {
    }

    protected override void Bounce()
    {
        base.Bounce();
    }

   
}