using UnityEngine;

[SelectionBase]
public class CheckPoint : MonoBehaviour
{
    [SerializeField] private EraType _type;

    public EraType Type => _type;
}