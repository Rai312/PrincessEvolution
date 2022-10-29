using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDirection;
    [SerializeField][Min(0)] private float _delay;
    private float _speed;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
            return;
        }
        
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
    }

    public void ElevateBy(float speed)
    {
        _speed = speed;
        enabled = true;
    }

    public void StopElevate()
    {
        enabled = false;
    }
}