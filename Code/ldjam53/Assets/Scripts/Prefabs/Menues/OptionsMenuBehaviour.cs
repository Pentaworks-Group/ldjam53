using Assets.Scripts.Base;


using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuBehaviour : MonoBehaviour
{
    public Slider EffectsVolumeSlider;
    public Slider AmbienceVolumeSlider;
    public Slider BackgroundVolumeSlider;
    private Slider moveSensivitySlider;
    private Slider zoomSensivitySlider;
    private Toggle sideScrollingEnabledToggle;

    private void Awake()
    {
        moveSensivitySlider = transform.Find("OptionContainer/MoveSensivity/Right/Slider").GetComponent<Slider>();
        zoomSensivitySlider = transform.Find("OptionContainer/ZoomSensivity/Right/Slider").GetComponent<Slider>();

        sideScrollingEnabledToggle = transform.Find("OptionContainer/EnableSideScroll/Right/Toggle").GetComponent<Toggle>();
    }

    private void Start()
    {
        UpdateValues();
    }

    private void FixedUpdate()
    {
        UpdateValues();
    }

    public void OnEffectsVolumeSliderChanged()
    {
        if (Core.Game?.Options != default)
        {
            Core.Game.Options.EffectsVolume = EffectsVolumeSlider.value;
        }
    }

    public void OnAmbienceVolumeSliderChanged()
    {
        if (Core.Game?.Options != default)
        {
            Core.Game.Options.AmbienceVolume = AmbienceVolumeSlider.value;
        }
    }

    public void OnBackgroundVolumeSliderChanged()
    {
        if (Core.Game?.Options != default)
        {
            Core.Game.Options.BackgroundVolume = BackgroundVolumeSlider.value;
        }
    }

    public void OnMoveSensivitySliderChanged()
    {
        if (Core.Game?.Options != default && (this.moveSensivitySlider != null))
        {
            Core.Game.Options.MoveSensivity = this.moveSensivitySlider.value;
        }
    }

    public void OnZoomSensivitySliderChanged()
    {
        if ((Core.Game?.Options != default) && (zoomSensivitySlider != null))
        {
            Core.Game.Options.ZoomSensivity = zoomSensivitySlider.value;
        }
    }

    public void OnSideScrollEnabledToggleValueChanged()
    {
        if (Core.Game?.Options != default && this.sideScrollingEnabledToggle != null)
        {
            Core.Game.Options.IsMouseScreenEdgeScrollingEnabled = this.sideScrollingEnabledToggle.isOn;
        }
    }

    public void OnMobileInterfaceValueChanged(Toggle t)
    {
        if (t.isOn)
        {
            //TextMeshProUGUI text = t.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            //Core.Game.Options.MobileInterface = text.text;
        }
    }

    public void OnRestoreDefaultsClick()
    {
        Core.Game.PlayButtonSound();

        Core.Game.RestoreDefaultOptions();

        UpdateValues();
    }

    public void SaveOptions()
    {
        Core.Game.SaveOptions();
    }

    private void UpdateValues()
    {
        if (Core.Game.Options != default)
        {
            if (this.EffectsVolumeSlider.value != Core.Game.Options.EffectsVolume)
            {
                this.EffectsVolumeSlider.value = Core.Game.Options.EffectsVolume;
            }

            if (this.AmbienceVolumeSlider.value != Core.Game.Options.AmbienceVolume)
            {
                this.AmbienceVolumeSlider.value = Core.Game.Options.AmbienceVolume;
            }

            if (this.BackgroundVolumeSlider.value != Core.Game.Options.BackgroundVolume)
            {
                this.BackgroundVolumeSlider.value = Core.Game.Options.BackgroundVolume;
            }

            if (this.moveSensivitySlider.value != Core.Game.Options.MoveSensivity)
            {
                this.moveSensivitySlider.value = Core.Game.Options.MoveSensivity;
            }

            if (this.zoomSensivitySlider.value != Core.Game.Options.ZoomSensivity)
            {
                this.zoomSensivitySlider.value = Core.Game.Options.ZoomSensivity;
            }

            if (this.sideScrollingEnabledToggle.isOn != Core.Game.Options.IsMouseScreenEdgeScrollingEnabled)
            {
                this.sideScrollingEnabledToggle.isOn = Core.Game.Options.IsMouseScreenEdgeScrollingEnabled;
            }
        }
    }
}
