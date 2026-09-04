using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    private void Start()
    {
        // Choose a random skybox when the scene starts.
        SetRandomSkybox();
    }

    private void SetRandomSkybox()
    {
        Material selectedSkybox;

        // Randomly choose between the day and night skyboxes.
        if (Random.value < 0.5f)
        {
            selectedSkybox = daySkybox;
        }
        else
        {
            selectedSkybox = nightSkybox;
        }

        // Apply the selected skybox and refresh the environment lighting.
        RenderSettings.skybox = selectedSkybox;
        DynamicGI.UpdateEnvironment();
    }
}