using ModularEventArchitecture;
using UnityEngine;
using UnityEngine.UI;

public class SoundModule : ModuleBase
{
    //-----------------------------------------------------------
    [Information("Этот модуль отвечает за воспроизведение музыки. Если его удалить, то музыка не будет воспроизводиться.", InformationAttribute.InformationType.Info, false)]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Slider _volumeSlider;
    //!-----------------------------------------------------------

    public override void Initialize()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.1f);
        if (_audioSource != null)
        {
            _audioSource.volume = savedVolume;
            _audioSource.Play();
        }

        if (_volumeSlider != null)
        {
            _volumeSlider.value = savedVolume;
            _volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        if (_audioSource != null)
            _audioSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }
}