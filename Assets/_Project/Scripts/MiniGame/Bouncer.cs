using ShakerSystem;
using UnityEngine;

namespace MiniGame
{
    public abstract class Bouncer : MonoBehaviour
    {
        #region Serialized Fields

        [Header("------ Bouncer -------")] [SerializeField]
        protected float horizontalMultiplier;

        [SerializeField] protected float verticalMultiplier;
        [SerializeField] private Vector2 verticalClamp;
        [SerializeField] private Vector2 horizontalClamp;

        #endregion

        #region Protected Fields

        protected Transform Transform;
        protected Shaker Shaker;
        protected Rigidbody Rigidbody;

        #endregion

        #region Private Fields

        private Vector3 _startPosition;

        private const string ShakerTag = "Shaker";

        #endregion

        #region Properties

        protected abstract Vector3 direction { get; }

        #endregion

        #region Unity LifeCycle

        protected virtual void Awake()
        {
            Transform = transform;
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

        #endregion

        #region Helper Methods

        private void Initialize()
        {
            _startPosition = Transform.position;
        }

        /// <summary>
        /// Calculates bounce velocity by shaker velocity.
        /// </summary>
        protected virtual void Bounce()
        {
            var force = Shaker.velocity;
            force.x *= horizontalMultiplier * direction.x;
            force.y *= verticalMultiplier * direction.y;
            force.y = Mathf.Clamp(force.y, verticalClamp.x, verticalClamp.y);
            force.x = Mathf.Clamp(force.x + Rigidbody.velocity.x, horizontalClamp.x, horizontalClamp.y);
            Rigidbody.velocity = force;
        }

        private void ReturnToStartPosition()
        {
            Transform.position = _startPosition;
            Transform.rotation = Quaternion.identity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(ShakerTag))
                Bounce();
        }

        #endregion
    }
}