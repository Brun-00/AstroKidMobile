using UnityEngine;
using Assets.Scripts;

public class BackgroundManager : Singleton<BackgroundManager>
{
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    private void Start()
    {
        SetRandomSkybox();
    }

    private void SetRandomSkybox()
    {
        Material selectedSkybox =
            Random.value < 0.5f
            ? daySkybox
            : nightSkybox;

        RenderSettings.skybox = selectedSkybox;
        DynamicGI.UpdateEnvironment();
    }
}