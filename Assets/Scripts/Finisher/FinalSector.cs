using System.Collections.Generic;
using UnityEngine;

public class FinalSector : Sector
{
    [SerializeField] private FinalSectorContent[] _finalSectorContents;
    [SerializeField] private GameObject _endFinishingPlace;
    [SerializeField] private GameObject _endLevelPlace;

    private Dictionary<EraType, FinalSectorContent> _contentsMap;

    public FinalSectorContent FinalSectorContent { get; private set; }
    public Vector3 EndFinishingPlace => _endFinishingPlace.transform.position;
    public Vector3 EndLevelPlace => _endLevelPlace.transform.position;


    private void Awake()
    {
        CreateMap();
    }

    public void SetViewBy(EraType type)
    {
        FinalSectorContent = _contentsMap[type];
        _contentsMap[type].SetActive(true);
    }

    private void CreateMap()
    {
        _contentsMap = new Dictionary<EraType, FinalSectorContent>();

        for (var i = 0; i < _finalSectorContents.Length; i++)
        {
            _contentsMap.Add((EraType) i + 1, _finalSectorContents[i]);
            _finalSectorContents[i].SetActive(false);
        }
    }
}