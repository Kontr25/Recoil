using System;
using System.Collections.Generic;
using _Scripts.Player;
using Cinemachine;
using UnityEngine;

public class FinishAction : MonoBehaviour
{
    public static Action<FinishType> Finish;
    public static bool IsGameOver = false;
    [SerializeField] private List<GameObject> _finishableObjects;
    [SerializeField] private ParticleSystem[] _confetti;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private CinemachineVirtualCamera _skinChangerCamera;
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        IsGameOver = false;
        Finish += Activate;
    }
    
    private void OnDestroy()
    {
        Finish -= Activate;
    }

    public void Activate(FinishType finishType = FinishType.None)
    {
        IsGameOver = true;
        _skinChangerCamera.Priority = 100;
        if (_finishableObjects.Count > 0)
        {
            switch (finishType)
            {
                case FinishType.Win:
                    
                    _levelLoader.LevelPassed();
                    
                    foreach (var obj in _finishableObjects)
                    {
                        if (obj.TryGetComponent(out IFinishable finishable))
                            finishable.StartActionOnWin();
                    }

                    for (int i = 0; i < _confetti.Length; i++)
                    {
                        _confetti[i].Play();
                    }
                    break;

                case FinishType.Lose:
                    foreach (var obj in _finishableObjects)
                    {
                        if (obj.TryGetComponent(out IFinishable finishable))
                            finishable.StartActionOnLose();
                    }
                    break;

                default:
                    break;
            }
        }
    }
    

    public enum FinishType
    {
        None,
        Win,
        Lose
    }
}