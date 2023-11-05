using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Scripts.Levels
{
    public class LevelCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Transform _textTransform;
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _firstMoveSpeed;
        [SerializeField] private float _midleMoveSpeed;
        [SerializeField] private float _lastMoveSpeed;
        
        
        public void Move(int level)
        {
            _levelText.text = level.ToString();
            StartCoroutine(MoveRoutine());
        }

        private IEnumerator MoveRoutine()
        {
            yield return new WaitForSeconds(.3f);
            _textTransform.DOMove(_points[0].position, _firstMoveSpeed).SetEase(Ease.InExpo).onComplete = () =>
            {
                _textTransform.position = _points[0].position;
                _textTransform.DOMove(_points[1].position, _midleMoveSpeed).SetEase(Ease.Linear).onComplete = () =>
                {
                    _textTransform.position = _points[1].position;
                    _textTransform.DOMove(_points[2].position, _lastMoveSpeed).SetEase(Ease.OutExpo).onComplete = () =>
                    {
                        _textTransform.position = _points[2].position;
                    };
                };
            };
        }
    }
}