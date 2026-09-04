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
        // Use the attached Slider if one was not assigned manually.
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    private void Start()
    {
        // Match the slider with the current mixer value.
        SyncSliderWithMixer();
    }

    private void SyncSliderWithMixer()
    {
        if (slider == null || group == null)
            return;

        // Read the current exposed mixer parameter.
        if (group.GetFloat(
            floatParam,
            out float currentValue))
        {
            slider.SetValueWithoutNotify(
                currentValue
            );
        }
    }

    public void ChangeValue(float f)
    {
        // Update the mixer parameter using the slider value.
        group.SetFloat(floatParam, f);
    }
}