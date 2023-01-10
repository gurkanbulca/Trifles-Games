using DG.Tweening;
using UnityEngine;

namespace MiniGame
{
    public class PaperBouncer : Bouncer
    {
        [Header("------ Paper -------")] [SerializeField]
        private float torqueMultiplier;

        [SerializeField] private float maxPeriod;
        [SerializeField] private float minVerticalVelocityToRotate = 5f;


        private float _angularVelocity;
        private float _period;
        private Tween _tween;

        protected override Vector3 direction { get; } = new(1, 1, 0);


        protected override void Awake()
        {
            base.Awake();
            Transform = transform.GetChild(0);
        }

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
    }
}