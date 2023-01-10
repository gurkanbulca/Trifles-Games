using UnityEngine;

namespace GameCore
{
    public class InputController
    {
        private Vector3 _previousMousePosition;

        private Vector2 _drag;
        private readonly Vector2 _zeroDrag = new(0, 0);

        public InputController()
        {
            _drag = new Vector2();
        }

        public void Update(float updatePeriod)
        {
            if (Input.GetMouseButtonDown(0))
                _previousMousePosition = Input.mousePosition;

            CalculateDrag(updatePeriod);
        }

        private void CalculateDrag(float updatePeriod)
        {
            _drag.x = Input.mousePosition.x - _previousMousePosition.x;
            _drag.y = Input.mousePosition.y - _previousMousePosition.y;
            _drag *= updatePeriod;
            _previousMousePosition = Input.mousePosition;
        }

        public Vector2 GetDrag() => Input.GetMouseButton(0) ? _drag : _zeroDrag;
    }
}