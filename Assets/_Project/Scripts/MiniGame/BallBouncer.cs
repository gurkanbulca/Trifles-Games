using Extensions;
using UnityEngine;

namespace MiniGame
{
    public class BallBouncer : Bouncer
    {
        #region Private Fields

        private const string FloorTag = "Floor";

        #endregion

        #region Properties

        protected override Vector3 direction { get; } = new Vector3(1, -1, 0);

        #endregion

        #region Unity LifeCycle

        private void OnEnable()
        {
            Rigidbody.velocity = Vector3.up * 5f;
        }

        #endregion

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if (!collision.gameObject.CompareTag(FloorTag)) return;

            if (Rigidbody.velocity.y < 5)
                Rigidbody.velocity = Rigidbody.velocity.WithY(4.5f);
        }
    }
}