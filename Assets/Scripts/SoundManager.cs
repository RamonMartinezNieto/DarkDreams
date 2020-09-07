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
	public AudioSource SecondaryShotSource; 

	public static SoundManager Instance = null;
	
	void Awake()
    {
		singletonInstance();
	}

    private void Start()
    {
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
		MusicSource.volume = MusicVolumen;
		EffectsSource.volume = MusicEffectVolumen;
		SecondaryShotSource.volume = MusicEffectVolumen;

		SecondaryShotSource.mute = !MusicEffectOn;
		EffectsSource.mute = !MusicEffectOn;
		MusicSource.mute = !MusicOn;
	}

	public void PlayEffect(string clip, float variationPitch = 0f, float variationVolumen = 0f)
	{
		EffectsSource.volume = MusicEffectVolumen - variationVolumen;
		EffectsSource.pitch =  1f - variationPitch;
		EffectsSource.clip = getClip(clip, musicEffects);
		EffectsSource.Play();
	}	
	
	public void PlaySecondaryEffect(string clip)
	{
		SecondaryShotSource.clip = getClip(clip, musicEffects);
		SecondaryShotSource.Play();
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
	public void StopSecondaryEffect() => SecondaryShotSource.Stop();
	public void LoopMusic(bool looping) => MusicSource.loop = looping;
	
	public void ChangeVolumenMusic(float vol)
	{
		MusicSource.volume = MusicVolumen;
	}
	public void ChangeVolumenEffects(float vol)
	{
		EffectsSource.volume = MusicEffectVolumen;
		SecondaryShotSource.volume = MusicEffectVolumen;
	}

	public void MuteMusic(bool mute) 
	{
		MusicSource.mute = mute;
	}

	public void MuteEffectsSounds(bool mute) 
	{
		EffectsSource.mute = mute;
		SecondaryShotSource.mute = mute;
	}

}
