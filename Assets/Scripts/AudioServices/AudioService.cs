using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generics;

namespace GameAudio {

    public enum AudioType {
        LEVEL_BG,
        SHOT_FIRED,
        TANK_EXPLOSION,
        BUTTON_CLICK,
        ACHIEVEMENT_UNLOCKED
    }

    [System.Serializable]
    public class GameAudio {
        public AudioType audioType;
        [HideInInspector]
        public AudioSource audioSrc;
        public AudioClip audioClip;
        public bool loop;
        [Range(0, 1)]
        public float volume;
    }

    public class AudioService : GenericMonoSingleton<AudioService>
    {
        [SerializeField] GameAudio[] gameAudios;
        protected override void Awake() {
            if (Instance != null) {
                Destroy(this);
            } else {
                instance = (AudioService)this;
                DontDestroyOnLoad(this.gameObject);
            }

            foreach (GameAudio gameAudio in gameAudios) {
                gameAudio.audioSrc = gameObject.AddComponent<AudioSource>();
                gameAudio.audioSrc.volume = gameAudio.volume;
                gameAudio.audioSrc.clip = gameAudio.audioClip;
                gameAudio.audioSrc.loop = gameAudio.loop;
            }

        }

        public void PlayAudio(AudioType audioType) {
            GameAudio gameAudio = Array.Find(gameAudios, item => item.audioType == audioType);
            gameAudio.audioSrc.Play();
        }

        public void StopAudio(AudioType audioType) {
            GameAudio gameAudio = Array.Find(gameAudios, item => item.audioType == audioType);
            gameAudio.audioSrc.Stop();
        }
        
        
    }

}
