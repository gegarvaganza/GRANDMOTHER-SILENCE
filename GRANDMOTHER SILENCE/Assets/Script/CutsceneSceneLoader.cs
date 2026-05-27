using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneSceneLoader : MonoBehaviour
{
    public PlayableDirector director;
    public string gameplayScene;

    void Start()
    {
        director.stopped += OnCutsceneFinished;
    }

    void OnCutsceneFinished(PlayableDirector pd)
    {
        SceneManager.LoadScene(gameplayScene);
    }
}