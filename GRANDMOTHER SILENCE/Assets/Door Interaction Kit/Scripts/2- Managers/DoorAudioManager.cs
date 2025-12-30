using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace DoorInteractionKit
{
    public class DoorAudioManager : MonoBehaviour
    {
        [Header("List of Sound Effect SO's")]
        [SerializeField] private Sound[] sounds;

        [Header("Sound Mixer Group")]
        [SerializeField] private AudioMixerGroup mixerGroup;

        private Dictionary<Sound, Coroutine> soundDelays = new Dictionary<Sound, Coroutine>();

        public static DoorAudioManager instance;

        void Awake()
        {
            if (instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;

                s.source.outputAudioMixerGroup = mixerGroup;
            }
        }

        public void Play(Sound sound, float delay = 0f)
        {
            // If there's already a delay going on for this sound, stop it
            if (soundDelays.ContainsKey(sound) && soundDelays[sound] != null)
            {
                StopCoroutine(soundDelays[sound]);
            }

            // Start the delay and store the coroutine in case we need to stop it
            soundDelays[sound] = StartCoroutine(PlayDelayed(sound, delay));
        }

        private IEnumerator PlayDelayed(Sound sound, float delay)
        {
            yield return new WaitForSeconds(delay);

            Sound s = sounds.FirstOrDefault(item => item == sound);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + sound + " not found!");
                yield break;
            }

            s.source.volume = s.volume * (1f + Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.source.pitch = s.pitch * (1f + Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            s.source.Play();
        }

        public void StopPlaying(Sound sound)
        {
            Sound s = sounds.FirstOrDefault(item => item == sound);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.volume = s.volume * (1f + Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.source.pitch = s.pitch * (1f + Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
            s.source.Stop();
        }

        public void PausePlaying(Sound sound)
        {
            Sound s = sounds.FirstOrDefault(item => item == sound);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.volume = s.volume * (1f + Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.source.pitch = s.pitch * (1f + Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
            s.source.Pause();
        }
    }
}
