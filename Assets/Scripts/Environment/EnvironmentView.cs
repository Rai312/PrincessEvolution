using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentView : MonoBehaviour
{
    [SerializeField] private Environment[] _environments;
    [SerializeField][Min(0.1f)] private float _changeDuration;
    private Environment _currentEnvironment;
    private Dictionary<EraType, Environment> _environmentsMap;
    private Parameter _evolution;
    private Camera _camera;

    private void OnDisable()
    {
        if(_evolution == null)
            return;
        _evolution.Changed -= OnChanged;
    }

    public void Initialize(Parameter evolution)
    {
        _camera = Camera.main;
        CreateMap();
        _currentEnvironment = _environmentsMap[EraType.Primeval];
        _evolution = evolution;
        SetEnvironmentBy((EraType) evolution.Value);
        _evolution.Changed += OnChanged;
    }

    private void OnChanged(int value)
    {
        SetEnvironmentBy((EraType) value);
    }

    private void SetEnvironmentBy(EraType type)
    {
        if (type < EraType.Primeval)
            type = EraType.Primeval;
        _currentEnvironment.Hide();
        _currentEnvironment = _environmentsMap[type];
        _currentEnvironment.Show();
        StartCoroutine(ChangingTo(_environmentsMap[type].Color));
    }

    private void CreateMap()
    {
        _environmentsMap = new Dictionary<EraType, Environment>();
        foreach (var environment in _environments)
            _environmentsMap.Add(environment.Type, environment);
    }

    private IEnumerator ChangingTo(Color targetColor)
    {
        var startColor = _camera.backgroundColor;

        for (float i = 0; i < 1; i += Time.deltaTime/_changeDuration)
        {
            var color = Color.Lerp(startColor, targetColor, i);
            _camera.backgroundColor = color;
            RenderSettings.fogColor = color;
            yield return null;
        }

        _camera.backgroundColor = targetColor;
        RenderSettings.fogColor = targetColor;
    }
}