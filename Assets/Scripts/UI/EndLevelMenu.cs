using TMPro;
using UnityEngine;

public class EndLevelMenu : Menu
{
    [SerializeField] private TMP_Text _totalCoinsText;

    public void ShowTotalCoinsText(int value)
    {
        _totalCoinsText.text = value.ToString();
    }
}