using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerManager : Singleton<SceneControllerManager>
{
    public void AsyncLoadingScene(string SceneName)
    {
        LoadingScene.LoadScene(SceneName);
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}