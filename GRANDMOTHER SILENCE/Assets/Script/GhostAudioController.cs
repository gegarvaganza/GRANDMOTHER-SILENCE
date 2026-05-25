using UnityEngine;
using UnityEngine.AI;

public class GhostAudioController : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Clips")]
    public AudioClip idleClip;
    public AudioClip patrolClip;
    public AudioClip walkClip;
    public AudioClip runClip;

    [Header("AI")]
    public NavMeshAgent agent;

    public enum GhostState
    {
        Idle,
        Patrol,
        Walk,
        Run
    }

    public GhostState currentState;

    void Update()
    {
        UpdateState();
        PlayAudio();
    }

    void UpdateState()
    {
        float speed = agent.velocity.magnitude;

        if (speed < 0.1f)
            currentState = GhostState.Idle;

        else if (speed < 1.5f)
            currentState = GhostState.Patrol;

        else if (speed < 3.5f)
            currentState = GhostState.Walk;

        else
            currentState = GhostState.Run;
    }

    void PlayAudio()
    {
        AudioClip targetClip = null;

        switch (currentState)
        {
            case GhostState.Idle:
                targetClip = idleClip;
                break;

            case GhostState.Patrol:
                targetClip = patrolClip;
                break;

            case GhostState.Walk:
                targetClip = walkClip;
                break;

            case GhostState.Run:
                targetClip = runClip;
                break;
        }

        if (audioSource.clip != targetClip)
        {
            audioSource.clip = targetClip;
            audioSource.Play();
        }
    }
}