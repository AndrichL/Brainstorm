using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Sjoerd
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;


        public static AudioManager thisAudioManager;
        private void Awake()
        {
            if(thisAudioManager == null)
            {
                thisAudioManager = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.Clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = s.audioMixer;
            }
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Play sound: " + name + "is not found");
                return;
            }
            else
            {
                s.source.Play();
            }
        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Stop sound: " + name + "is not found");
                return;
            }
            else
            {
                s.source.Stop();
            }
        }

        public void Pause(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("pause sound: " + name + "is not found");
                return;
            }
            else
            {
                s.source.Pause();
            }
        }

        public void UnPause(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("unpause sound: " + name + "is not found");
                return;
            }
            else
            {
                s.source.UnPause();
            }
        }
    }
}
