using UnityEngine;

public class ItemsView : MonoBehaviour
{
    private Parameter _evolution;
    private MaterialSwitcher[] _switchers;
    private float _distanceToSwitchOn = 135f;

    private int _valueAge;

    private void Awake()
    {
        GetSwitchers();
    }

    private void Update()
    {
        foreach (var switcher in _switchers)
            if (switcher.GetDistance() < _distanceToSwitchOn)
                switcher.SetMaterialBy((EraType)_valueAge);
    }

    private void OnDestroy()
    {
        _evolution.Changed -= OnChanged;
    }

    public void Initialize(Parameter evolution)
    {
        _evolution = evolution;
        OnChanged(evolution.Value);
        _evolution.Changed += OnChanged;
    }

    private void OnChanged(int value)
    {
        GetSwitchers();
        _valueAge = value;
    }

    private void GetSwitchers()
    {
        _switchers = GetComponentsInChildren<MaterialSwitcher>();
    }
}