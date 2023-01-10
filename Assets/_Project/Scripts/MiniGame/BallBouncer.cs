using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBouncer : Bouncer
{
    protected override Vector3 direction { get; } = new Vector3(1,-1,0);
}