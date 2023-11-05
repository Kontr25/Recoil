using System.Collections;
using _Scripts.Block;
using _Scripts.Enemy;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionEffect;
        [SerializeField] private float _disableTime;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _meshGameObject;

        private Coroutine _shotCoroutine;
        private Coroutine _disableCoroutine;
        private WaitForSeconds _disableDelay;
        private bool _isCanMove = false;
        private bool _isCanDamage = true;

        private void Start()
        {
            _disableDelay = new WaitForSeconds(_disableTime);
        }

        private void StartDisable()
        {
            _explosionEffect.Play();
            StopDisable();
            _disableCoroutine = StartCoroutine(DisableCoroutine());
        }

        private IEnumerator DisableCoroutine()
        {
            yield return _disableDelay;
            gameObject.SetActive(false);
        }

        private void StopDisable()
        {
            if (_disableCoroutine != null)
            {
                StopCoroutine(_disableCoroutine);
                _disableCoroutine = null;
            }
        }

        public void StartShot()
        {
            StopShot();
            _isCanDamage = true;
            _meshGameObject.SetActive(true);
            _isCanMove = true;
            _rigidbody.isKinematic = false;
            _shotCoroutine = StartCoroutine(ShotCoroutine());
        }

        private void StopShot()
        {
            _meshGameObject.SetActive(false);
            _rigidbody.isKinematic = true;
            _isCanMove = false;
            if (_shotCoroutine != null)
            {
                StopCoroutine(_shotCoroutine);
                _shotCoroutine = null;
            }
        }

        private IEnumerator ShotCoroutine()
        {
            while (_isCanMove)
            {
                _rigidbody.velocity = transform.right * _speed;
                yield return new WaitForFixedUpdate();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!_isCanDamage) return;

            _isCanDamage = false;
            if (other.TryGetComponent(out EnemyController enemy))
            {
                enemy.Death();
            }
            
            if (other.TryGetComponent(out DestroyableBlock block))
            {
                block.DestroyBlock();
            }
            
            StopShot();
            StartDisable();
        }
    }
}