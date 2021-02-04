using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Sjoerd
{
    public class AudioManager : MonoBehaviour
    {
        public AudioMixerGroup mainGroep;
        public AudioMixerGroup musicGroep;
        public AudioMixerGroup SFXGroep;

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

            PlayBackgroundMusic();
        }


        private void PlayBackgroundMusic()
        {
            if(SceneManager.GetActiveScene().buildIndex == 0) // Menu scene
            {
                Play("menuOST");
            }
            else if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                Play("OST");
            }
        }

        //start to play audio
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

        //stop the audio
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

        //pause the audio
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

        //unpause the audio
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

        public void SetVolumeMain(float Volume)
        {
            mainGroep.audioMixer.SetFloat("volume", Volume);
        }

        public void SetVolumeMusic(float Volume)
        {
            musicGroep.audioMixer.SetFloat("volumeMusic", Volume);
        }

        public void SetVolumeaSFX(float Volume)
        {
            SFXGroep.audioMixer.SetFloat("volumeSFX", Volume);
        }
    }
}
