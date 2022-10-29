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
        //Debug.Log("Тут нужно запустить анимацию проигрыша: _player.AnimatorController.Fail()");
        _player.Finisher.ShowFail();
        _player.AnimatorController.EndLevel();
    }

    public void Exit()
    {
        _ui.FailMenu.Hide();
    }
}