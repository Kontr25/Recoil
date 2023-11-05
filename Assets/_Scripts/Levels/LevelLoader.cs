using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Levels;
using _Scripts.Player;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private int _levelForLoad;
    [SerializeField] private OverlayImage _overlayImage;
    [SerializeField] private LevelCounter _levelCounter;
    [SerializeField] private PlayerSkinController _playerSkinController;

    private int _currentLevel;
    private Level _currentLvl;

    private void Awake()
    {
        YandexGame.GetDataEvent += GetData;
        
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }

    private void OnDestroy()
    {
        YandexGame.GetDataEvent -= GetData;
    }

    public void GetData()
    {
        _playerSkinController.SetSkin(YandexGame.savesData.currentSkin);
#if UNITY_EDITOR
        SetLevel();
#endif
        _currentLevel = YandexGame.savesData.currentLevel;
        _levelCounter.Move(_currentLevel + 1);
        
        LoadLevel();
    }

    private void LoadLevel()
    {
        if (_levels.Count >= _currentLevel)
        {
            _currentLvl = Instantiate(_levels[_currentLevel], Vector3.zero, Quaternion.identity);
        }

        if (_currentLvl != null)
        {
            _playerController.transform.position = _currentLvl.PlayerPosition();
        }
        
        Time.timeScale = 1;
    }

    public void ClearSaves()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
        RestartLevel();
    }

    private void SetLevel()
    {
        if (_levelForLoad != 0)
            YandexGame.savesData.currentLevel = _levelForLoad - 1;
    }

    public void LevelPassed()
    {
        _currentLevel++;

        if (_levels.Count <= _currentLevel)
            _currentLevel = 0;
        YandexGame.savesData.currentLevel = _currentLevel;
        YandexGame.SaveProgress();
    }

    public void LoadNextLevel()
    {
        RestartLevel();
    }

    private IEnumerator RestartRoutine()
    {
        _overlayImage.FadeImage();
        yield return new WaitForSeconds(_overlayImage.FadeInDuration);
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        YandexGame.SaveProgress();
        StartCoroutine(RestartRoutine());
    }
}
