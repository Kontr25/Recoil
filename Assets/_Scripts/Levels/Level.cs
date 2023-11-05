using _Scripts.Environment;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _playerPositionPoint;
    [SerializeField] private LevelColor _levelColor;

    private void Start()
    {
        _playerPositionPoint.gameObject.SetActive(false);
        BackController.Instance.SetColor(_levelColor);
    }

    public Vector3 PlayerPosition()
    {
        return _playerPositionPoint.position;
    }
}
