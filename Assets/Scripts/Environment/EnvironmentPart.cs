using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class EnvironmentPart : MonoBehaviour
{
    [SerializeField] private AnimationCurve _appear;
    [SerializeField] private AnimationCurve _disappear;
    [SerializeField] [Min(0.1f)] private float _duration;

    private Vector3 _defaultPosition;

    private void Start()
    {
        _defaultPosition = transform.position;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(Moving(_appear, null));
    }

    public void Hide()
    {
        if (isActiveAndEnabled)
            StartCoroutine(Moving(_disappear, () => gameObject.SetActive(false)));
    }

    private IEnumerator Moving(AnimationCurve curve, Action stopped)
    {
        for (float i = 0; i < 1; i += Time.deltaTime / _duration)
        {
            var positionY = _defaultPosition.y + curve.Evaluate(i);
            transform.position = new Vector3(_defaultPosition.x, positionY, _defaultPosition.z);

            yield return null;
        }

        transform.position = _defaultPosition;
        stopped?.Invoke();
    }
}