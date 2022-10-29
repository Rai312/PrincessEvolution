using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterialSwitcher : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private Player _player;
    [SerializeField] private SkinnedMeshRenderer _mesh;

    private float _distanceToSwitchOn = 80f;

    private void Update()
    {
        SetMaterialBy();
    }

    private void SetMaterialBy()
    {
        if (GetDistance() < _distanceToSwitchOn)
        {
                _mesh.material = _materials[1];
        }
        else
        {
                _mesh.material = _materials[0];
        }
    }

    private float GetDistance()
    {
        return Vector3.Distance(_player.transform.position, transform.position);
    }
}
