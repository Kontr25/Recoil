using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private bool _unsubscribeOnDisable;
        
        private TMP_Text _text;
        
        public string LocalizationKey;

        private void Start()
        {
            Localize();
        }

        private void OnEnable()
        {
            LocalizationManager.LocalizationChanged += Localize;
        }

        private void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void OnDisable()
        {
            if (_unsubscribeOnDisable)
            {
                LocalizationManager.LocalizationChanged -= Localize;
            }
        }

        private void Localize()
        {
            try
            {
                _text = GetComponent<TMP_Text>();
                _text.text = LocalizationManager.Localize(LocalizationKey);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}