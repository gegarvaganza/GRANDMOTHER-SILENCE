using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UHFPS.Runtime;

public class SceneLoader : MonoBehaviour
{
    public BackgroundFader backgroundFader;
    public string sceneName = "LevelManager";

    public void LoadScene()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        yield return backgroundFader.StartBackgroundFade(false);

        SceneManager.LoadScene(sceneName);
    }
}