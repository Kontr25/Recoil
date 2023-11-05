using System.Collections.Generic;
using _Scripts.Player;
using UnityEngine;
using YG;

namespace _Scripts.UI
{
    public class SkinWindow : MonoBehaviour
    {
        [SerializeField] private List<SkinCell> _allSkins;
        [SerializeField] private PlayerSkinController _playerSkinController;

        public void ChangeSkin()
        {
            for (int i = 0; i < _allSkins.Count; i++)
            {
                if (_allSkins[i].IsClicked)
                {
                    _allSkins[i].IsClicked = false;
                    _allSkins[i].ChangeSkin();
                    _playerSkinController.SetSkin(YandexGame.savesData.currentSkin);
                }
                _allSkins[i].CheckCurrentSkin();
            }
        }
    }
}