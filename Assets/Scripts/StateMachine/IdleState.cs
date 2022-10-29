public class IdleState : IState
{
    private readonly Player _player;
    private readonly UI _uI;

    public IdleState(UI uI, Player player)
    {
        _uI = uI;
        _player = player;
    }

    public void Enter()
    {
        _uI.MainMenu.Show();
        _player.StopMove();
        _player.AnimatorController.Idle();
    }

    public void Exit()
    {
        _uI.MainMenu.Hide();
    }
}