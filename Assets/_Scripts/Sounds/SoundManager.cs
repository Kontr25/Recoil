using System;
using UnityEngine;

namespace _Scripts.Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _victory;
        [SerializeField] private AudioSource _defeat;
        
        public static Action VictorySound;
        public static Action DefeatSound;

        private void OnEnable()
        {
            VictorySound += Victory;
            DefeatSound += Defeat;
        }

        private void OnDestroy()
        {
            VictorySound -= Victory;
            DefeatSound -= Defeat;
        }

        private void Victory()
        {
            _victory.Play();
        }
        
        private void Defeat()
        {
            _victory.Play();
        }
    }
}