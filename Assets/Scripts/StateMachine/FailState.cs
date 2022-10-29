using UnityEngine;

public class FailState : IState
{
    private readonly Player _player;
    private readonly UI _ui;

    public FailState(UI uI, Player player)
    {
        _ui = uI;
        _player = player;
    }

    public void Enter()
    {
        _ui.FailMenu.Show();
        _player.StopMove();
        _player.AnimatorController.EndLevel();

    }

    public void Exit()
    {
        _ui.FailMenu.Hide();
    }
}