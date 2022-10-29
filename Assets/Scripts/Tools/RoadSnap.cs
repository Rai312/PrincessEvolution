using UnityEngine;

#if UNITY_EDITOR
public class RoadSnap : MonoBehaviour
{
    private MeshCollider _mesh;

    private void Awake()
    {
        Destroy(_mesh);
    }

    private void Reset()
    {
        AddMesh();
    }

    private void AddMesh()
    {
        _mesh = GetComponent<MeshCollider>();
        if (_mesh)
            DestroyImmediate(_mesh);
        _mesh = gameObject.AddComponent<MeshCollider>();
    }
}
#endif