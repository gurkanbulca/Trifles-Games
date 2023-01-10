using DG.Tweening;
using UnityEngine;

namespace MiniGame
{
    public class PaperBouncer : Bouncer
    {
        #region Serialized Fields

        [Header("------ Paper -------")] [SerializeField]
        private float torqueMultiplier;

        [SerializeField] private float maxPeriod;
        [SerializeField] private float minVerticalVelocityToRotate = 5f;

        #endregion

        #region Private Fields

        private float _angularVelocity;
        private float _period;
        private Tween _tween;

        #endregion

        #region Properties

        protected override Vector3 direction { get; } = new(1, 1, 0);

        #endregion

        #region Unity LifeCycle

        protected override void Awake()
        {
            base.Awake();
            Transform = transform.GetChild(0);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Append angular velocity by Shaker's x parameter of velocity vector.
        /// </summary>
        protected override void Bounce()
        {
            base.Bounce();
            if (Shaker.velocity.y < minVerticalVelocityToRotate)
                return;
            _angularVelocity = Shaker.velocity.x * torqueMultiplier;
            _period = 2 * Mathf.PI / Mathf.Abs(_angularVelocity);
            if (_period > maxPeriod)
                return;
            _period = Mathf.Max(.5f, _period);
            RotateWithTween();
        }

        /// <summary>
        /// Recursive method for rotate transform by calculated angular velocity.
        /// Recursion stops when transform start to fall. 
        /// </summary>
        private void RotateWithTween()
        {
            Transform.DORotate(new Vector3(0, 0, (_angularVelocity < 0 ? 1f : -1f) * 360), _period,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetUpdate(UpdateType.Fixed)
                .OnComplete(
                    () =>
                    {
                        if (Rigidbody.velocity.y > 0)
                            RotateWithTween();
                    });
        }

        #endregion
    }
}