using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioChangeVolume : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam = "MyExposedParam";

    public Slider slider;

    private void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    private void Start()
    {
        SyncSliderWithMixer();
    }

    private void SyncSliderWithMixer()
    {
        if (slider == null || group == null)
            return;

        if (group.GetFloat(floatParam, out float currentValue))
        {
            slider.SetValueWithoutNotify(currentValue);
        }

    }

    public void ChangeValue(float f)
    {
        group.SetFloat(floatParam, f);
    }
}