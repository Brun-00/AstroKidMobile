using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI text;

    public void Start()
    {
        // Display the initial ScriptableObject value.
        text.text = soInt.value.ToString();
    }

    public void Update()
    {
        // Keep the displayed value synchronized with the ScriptableObject.
        text.text = soInt.value.ToString();
    }
}