public class FinisherState : IState
{
    private readonly Player _player;
    private readonly UI _uI;

    public FinisherState(UI uI, Player player)
    {
        _uI = uI;
        _player = player;
    }

    public void Enter()
    {
        _uI.FinisherMenu.Show();
        _player.StopMove();
        _player.SetEnergyBarActive(false);
        _player.Coins.Changed += OnCoinsChanged;
        _player.AnimatorController.Finisher();
        _player.Finisher.ShowFinisher(_player, () =>
        {
            _player.OnEndLevel();
        });
    }

    public void Exit()
    {
        _uI.FinisherMenu.Hide();
        _player.Coins.Changed -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int value)
    {
        _uI.FinisherMenu.CoinText.text = value.ToString();
    }
}