using DG.Tweening;
using UnityEngine;

public class ItemOfClothing : MonoBehaviour
{
    [SerializeField] private float _durationDecreaseComponent = 0.10f;
    [SerializeField] private float _durationIncreaseComponent = 0.2f;
    [SerializeField] private float _maxValueScale = 2.5f;
    [SerializeField] private float _minValueScale = 1f;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        InitParticleSystem();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        PlayParticle();
        Scaling();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void Scaling()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(_maxValueScale, _durationIncreaseComponent));
        sequence.Append(transform.DOScale(_minValueScale, _durationDecreaseComponent));
    }

    private void PlayParticle()
    {
        _particleSystem.Play();
    }

    private void InitParticleSystem()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
}