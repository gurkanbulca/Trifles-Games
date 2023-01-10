using Extensions;
using GameCore;
using UnityEngine;

namespace ShakerSystem
{
    public class Shaker : MonoBehaviour
    {
        #region SerializedFields

        [SerializeField] private Vector2 verticalClamp;
        [SerializeField] private Vector2 horizontalClamp;
        [SerializeField] private float sensitivity;
        [SerializeField] private float returnSpeed;

        #endregion

        #region Private Fields

        private Vector3 _startPosition;

        private readonly Vector3 _zeroVector3 = Vector3.zero;

        #endregion

        #region Properties

        public Vector3 velocity { get; private set; }

        #endregion

        #region Unity LifeCycle

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

        #endregion

        #region Helper Methods

        /// <summary>
        /// When mouse button is not holding, lerp self position to start position by return speed.
        /// </summary>
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

        /// <summary>
        /// Move itself through clamped coordinates my mouse drag.
        /// </summary>
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

        #endregion
    }
}