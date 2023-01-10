using UnityEngine;

namespace GameCore
{
    public class InputController
    {
        #region Private Fields

        private Vector3 _previousMousePosition;
        private Vector2 _drag;
        private readonly Vector2 _zeroDrag = new(0, 0);

        #endregion

        #region Constructor

        public InputController()
        {
            _drag = new Vector2();
        }

        #endregion

        #region Public Methods

        public void Update(float updatePeriod)
        {
            if (Input.GetMouseButtonDown(0))
                _previousMousePosition = Input.mousePosition;

            CalculateDrag(updatePeriod);
        }

        public Vector2 GetDrag() => Input.GetMouseButton(0) ? _drag : _zeroDrag;

        #endregion

        #region Helper Methods

        /// <summary>
        /// Calculates drag for update period.
        /// </summary>
        /// <param name="updatePeriod"></param>
        private void CalculateDrag(float updatePeriod)
        {
            _drag.x = Input.mousePosition.x - _previousMousePosition.x;
            _drag.y = Input.mousePosition.y - _previousMousePosition.y;
            _drag *= updatePeriod;
            _previousMousePosition = Input.mousePosition;
        }

        #endregion
    }
}