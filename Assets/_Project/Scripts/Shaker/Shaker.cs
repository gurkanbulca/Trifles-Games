using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] private Vector2 verticalClamp;
    [SerializeField] private Vector2 horizontalClamp;
    [SerializeField] private float sensitivity;
    [SerializeField] private float returnSpeed;

    private Vector3 _startPosition;

    private readonly Vector3 _zeroVector3 = Vector3.zero;

    public Vector3 velocity { get; private set; }

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
            Move();
        else
        {
            ReturnToStartPosition();
        }
    }

    private void ReturnToStartPosition()
    {
        var direction = (_startPosition - transform.position);
        if (direction.sqrMagnitude < .01f)
        {
            velocity = _zeroVector3;
            return;
        }

        velocity = returnSpeed * direction.normalized;
        transform.Translate(velocity * Time.fixedDeltaTime);
    }

    private void Move()
    {
        var drag = GameManager.Instance.GetDrag() * sensitivity;
        var position = transform.position;
        var targetX = Mathf.Clamp(position.x + drag.x, _startPosition.x + horizontalClamp.x,
            _startPosition.x + horizontalClamp.y);
        var targetY = Mathf.Clamp(position.y + drag.y, _startPosition.y + verticalClamp.x,
            _startPosition.y + verticalClamp.y);
        var targetPosition = position.WithX(targetX).WithY(targetY);
        velocity = (targetPosition - position) / Time.fixedDeltaTime;
        transform.position = targetPosition;
    }
}