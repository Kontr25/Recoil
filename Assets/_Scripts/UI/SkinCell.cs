using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _Scripts.UI
{
    public class SkinCell : MonoBehaviour
    {
        [SerializeField] private YandexGame _yaSDK;
        [SerializeField] private int _skinNumber;
        [SerializeField] private Button _watchAddButton;
        [SerializeField] private GameObject _currentSkinIcon;

        private bool _isClicked = false;

        public bool IsClicked
        {
            get => _isClicked;
            set => _isClicked = value;
        }
        
        private void OnEnable() => YandexGame.GetDataEvent += CheckCurrentSkin;
        private void OnDisable() => YandexGame.GetDataEvent -= CheckCurrentSkin;
        
        private void Awake()
        {
            if (YandexGame.SDKEnabled == true)
            {
                CheckCurrentSkin();
            }
        }

        public void CheckCurrentSkin()
        {
            if (YandexGame.savesData.currentSkin == _skinNumber)
            {
                _watchAddButton.gameObject.SetActive(false);
                _currentSkinIcon.SetActive(true);
            }
            else
            {
                _watchAddButton.gameObject.SetActive(true);
                _currentSkinIcon.SetActive(false);
            }
        }

        private void Start()
        {
            _watchAddButton.onClick.AddListener(WatchAdd);
        }

        private void OnDestroy()
        {
            _watchAddButton.onClick.RemoveListener(WatchAdd);
        }

        public void WatchAdd()
        {
            _isClicked = true;
            _yaSDK._RewardedShow(1);
        }

        public void ChangeSkin()
        {
            YandexGame.savesData.currentSkin = _skinNumber;
            YandexGame.SaveProgress();
            CheckCurrentSkin();
        }
    }
}