using System.Collections;
using UnityEngine;

public class FinisherMover : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 0.01f;
    [SerializeField] private GameObject _playerModel;
    [SerializeField] private float _roadWidth;
    [SerializeField] private float _finishingMoveSpeed;

    private Vector3 _currentMousePosition;
    private Vector3 _currentPlayerModelPosition;

    private bool _isControllable;
    private float _saveOffset;

    private void Awake()
    {
        Disable();
        _currentPlayerModelPosition = Vector3.zero;
    }

    private void Update()
    {
        if (_isControllable == false)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _currentMousePosition = Input.mousePosition;
            _currentPlayerModelPosition = _playerModel.transform.localPosition;
        }

        if (Input.GetMouseButton(0))
        {
            var input = _sensitivity * (Input.mousePosition - _currentMousePosition) / Screen.width;

            var offset = _currentPlayerModelPosition + input;

            var offsetX = Mathf.Clamp(offset.x, -_roadWidth, _roadWidth);

            _playerModel.transform.localPosition = new Vector3(offsetX, 0, 0);
        }
    }

    public void Enable()
    {
        enabled = true;
        _isControllable = true;
        _currentMousePosition = Input.mousePosition;
        _currentPlayerModelPosition = Vector3.zero;
    }

    public void Disable()
    {
        _isControllable = false;
        StartCoroutine(MovingToDefaultPosition());
    }

    private IEnumerator MovingToDefaultPosition()
    {
        while (_playerModel.transform.localPosition != Vector3.zero)
        {
            _playerModel.transform.localPosition =
                Vector3.MoveTowards(_playerModel.transform.localPosition,
                    Vector3.zero, _finishingMoveSpeed * Time.deltaTime);
            yield return null;
        }

        enabled = false;
    }
}