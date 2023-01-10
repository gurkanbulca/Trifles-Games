using UnityEngine;

namespace MiniGame
{
    public class BallBouncer : Bouncer
    {
        #region Properties

        protected override Vector3 direction { get; } = new Vector3(1, -1, 0);

        #endregion

        #region Unity LifeCycle

        private void OnEnable()
        {
            Rigidbody.velocity = Vector3.up * 5f;
        }

        #endregion

        
    }
}