using UnityEngine;

public class CommonSector : Sector
{
    [SerializeField] private MeshRenderer _sectorRenderer;
    [SerializeField] private CoinSpawner _coinSpawner;

    public void SpawnCoins()
    {
        _coinSpawner.MakeActive();
    }

    public void SetMaterial(Material material)
    {
        _sectorRenderer.material = material;
    }
}