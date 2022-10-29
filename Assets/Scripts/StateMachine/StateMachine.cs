using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private UI _uI;
    [SerializeField] private Player _player;
    
    private Dictionary<Type, IState> _statesMap;
    private IState _currentState;

    private void Awake()
    {
        InitStates();
    }

    private void OnEnable()
    {
        _uI.MainMenu.StartButton.onClick.AddListener(SetPlayState);
        _uI.PlayMenu.SettingsButton.onClick.AddListener(SetPauseState);
        _uI.SettingsMenu.ResumeButton.onClick.AddListener(SetPlayState);
        _player.FinishTaken += SetFinisherState;
        _player.LevelEnded += SetEndlevelState;
        _player.GameFailed += SetFailState;
    }

    private void OnDisable()
    {
        _uI.MainMenu.StartButton.onClick.RemoveListener(SetPlayState);
        _uI.PlayMenu.SettingsButton.onClick.RemoveListener(SetPauseState);
        _uI.SettingsMenu.ResumeButton.onClick.RemoveListener(SetPlayState);
        _player.FinishTaken -= SetFinisherState;
        _player.LevelEnded -= SetEndlevelState;
        _player.GameFailed -= SetFailState;
    }

    private void Start()
    {
        SetStateByDefault();
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<Type, IState>();

        _statesMap[typeof(IdleState)] = new IdleState(_uI, _player);
        _statesMap[typeof(PlayState)] = new PlayState(_uI, _player);
        _statesMap[typeof(PauseState)] = new PauseState(_uI, _player);
        _statesMap[typeof(FinisherState)] = new FinisherState(_uI, _player);
        _statesMap[typeof(EndLevelState)] = new EndLevelState(_uI, _player);
        _statesMap[typeof(FailState)] = new FailState(_uI, _player);
    }

    private void SetIdleState()
    {
        var state = GetState<IdleState>();
        SetState(state);
    }

    private void SetPlayState()
    {
        var state = GetState<PlayState>();
        SetState(state);
    }

    private void SetPauseState()
    {
        var state = GetState<PauseState>();
        SetState(state);
    }

    private void SetFinisherState()
    {
        var state = GetState<FinisherState>();
        SetState(state);
    }

    private void SetEndlevelState()
    {
        var state = GetState<EndLevelState>();
        SetState(state);
    }

    private void SetFailState()
    {
        var state = GetState<FailState>();
        SetState(state);
    }

    private void SetStateByDefault()
    {
        SetIdleState();
    }
    
    private void SetState(IState newState)
    {
        if(_currentState != null)
            _currentState.Exit();

        _currentState = newState;
        _currentState.Enter();
    }

    private IState GetState<T>() where T : IState
    {
        var type = typeof(T);
        return _statesMap[type];
    }
}