using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class BloodExplosion : MonoBehaviour
    {
        [SerializeField] private List<Rigidbody> _bloodParticles;
        [SerializeField] private float _explosionForceMin;
        [SerializeField] private float _explosionForceMax;
        [SerializeField] private float _explosionRadius;

        public void Explosion()
        {
            for (int i = 0; i < _bloodParticles.Count; i++)
            {
                _bloodParticles[i].gameObject.SetActive(true);
                float explosionForce = Random.Range(_explosionForceMin, _explosionForceMax);
                _bloodParticles[i].AddExplosionForce(explosionForce, transform.position, _explosionRadius);
                _bloodParticles[i].AddTorque(transform.forward * Random.Range(-1, 2) * 50);
            }
        }
    }
}