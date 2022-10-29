public class PlayState : IState
{
    private readonly Player _player;
    private readonly UI _uI;

    public PlayState(UI uI, Player player)
    {
        _uI = uI;
        _player = player;
    }

    public void Enter()
    {
        _uI.PlayMenu.Show();
        _player.StartMove();
        //_player.Animator.Play(PlayerAnimator.Run);
        _player.AnimatorController.Run();
    }

    public void Exit()
    {
        _uI.PlayMenu.Hide();
        _player.StopMove();
    }
}