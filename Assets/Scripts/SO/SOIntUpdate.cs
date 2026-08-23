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
        text.text = soInt.value.ToString();
    }

    public void Update()
    {
        text.text = soInt.value.ToString();
    }
}

