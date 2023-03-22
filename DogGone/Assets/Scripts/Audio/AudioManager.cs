using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    
    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        Play("MonkeysSpinningMonkeys");
    }

    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name); //Gets a sound from the array

        if (s == null){ //If it cant find a sound put a debug message in the log
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        //Play the sound
        s.source.Play();
    }
}