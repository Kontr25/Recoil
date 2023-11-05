using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.UI
{
    public class WinEffects : MonoBehaviour, IFinishable
    {
        [SerializeField] private List<Transform> _effectsPoints;
        [SerializeField] private List<ParticleSystem> _effects;
        [SerializeField] private float _effectsPlayDelay;

        private WaitForSeconds _delay;

        private void Start()
        {
            _delay = new WaitForSeconds(_effectsPlayDelay);
        }

        public void StartActionOnWin()
        {
            StartCoroutine(EffectsPlayRoutine());
        }

        private IEnumerator EffectsPlayRoutine()
        {
            for (int i = 0; i < _effects.Count; i++)
            {
                Transform _targetTransform = _effectsPoints[Random.Range(0, _effectsPoints.Count)];
                _effects[i].transform.position = _targetTransform.position;
                _effectsPoints.Remove(_targetTransform);
                _effects[i].Play();
                yield return _delay;
            }
        }
        
        public void StartActionOnLose()
        {
            
        }
    }
}