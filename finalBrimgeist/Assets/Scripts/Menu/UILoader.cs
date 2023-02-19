using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
        if (SceneManager.GetSceneByName("UI").isLoaded == false)
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        else SceneManager.UnloadSceneAsync("UI");
    }

    public static void GoToScene(int index)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive).completed +=
            x => SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(index));
    }
}
