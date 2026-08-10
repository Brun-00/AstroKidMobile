using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ArtManager : Singleton<ArtManager>
{
    public enum ArtType
    {
        DAY,
        NIGHT
    }

    public List<ArtSetup> artSetups;

    public ArtSetup GetSetupByType(ArtType artType)
    {
        return artSetups.Find(x => x.artType == artType);
    }


}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject gameObject;

    public Material skybox;
}

