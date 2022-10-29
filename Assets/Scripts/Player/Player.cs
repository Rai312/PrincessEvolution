using System;
using System.Collections;
using RunnerMovementSystem;
using RunnerMovementSystem.Examples;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int EvolutionStep = 1;

    [SerializeField] private MouseInput _mouseInput;
    [SerializeField] private FinisherMover _finisherMover;
    [SerializeField] private MovementSystem _movementSystem;
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private EnergyView _energyView;
    [SerializeField] private EvolutionView _evolutionView;
    [SerializeField] private EnvironmentView _environmentView;
    [SerializeField] private ItemsView _itemsView;
    [SerializeField] private Data _data;
    [SerializeField] private int _startEnergy;
    [SerializeField] private EraType _startEvolution;
    [SerializeField] private EraType _evolutionLimit;
    [SerializeField] [Min(1)] private int _energyToEvolve;
    [SerializeField] [Min(0)] private float _finishingMoveSpeed;

    private Parameter _energy;
    private Coroutine _energyChangingCoroutine;
    private Parameter _evolution;

    public Finisher Finisher { get; private set; }
    public int Scores => _energy.Value;
    public EraType EvolutionLevel => (EraType) _evolution.Value;
    public PrincessAnimatorController AnimatorController => _evolutionView.AnimatorController;
    public FinisherMover FinisherMover => _finisherMover;
    public Parameter Coins { get; private set; }
    public Data Data => _data;

    private void Awake()
    {
        _energy = new Parameter(_startEnergy + _energyToEvolve * (int)_startEvolution);
        _evolution = new Parameter((int) _startEvolution);
        Coins = new Parameter(0);
        _evolutionView.Initialize(_evolution);
        _energyView.Initialize(_evolutionView, _evolution, _energy, _energyToEvolve);
        _environmentView.Initialize(_evolution);
        _itemsView.Initialize(_evolution);
    }

    private void OnEnable()
    {
        _collisionHandler.ItemTaken += OnItemTaken;
        _collisionHandler.GateTaken += OnGateTaken;
        _collisionHandler.EnemyTaken += OnEnemyTaken;
        _collisionHandler.FinishTriggerTaken += OnFinishTriggerTaken;
        _collisionHandler.EndLevelTaken += OnEndLevel;
        _collisionHandler.CoinTaken += OnCoinTaken;
        _collisionHandler.TrapZoneEnter += OnTrapZoneEnter;
        _collisionHandler.TrapZoneExit += OnTrapZoneExit;
        _energyView.SliderChanged += OnSliderChanged;
    }

    private void OnDisable()
    {
        _collisionHandler.ItemTaken -= OnItemTaken;
        _collisionHandler.GateTaken -= OnGateTaken;
        _collisionHandler.EnemyTaken -= OnEnemyTaken;
        _collisionHandler.FinishTriggerTaken -= OnFinishTriggerTaken;
        _collisionHandler.EndLevelTaken -= OnEndLevel;
        _collisionHandler.CoinTaken -= OnCoinTaken;
        _collisionHandler.TrapZoneEnter -= OnTrapZoneEnter;
        _collisionHandler.TrapZoneExit -= OnTrapZoneExit;
        _energyView.SliderChanged -= OnSliderChanged;
    }

    public event Action FinishTaken;
    public event Action GameFailed;
    public event Action LevelEnded;

    private void OnItemTaken(Item item)
    {
        if ((int) item.Type > _evolution.Value)
        {
            var eraDifference = (int) item.Type - _evolution.Value;
            var scores = item.Value * eraDifference;
            _energy.Add(scores);
            CheckEnergyToEvolve();
        }
        else if ((int) item.Type < _evolution.Value)
        {
            if (_energy.Value - item.Value <= 0)
                _energy.Add(-_energy.Value);
            else
                _energy.Add(-item.Value);
            CheckEnergyToEvolve();
        }
        else
        {
            _energy.Add(item.Value);   
        }
    }

    private void OnGateTaken(Gate gate)
    {
        if (_energy.Value + gate.Value <= 0)
            _energy.Add(-_energy.Value);
        _energy.Add(gate.Value);
        CheckEnergyToEvolve();
    }

    private void OnEnemyTaken(Enemy enemy)
    {
        if (_energy.Value + enemy.Value <= 0)
            _energy.Add(-_energy.Value);
        _energy.Add(enemy.Value);
        CheckEnergyToEvolve();
    }

    private void OnFinishTriggerTaken(Finisher finisher)
    {
        if (EvolutionLevel == EraType.Monkey)
        {
            Finisher = finisher;
            GameFailed?.Invoke();
            return;
        }
        
        Finisher = finisher;
        FinishTaken?.Invoke();
    }

    public void OnEndLevel()
    {
        LevelEnded?.Invoke();
    }

    private void OnCoinTaken(int value)
    {
        Coins.Add(value);
    }

    private void OnTrapZoneEnter(int value, float perTime)
    {
        if (_energyChangingCoroutine != null)
            StopCoroutine(_energyChangingCoroutine);
        _energyChangingCoroutine = StartCoroutine(EnergyChanging(value, perTime));
    }

    private void OnTrapZoneExit()
    {
        StopCoroutine(_energyChangingCoroutine);
    }

    private void OnSliderChanged(float value)
    {
        //Debug.Log("Тут нужно раскоменить и передать значение слайдера в нужный параметр аниматора.");
        //Debug.Log(value);
        //_evolutionView.CurrentModel.AnimatorController.Set(value);
        _evolutionView.CurrentModel.AnimatorController.SetProgress(value);
    }

    private void CheckEnergyToEvolve()
    {
        if (_energy.Value >= _energyToEvolve * (_evolution.Value + 1))
        {
            if (_evolution.Value < (int) EraType.Future && (EraType) _evolution.Value < _evolutionLimit)
                _evolution.Add(EvolutionStep);
        }
        else if (_energy.Value < _energyToEvolve * _evolution.Value)
        {
            if (_evolution.Value > (int) EraType.Monkey)
                _evolution.Add(-EvolutionStep);
        }
    }

    public void StartMove()
    {
        _mouseInput.enabled = true;
        _movementSystem.enabled = true;
    }

    public void StopMove()
    {
        _mouseInput.enabled = false;
        _movementSystem.enabled = false;
    }

    public void SetEnergyBarActive(bool isActive)
    {
        _energyView.gameObject.SetActive(isActive);
    }

    public void MoveTo(Vector3 target, Action stopped)
    {
        StartCoroutine(MovingTo(target, stopped));
    }

    private IEnumerator MovingTo(Vector3 target, Action stopped)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target,
                _finishingMoveSpeed * Time.deltaTime);
            yield return null;
        }

        stopped?.Invoke();
    }

    private IEnumerator EnergyChanging(int value, float perTime)
    {
        var time = perTime;
        while (_energy.Value > 0)
        {
            if (time >= perTime)
            {
                _energy.Add(value);
                CheckEnergyToEvolve();
                time -= perTime;
            }

            time += Time.deltaTime;
            yield return null;
        }
    }
}