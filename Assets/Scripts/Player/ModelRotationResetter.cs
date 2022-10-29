using UnityEngine;

public class ModelRotationResetter : MonoBehaviour
{
    private void OnDisable()
    {
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}