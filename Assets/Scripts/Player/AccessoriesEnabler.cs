using UnityEngine;

public class AccessoriesEnabler : MonoBehaviour
{
    [SerializeField] private ItemOfClothing[] _linkedItemOfClothing;

    private void OnEnable()
    {
        if (_linkedItemOfClothing.Length == 0)
            return;

        foreach (var item in _linkedItemOfClothing)
            item.Enable();
    }

    private void OnDisable()
    {
        if (_linkedItemOfClothing.Length == 0)
            return;

        foreach (var item in _linkedItemOfClothing) item.Disable();
    }
}