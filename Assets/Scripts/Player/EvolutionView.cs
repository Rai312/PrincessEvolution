using System.Collections.Generic;
using UnityEngine;

public class EvolutionView : MonoBehaviour
{
    [SerializeField] private Model[] _models;
    [SerializeField] private EffectOfEvolution _effectOfEvolution;
    [SerializeField] private ChangerTextTypeAge _changerTextTypeAge;
    private Dictionary<EraType, Model> _modelsMap;
    private Parameter _evolution;

    public PrincessAnimatorController AnimatorController => CurrentModel.AnimatorController;
    public Model CurrentModel { get; private set; }

    private void OnDisable()
    {
        _evolution.Changed -= OnChanged;
    }

    public void Initialize(Parameter evolution)
    {
        CreateMap();
        CurrentModel = _modelsMap[EraType.Primeval];
        _evolution = evolution;
        SetViewBy((EraType) _evolution.Value);
        _evolution.Changed += OnChanged;
    }

    private void OnChanged(int value)
    {
        SetViewBy((EraType) value);
        _changerTextTypeAge.TryChange((EraType)value);
        CurrentModel.AnimatorController.Evolution();
        _effectOfEvolution.PlayEffect((EraType)value);
    }

    private void SetViewBy(EraType type)
    {
        CurrentModel.SetActive(false);
        CurrentModel = _modelsMap[type];
        CurrentModel.SetActive(true);
    }

    private void CreateMap()
    {
        _modelsMap = new Dictionary<EraType, Model>();
        foreach (var model in _models)
        {
            _modelsMap.Add(model.Type, model);
            model.SetActive(false);
        }
    }
}