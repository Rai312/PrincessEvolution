using UnityEngine;

public class Model : MonoBehaviour
{
    [SerializeField] private PrincessAnimatorController _animatorController;
    [SerializeField] private EraType _type;
    [SerializeField] private ItemOfClothing[] _components;

    public PrincessAnimatorController AnimatorController => _animatorController;
    public EraType Type => _type;

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void CheckModelPartsBy(float value)
    {
        if (_components.Length == 0)
            return;

        var _cutCount = _components.Length + 2;
        
        var _cut = 1.0f / _cutCount;

        for (var i = 0; i < _components.Length; i++)
            if (value >= _cut * (i + 1))
                _components[i].Enable();
            else
                _components[i].Disable();
    }
}