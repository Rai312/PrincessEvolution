using UnityEngine;

[ExecuteInEditMode]
public class ItemSnap : MonoBehaviour
{
    [SerializeField] private float _castDistanceZ;
    [SerializeField] private float _placeDistanceZ;

    private void Awake()
    {
        if(Application.isPlaying)
            Destroy(this);
    }

    private void Update()
    {
        var ray = new Ray(transform.position, Vector3.down * _castDistanceZ);
        Debug.DrawRay(ray.origin, ray.direction * _castDistanceZ, Color.red);

        if (Physics.Raycast(ray, out var hitInfo, _castDistanceZ))
            transform.position = hitInfo.point + Vector3.up * _placeDistanceZ;
    }
}