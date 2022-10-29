using UnityEngine;

namespace RunnerMovementSystem.Examples
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private MovementSystem _roadMovement;
        [SerializeField] private float _sensitivity = 0.01f;
        [SerializeField] private Player _player;

        private Vector3 _currentMousePosition;
        private float _saveOffset;
        private Vector3 _lastMousePosition;
        private float _speedSwipe;

        public bool IsMoved { get; private set; }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _saveOffset = _roadMovement.Offset;
                _currentMousePosition = Input.mousePosition;
                IsMoved = true;

                _lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                _speedSwipe = ((Input.mousePosition.x - _lastMousePosition.x) / Time.deltaTime) / Screen.width;

                if (_speedSwipe > 0.15f || _speedSwipe < -0.15f)
                {
                    _player.AnimatorController.SetSpeedSwipe(_speedSwipe);
                }

                var offset = Input.mousePosition - _currentMousePosition;
                _roadMovement.SetOffset(_saveOffset + offset.x * _sensitivity);
                _lastMousePosition = Input.mousePosition;

            }
            if (Input.GetMouseButtonUp(0))
            {
                _speedSwipe = 0;
                _player.AnimatorController.SetSpeedSwipe(_speedSwipe);
            }


            if (Input.GetMouseButtonUp(0)) IsMoved = false;
            
            _roadMovement.MoveForward();
        }

        private void OnEnable()
        {
            _roadMovement.PathChanged += OnPathChanged;
        }

        private void OnDisable()
        {
            _roadMovement.PathChanged -= OnPathChanged;
        }

        private void OnPathChanged(PathSegment _)
        {
            _saveOffset = _roadMovement.Offset;
            _currentMousePosition = Input.mousePosition;
        }
    }
}