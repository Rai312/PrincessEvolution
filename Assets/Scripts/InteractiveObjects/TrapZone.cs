using UnityEngine;

public class TrapZone : MonoBehaviour
{
    [SerializeField] private int _energyValue;
    [SerializeField] private float _perTime;

    public int EnergyValue => _energyValue;
    public float PerTime => _perTime;
}