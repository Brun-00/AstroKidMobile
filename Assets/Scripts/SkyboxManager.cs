using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    private void Start()
    {
        SetRandomSkybox();
    }

    private void SetRandomSkybox()
    {
        Material selectedSkybox;

        if (Random.value < 0.5f)
        {
            selectedSkybox = daySkybox;
        }
        else
        {
            selectedSkybox = nightSkybox;
        }

        RenderSettings.skybox = selectedSkybox;
        DynamicGI.UpdateEnvironment();
    }
}