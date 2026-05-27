using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroCutsceneLoader : MonoBehaviour
{
    public PlayableDirector director;
    public string gameplayScene;

    void Start()
    {
        director.stopped += OnCutsceneFinished;

        director.Play();
    }

    void OnCutsceneFinished(PlayableDirector pd)
    {
        Debug.Log("CUTSCENE FINISHED");

        SceneManager.LoadScene(gameplayScene);
    }
}