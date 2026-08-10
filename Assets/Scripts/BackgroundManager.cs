using UnityEngine;
using Assets.Scripts;

public class BackgroundManager : Singleton<BackgroundManager>
{
    public void ChangeSkybox(Material skyboxMaterial)
    {
        RenderSettings.skybox = skyboxMaterial;


        DynamicGI.UpdateEnvironment();
    }
}