using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : Menu
{
    [SerializeField] private Button _settingsButton;

    public Button SettingsButton => _settingsButton;
}