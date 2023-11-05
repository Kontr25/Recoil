using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemiesCounter : MonoBehaviour
    {
        public static EnemiesCounter Instance;

        [SerializeField] private List<EnemyController> _enemies;

        private void Awake()
        {
            _enemies = new List<EnemyController>();
            if (Instance == null)
            {
                transform.SetParent(null);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddEnemy(EnemyController enemy)
        {
            if (!_enemies.Contains(enemy))
            {
                _enemies.Add(enemy);
            }
        }
        
        public void RemoveEnemy(EnemyController enemy)
        {
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);

                if (_enemies.Count < 1)
                {
                    FinishAction.Finish.Invoke(FinishAction.FinishType.Win);
                }
            }
        }
    }
}