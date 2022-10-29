using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject _coinPrefab;

    private bool _canSpawn = false;

    public void MakeActive()
    {
        _canSpawn = true;
    }
    
    // Used in sector appear animation
    private void Spawn()
    {
        if(_canSpawn == false)
            return;
        
        foreach (var point in _spawnPoints) Instantiate(_coinPrefab, point.transform.position, Quaternion.identity);
    }
}