using UnityEngine;

public class TransformableOfPterodctyles : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speedRotation;
    private void FixedUpdate()
    {
        transform.Rotate(_rotation * _speedRotation * Time.deltaTime);
    }
}
