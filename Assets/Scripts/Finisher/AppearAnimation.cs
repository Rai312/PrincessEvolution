using System.Collections;
using UnityEngine;

public class AppearAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private AnimationCurve _curveY;
    [SerializeField] private AnimationCurve _curveZ;
    [SerializeField] [Min(0.01f)] private float _duration;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Appearing());
    }

    private IEnumerator Appearing()
    {
        for (float i = 0; i < 1; i += Time.deltaTime / _duration)
        {
            var offset = new Vector3(0, _curveY.Evaluate(i), _curveZ.Evaluate(i));

            _model.transform.localPosition = offset;

            yield return null;
        }

        _model.transform.localPosition = Vector3.zero;
    }
}