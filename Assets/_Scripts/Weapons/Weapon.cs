using System.Collections;
using _Scripts.Player;
using _Scripts.PoolObject;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float _recoilForce;
        [SerializeField] private PlayerController _player;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private Transform _container;
        [SerializeField] private int _pollCapacity;
        [SerializeField] private Transform _recoilPoint;
        [SerializeField] private float _recoilDuration;
        [SerializeField] private float _delayBetweenShots;
        
        private Coroutine _shotCoroutine;
        private WaitForSeconds _shotDelay;
        private Sequence _sequence;
        private Pool<Bullet> _pool;
        private Vector3 _defaultLocalPosition;

        public float RecoilForce => _recoilForce;

        private void Awake()
        {
            _pool = new Pool<Bullet>(_bulletPrefab, _pollCapacity, _container)
            {
                AutoExpand = true
            };
            _shotDelay = new WaitForSeconds(_delayBetweenShots);
            _defaultLocalPosition = transform.localPosition;
        }

        public void StartShot()
        {
            Shot();
        }

        public void Shot()
        {
            Recoil();
            _player.Recoil();
            var bullet = _pool.GetFreeElement();
            bullet.transform.SetParent(_container);
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.localRotation = Quaternion.identity;
            bullet.transform.SetParent(null);
            bullet.StartShot();
        }

        private void Recoil()
        {
            if (_sequence != null)
            {
                _sequence.Kill();
                _sequence = null;
            }

            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOLocalMove(_recoilPoint.localPosition, _recoilDuration/2)).SetEase(Ease.Linear);
            _sequence.Append(transform.DOLocalMove(_defaultLocalPosition, _recoilDuration)).SetEase(Ease.Linear);
        }
    }
}
