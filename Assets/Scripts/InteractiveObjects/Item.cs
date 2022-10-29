using UnityEngine;

[SelectionBase]
public class Item : MonoBehaviour
{
    [SerializeField] [Min(0)] private int _value;
    [SerializeField] private EraType _type;

    private ModelsOfItem _modelsOfItem;
    public int Value => _value;
    public EraType Type => _type;

    private void Awake()
    {
        _modelsOfItem = GetComponentInChildren<ModelsOfItem>();
    }
    public void Disable()
    {
        _modelsOfItem.gameObject.SetActive(false);
    }
}

public enum EraType
{
    Monkey,
    Primeval,
    Medieval,
    Modernity,
    Future
}