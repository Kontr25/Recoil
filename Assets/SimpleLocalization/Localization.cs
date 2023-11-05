using System;
using Assets.SimpleLocalization;
using ServiceSystem;
using UnityEngine;
using YG;

namespace LocalizationSimple
{
    public class Localization : IService
    {
        private const string LanguageKey = "Language";

        public int GetCurrentLanguage()
        {
            return YandexGame.savesData.currentLanguageID;
        }

        public void TranslateProject()
        {
            SetNewLanguage(GetCurrentLanguage());
        }

        public void SetNewLanguage(int languageId)
        {
            YandexGame.savesData.currentLanguageID = languageId;
            LocalizationManager.Read();
            Debug.Log(languageId);
            switch (languageId)
            {
                case 0:
                    LocalizationManager.Language = "Russian";
                    break;
                case 1:
                    LocalizationManager.Language = "English";
                    break;
            }
        }
    }
}