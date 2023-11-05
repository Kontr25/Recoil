using DG.Tweening;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class BloodParticle : MonoBehaviour
    {
        [SerializeField] private float _scalingTime;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private ParticleSystem _trail;
        private Material _material;
        private bool _isActivate = false;

        private void OnValidate()
        {
            _trail = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            _material = _meshRenderer.material;
            var mainModule = _trail.main;
            mainModule.startColor = _material.color;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyBlockBlood block))
            {
                block.Activate(_material);
                transform.DOScale(Vector3.zero, _scalingTime).onComplete = () =>
                {
                    Destroy(gameObject);
                };
            }
        }
    }
}