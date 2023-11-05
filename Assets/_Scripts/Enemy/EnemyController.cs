using _Scripts.Camera;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private GameObject _meshGameobject;
        [SerializeField] private BloodExplosion _bloodExplosion;
        [SerializeField] private BoxCollider _mainCollider;
        [SerializeField] private AudioSource _deathSound;

        private bool _isDeath = false;

        private void Start()
        {
            EnemiesCounter.Instance.AddEnemy(this);
        }

        public void Death()
        {
            if(_isDeath) return;
            _deathSound.Play();
            _isDeath = true;
            EnemiesCounter.Instance.RemoveEnemy(this);
            _meshGameobject.SetActive(false);
            _bloodExplosion.Explosion();
            _mainCollider.enabled = false;
            CameraEffects.Instance.Shake();
        }
    }
}