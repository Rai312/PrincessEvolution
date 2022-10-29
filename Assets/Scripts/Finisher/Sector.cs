using UnityEngine;

public abstract class Sector : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private NextSectorSpawnPoint _nextSectorSpawnPoint;
    [SerializeField][Min(0)] private float _appearDelay;

    public Transform NextSectorSpawnPoint => _nextSectorSpawnPoint.transform;
    public float AppearDelay => _appearDelay;
    
    public void Show()
    {
        _model.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _model.gameObject.SetActive(false);
    }

    
}