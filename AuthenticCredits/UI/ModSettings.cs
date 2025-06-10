using System;
using System.ComponentModel;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Settings;
using AuthenticCredits;
using UnityEngine;
using Zenject;
using Colour = UnityEngine.Color;

namespace AuthenticCredits.UI
{
    public class ModSettings : MonoBehaviour, IInitializable, INotifyPropertyChanged
    {
        [Inject]
        private Config? config;
        private GameplaySetupViewController? gameplaySetupViewController;
        public event PropertyChangedEventHandler PropertyChanged = null!;

        [UIValue("enabled")]
        public bool enabled
        {
            get => config.enabled;
            set
            {
                config.enabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(enabled)));
            }
        }

        public void Initialize()
        {
            BSMLSettings.Instance.AddSettingsMenu("AuthenticCredits", "AuthenticCredits.UI.ModSettings.bsml", this);
        }
    }
}
