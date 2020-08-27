using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PlayerConf
{
	//Struct to simulate a Dictionary on Inspector in Unity
	[Serializable]
	public struct DictionaryClips 
	{
		public string name;
		public AudioClip audioClip;
	}

	
	[Tooltip("Drag the diferents clips of music")]
	public DictionaryClips[] musicClips;
	
	[Tooltip("Drag the diferents clips of effects on game")]
	public DictionaryClips[] musicEffects;

	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	public static SoundManager Instance = null;
	
	private float currentVolumen;
	private bool muteAllSounds;

	void Awake()
    {
		singletonInstance();
	}

    private void Start()
    {
		//HandlerOptions ho = HandlerOptions.Instance;

		currentVolumen = MusicVolumen;

		//Look !, music ON = true is mute = false, be careful
		muteAllSounds = !MusicOn;
		
		setInitialParameters();
	}

	private void singletonInstance() 
	{
		//Singleton instance
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		//Dont destroy on load to persevere this object in all scenes
		DontDestroyOnLoad(gameObject);
	}

	private void setInitialParameters() 
	{
		EffectsSource.volume = currentVolumen;
		MusicSource.volume = currentVolumen;

		EffectsSource.mute = muteAllSounds;
		MusicSource.mute = muteAllSounds;
	}



	public void PlayEffect(string clip)
	{
		EffectsSource.clip = getClip(clip, musicEffects);
		EffectsSource.Play();
	}

	public void PlayMusic(string clip)
	{
		MusicSource.clip = getClip(clip, musicClips);
		MusicSource.Play();
	}

	private AudioClip getClip(string clip, DictionaryClips [] list) 
	{
		AudioClip clipToPlay = null;
		foreach (DictionaryClips dc in list)
		{
			if(dc.name.Equals(clip)) clipToPlay = dc.audioClip;
		}

		return clipToPlay;
	}

	public void StopMusic() => MusicSource.Stop();
	public void StopEffect() => EffectsSource.Stop();
	public void LoopMusic(bool looping) => MusicSource.loop = looping;
	public void ChangeVolumen(float vol)
	{
		currentVolumen = vol;
		EffectsSource.volume = currentVolumen;
		MusicSource.volume = currentVolumen;
	}

	public void MuteAllSounds(bool mute) 
	{
		EffectsSource.mute = mute;
		MusicSource.mute = mute;
	}

}
