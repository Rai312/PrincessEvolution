using UnityEngine;

public class MaterialSwitcher : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private MeshRenderer[] _models;
    [SerializeField] private Material[] _materials;
    [SerializeField] private Player _player;

    public float GetDistance()
    {
        return Vector3.Distance(_player.transform.position, transform.position);
    }

    public void SetMaterialBy(EraType type)
    {
        if(_item.Type < type)
            SetMaterialBy(0);
        else if(_item.Type > type)
        {
            SetMaterialBy(2);
        }
        else
        {
            //SetMaterialBy(1);
            SetMaterialBy(2);
        }
    }

    private void SetMaterialBy(int index)
    {
        foreach (var model in _models)
        {
            model.material = _materials[index];
        }
    }
}