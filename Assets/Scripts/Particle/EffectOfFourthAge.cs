using UnityEngine;
using DG.Tweening;

public class EffectOfFourthAge : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private TransformableOfInterceptors _transformableOfInterceptors;

    private float _intervalBetweenRotateAndParticle = 1f;
    private float _intervalBetweenParticleAndReduce = 0.5f;

    public void StartEffect()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() =>
        {
            _transformableOfInterceptors.IncreaseScale();
            StartCoroutine(_transformableOfInterceptors.Rotate());
        });

        sequence.AppendInterval(_intervalBetweenRotateAndParticle);
        sequence.AppendCallback(() =>
        {
            _particleSystem.Play();
        });

        sequence.AppendInterval(_intervalBetweenParticleAndReduce);
        sequence.AppendCallback(() =>
        {
            _transformableOfInterceptors.ReduceScale();
        });
    }
}
