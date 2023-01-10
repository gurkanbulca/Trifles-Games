using UnityEngine;

public class BallBouncer : Bouncer
{
    protected override Vector3 direction { get; } = new Vector3(1,-1,0);

    private void OnEnable()
    {
        Rigidbody.velocity = Vector3.up * 5f;
    }
}