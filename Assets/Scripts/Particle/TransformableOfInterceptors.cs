using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransformableOfInterceptors : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speedRotation;

    private float _durationIncreaseScaling = 1.5f;
    private float _durationReduceScaling = 1.0f;
    private float _minScaleValue = 0f;
    private float _maxScaleValue = 1f;
    private float _elapsedTime = 0;
    private float _durationRotation = 4f;

    public void IncreaseScale()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).DOScale(_maxScaleValue, _durationIncreaseScaling);
        }
    }

    public void ReduceScale()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).DOScale(_minScaleValue, _durationReduceScaling);
        }
    }

    public IEnumerator Rotate()
    {
        while (_durationRotation > _elapsedTime)
        {
            _elapsedTime += Time.deltaTime;

            transform.Rotate(_rotation * _speedRotation * Time.deltaTime);
            yield return null;
        }
        _elapsedTime = 0;
        yield break;
    }
}
