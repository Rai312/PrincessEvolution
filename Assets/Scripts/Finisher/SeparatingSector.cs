using UnityEngine;

public class SeparatingSector : Sector
{
    [SerializeField] private PlayerPlacePoint _playerPlacePoint;
    [SerializeField] private GameObject _startNextLadderPoint;
    [SerializeField] private GameObject[] _eraViews;
    
    public Vector3 PlayerPlacePoint => _playerPlacePoint.transform.position;
    public Vector3 StartNextLadderPoint => _startNextLadderPoint.transform.position;

    public void SetViewBy(EraType eraType)
    {
        for (var i = 0; i < _eraViews.Length; i++)
        {
            _eraViews[i].SetActive(i == (int) eraType);
        }
    }
}