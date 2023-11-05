using DG.Tweening;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyBlockBlood : MonoBehaviour
    {
        [SerializeField] private float _scalingTime;
        [SerializeField] private MeshRenderer _mesh;

        public void Activate(Material material)
        {
            _mesh.material = material;
            _mesh.enabled = true;
            _mesh.transform.DOScale(Vector3.one * 1.6f, _scalingTime);
        }
    }
}