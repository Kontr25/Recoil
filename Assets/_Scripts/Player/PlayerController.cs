using System.Collections.Generic;
using _Scripts.Block;
using _Scripts.Enemy;
using _Scripts.Weapons;
using DG.Tweening;
using UnityEngine;
using YG;

namespace _Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _impulceForce;
        [SerializeField] private List<Transform> _disabledTransforms;
        [SerializeField] private float _scalingTime;
        [SerializeField] private AudioSource _deathSound;

        private Weapon _weapon;
        private BloodExplosion _bloodExplosion;
        private bool _onGround;
        private bool _isDeath;

        public Weapon CurrentWeapon
        {
            get => _weapon;
            set => _weapon = value;
        }

        public BloodExplosion PlayerBloodExplosion
        {
            get => _bloodExplosion;
            set => _bloodExplosion = value;
        }
        
        private void Awake()
        {
            YandexGame.GetDataEvent += GetData;
        
            if (YandexGame.SDKEnabled == true)
            {
                GetData();
            }
        }

        private void OnDestroy()
        {
            YandexGame.GetDataEvent -= GetData;
        }

        public void GetData()
        {
            _rigidbody.isKinematic = false;
            YandexGame.GameReadyAPI();
        }

        public void MouseDown()
        {
            if(_isDeath) return;
            _weapon.StartShot();
        }

        public void Recoil()
        {
            _rigidbody.AddForce(-transform.right * _impulceForce, ForceMode.Impulse);
            if (CheckGrounded())
            {
                float recoilForce = 0;

                if (transform.right.x < .2f && transform.right.x >= 0)
                {
                    recoilForce = _weapon.RecoilForce * .2f;
                }
                else if(transform.right.x > -.2f && transform.right.x <= 0)
                {
                    recoilForce = _weapon.RecoilForce * -.2f;
                }
                else
                {
                    recoilForce = _weapon.RecoilForce * transform.right.x;
                }
                //_rigidbody.AddTorque(transform.forward * recoilForce, ForceMode.Impulse);
                _rigidbody.AddExplosionForce(50, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), 2 );
            }
            else
            {
                _rigidbody.angularVelocity = Vector3.zero;
            }
        }
        
        private bool CheckGrounded()
        {
            bool isHit = Physics.CheckSphere(transform.position, _sphereCollider.radius, _groundLayer);

            return isHit;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Trap trap))
            {
                Death();
            }
        }

        private void Death()
        {
            if(_isDeath || FinishAction.IsGameOver) return;

            _deathSound.Play();
            FinishAction.Finish.Invoke(FinishAction.FinishType.Lose);
            _isDeath = true;
            _bloodExplosion.Explosion();

            for (int i = 0; i < _disabledTransforms.Count; i++)
            {
                if (_disabledTransforms[i].gameObject.activeInHierarchy)
                {
                    _disabledTransforms[i].DOScale(Vector3.zero, _scalingTime);
                }
            }

            _rigidbody.isKinematic = true;
        }
    }
}