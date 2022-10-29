using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenLoader : MonoBehaviour
{
    private static ScreenLoader _instance;

    [SerializeField] private int _newBeginLevel;
    [SerializeField] private Slider _loadingProgressBar;

    private AsyncOperation _loadingSceneOperation;

    private void Start()
    {
        _instance = this;
        var playerData = Storage.Load();
        
        if (playerData.CurrentLevel >= SceneManager.sceneCountInBuildSettings)
        {
            playerData.CurrentLevel = _newBeginLevel;
            Storage.Save(playerData);
        }
        
        SwitchToScene(playerData.CurrentLevel);
    }

    private void Update()
    {
        if (_loadingSceneOperation == null)
            return;
        _loadingProgressBar.value = _loadingSceneOperation.progress;
    }

    private static void SwitchToScene(int sceneIndex)
    {
        if (sceneIndex < 1)
            throw new ArgumentException("BuildSettings doesn't contain level scenes.");
        
        _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
    }
}