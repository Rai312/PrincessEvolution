using TMPro;
using UnityEngine;

public class FinisherMenu : Menu
{
    [SerializeField] private TMP_Text _coinText;

    public TMP_Text CoinText => _coinText;
}