using UnityEngine;
using Assets.Scripts;

public class BackgroundManager : Singleton<BackgroundManager>
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
        // Randomly select between the day and night environments.
        Material selectedSkybox =
            Random.value < 0.5f
            ? daySkybox
            : nightSkybox;

        // Apply the selected skybox and refresh environment lighting.
        RenderSettings.skybox = selectedSkybox;
        DynamicGI.UpdateEnvironment();
    }
}