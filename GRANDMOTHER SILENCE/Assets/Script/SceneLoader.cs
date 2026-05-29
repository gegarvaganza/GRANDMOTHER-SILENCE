using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneLoader : MonoBehaviour
{
    public GameObject cutsceneObjects;
    public PlayableDirector timeline;

    public string sceneName = "LevelManager";

    private void Start()
    {
        cutsceneObjects.SetActive(false);
    }

    public void LoadScene()
    {
        cutsceneObjects.SetActive(true);

        timeline.stopped += OnTimelineFinished;

        timeline.Play();
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        timeline.stopped -= OnTimelineFinished;

        SceneManager.LoadScene(sceneName);
    }
}