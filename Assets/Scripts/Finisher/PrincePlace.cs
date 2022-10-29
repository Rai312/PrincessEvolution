using UnityEngine;

public class PrincePlace : MonoBehaviour
{
    [SerializeField] private GameObject[] _prince;

    public void SetPrinceBy(EraType eraType)
    {
        for (var i = 0; i < _prince.Length; i++) _prince[i].gameObject.SetActive((int) eraType == i);
    }
}