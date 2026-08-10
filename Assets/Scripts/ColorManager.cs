using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ColorManager : Singleton<ColorManager>
{
    public List<Material> materials;

    public List<ColorSetup> colorSetups;

    public void ChangeColorByType(ArtManager.ArtType artType)
    {
        var setup = colorSetups.Find(x => x.artType == artType);

        for(int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_BaseColor", setup.colors[i]);
        }
    }
}

[System.Serializable]   

public class ColorSetup
{
        public ArtManager.ArtType artType;
        public List<Color> colors;  
}
