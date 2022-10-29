public class EndLevelState : IState
{
    private readonly UI _uI;
    private readonly Player _player;

    public EndLevelState(UI uI, Player player)
    {
        _uI = uI;
        _player = player;
    }

    public void Enter()
    {
        SaveProgress();
        ShowTotalCoins();
        _uI.EndLevelMenu.Show();
        _player.AnimatorController.EndLevel();
    }

    public void Exit()
    {
        _uI.EndLevelMenu.Hide();
    }

    private void SaveProgress()
    {
        var totalCoins = _player.Data.GetCurrentCoins();
        totalCoins += _player.Coins.Value;
        _player.Data.SetCurrentCoins(totalCoins);
    }

    private void ShowTotalCoins()
    {
        var result = _player.Data.GetCurrentCoins();
        _uI.EndLevelMenu.ShowTotalCoinsText(result);
    }
}