using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load(int i)
    {
        Time.timeScale = 1f;
        SceneFader.Instance.LoadSceneWithFade(i);
    }

    public void load(string s)
    {
        SceneFader.Instance.LoadSceneWithFade(s);
    }
}

