using DG.Tweening;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIMover : MonoBehaviour
    {
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private float _moveDuration;

        private Vector3 _defaultPosition;

        private void Start()
        {
            _defaultPosition = transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y + 2000, transform.position.z);
        }

        public void Move()
        {
            transform.position = _defaultPosition;
            transform.DOMove(_targetPoint.position, _moveDuration);
        }
    }
}