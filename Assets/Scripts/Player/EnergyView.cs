using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnergyView : MonoBehaviour
{
    private const string Change = "Change";
    
    [SerializeField] private Slider[] _sliders;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationCurve _chengingCurve;
    [SerializeField] private float _chengingDuration;
    
    private Coroutine _changingValue;
    private EvolutionView _evolutionView;
    private Slider _currentSlider;

    private Parameter _energy;
    private Parameter _evolution;
    private float _sliderCapacity;

    public event Action<float> SliderChanged;

    private void OnDisable()
    {
        if (_changingValue == null)
            return;

        StopCoroutine(_changingValue);
    }

    private void OnDestroy()
    {
        _energy.Changed -= OnEnergyChanged;
        _evolution.Changed -= OnEvolutionChanged;
    }

    public void Initialize(EvolutionView evolutionView, Parameter evolution, Parameter energy, int sliderCapacity)
    {
        _evolutionView = evolutionView;
        _evolution = evolution;
        _energy = energy;
        _sliderCapacity = sliderCapacity;
        OnEvolutionChanged(evolution.Value);
        OnEnergyChanged(energy.Value);
        evolution.Changed += OnEvolutionChanged;
        _energy.Changed += OnEnergyChanged;
    }

    private void OnEnergyChanged(int value)
    {
        var currentValue = (value - _sliderCapacity * _evolution.Value) / _sliderCapacity;

        _evolutionView.CurrentModel.CheckModelPartsBy(currentValue);
        _animator.SetTrigger(Change);

        if (_changingValue != null)
            StopCoroutine(_changingValue);

        StartCoroutine(ChangingSliderTo(currentValue));
    }

    private void OnEvolutionChanged(int eraType)
    {
        SetSliderBy((EraType) eraType);
        OnEnergyChanged(_energy.Value);
    }

    private void SetSliderBy(EraType type)
    {
        DisableSliders();
        _sliders[(int) type].gameObject.SetActive(true);
        _currentSlider = _sliders[(int) type];
    }

    private void DisableSliders()
    {
        foreach (var slider in _sliders)
            slider.gameObject.SetActive(false);
    }

    private IEnumerator ChangingSliderTo(float targetValue)
    {
        var startValue = _currentSlider.value;

        for (float i = 0; i < 1; i += Time.deltaTime / _chengingDuration)
        {
            _currentSlider.value = Mathf.Lerp(startValue, targetValue, _chengingCurve.Evaluate(i));
            SliderChanged?.Invoke(_currentSlider.value);
            yield return null;
        }

        _currentSlider.value = targetValue;
    }
}