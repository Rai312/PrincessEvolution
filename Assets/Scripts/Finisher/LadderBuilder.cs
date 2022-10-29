using System.Collections.Generic;
using UnityEngine;

public class LadderBuilder : MonoBehaviour
{
    [SerializeField] private CommonSector _commonSectorPrefab;
    [SerializeField] private SeparatingSector _separatingSectorPrefab;
    [SerializeField] private FinalSector _finalSectorPrefab;
    [SerializeField] private FailSector _failSectorPrefab;
    [SerializeField] [Min(1)] private int _stepPrice;
    [SerializeField] private Material[] _sectorMaterials;
    [SerializeField] private GameObject _startPoint;

    private readonly List<Sector> _sectors = new List<Sector>();
    private readonly List<Vector3> _wayPoints = new List<Vector3>();
    private Transform _spawnPoint;

    public IReadOnlyList<Sector> Sectors => _sectors;
    public IReadOnlyList<Vector3> WayPoints => _wayPoints;
    public FinalSectorContent FinalSectorContent { get; private set; }

    public void CreateLadderBy(Player player)
    {
        var count = player.Scores / _stepPrice;
        var cutCount = (int) player.EvolutionLevel;
        var countInCut = count / cutCount;
        _spawnPoint = transform;
        _wayPoints.Add(_startPoint.transform.position);

        for (var j = 0; j < cutCount; j++)
        {
            for (var i = 0; i < countInCut; i++)
            {
                var sector = Spawn(_commonSectorPrefab);
                sector.SetMaterial(_sectorMaterials[j]);
                sector.Hide();
            }

            if (j < cutCount - 1)
            {
                var sector = Spawn(_separatingSectorPrefab);
                sector.SetViewBy((EraType)j);
                sector.Hide();
                _wayPoints.Add(sector.PlayerPlacePoint);
                _wayPoints.Add(sector.StartNextLadderPoint);
            }
            else
            {
                var sector = Spawn(_finalSectorPrefab);
                sector.SetViewBy(player.EvolutionLevel);
                FinalSectorContent = sector.FinalSectorContent;
                sector.Hide();
                _wayPoints.Add(sector.EndFinishingPlace);
                _wayPoints.Add(sector.EndLevelPlace);
            }
        }
        ForceSectorsSpawnCoins();
    }

    private void ForceSectorsSpawnCoins()
    {
        for (var i = 0; i < _sectors.Count; i++)
        {
            if (_sectors[i] is CommonSector && i < _sectors.Count - 4)
            {
                var sector = (CommonSector) _sectors[i];
                sector.SpawnCoins();
            }
        }
    }

    public void CreateFail()
    {
        _spawnPoint = transform;
        Spawn(_failSectorPrefab);
    }

    private T Spawn<T>(T sectorPrefab) where T : Sector
    {
        var sector = Instantiate(sectorPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation, transform);
        _sectors.Add(sector);
        _spawnPoint = sector.NextSectorSpawnPoint;
        return sector;
    }
}