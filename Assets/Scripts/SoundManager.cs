/**
 * Department: Game Developer
 * File: SoundManager.cs
 * Objective: Control all sounds of the game.
 * Employee: Ramón Martínez Nieto
 */
using System;
using UnityEngine;

/**
 * Class to controll all sounds of the game. This class inherit of the PlayerConf to know all 
 * parameters about the sounds that the player put in the controls panel.
 * 
 * There is tag "DontDestroyOnLoad" to continue alive during the game.
 * 
 * @author Ramón Martínez Nieto
 */
public class SoundManager : PlayerConf
{
	
	/**
	 * Struct to create a new audio. Audios are created in the inspector and 
	 * all audios in the dictionary have a name and audio clip.
	 */
	[Serializable]
	public struct DictionaryClips 
	{
		/**
		 * Name of the clip.
		 */ 
		public string name;
		/*
		 * Specific AudioClip
		 */
		public AudioClip audioClip;
	}

	/**
	 * Dictionary to establish the music of the game. In the SoundManager's GameObject 
	 * with this script, you can configure this dictionary to add new music.
	 */
	[Tooltip("Drag the diferents clips of music")]
	public DictionaryClips[] musicClips;

	/**
	 * Dictionary to establish music effects of the game. In the SoundManager's GameObject 
	 * with this script, you can configure this dictionary to add new effects.
	 * 
	 */
	[Tooltip("Drag the diferents clips of effects on game")]
	public DictionaryClips[] musicEffects;


	/**
	 * Represents the AudioSoruce for playing sound effects.
	 */
	public AudioSource EffectsSource;
	/**
	 * Represents the AudioSoruce for playing Music in game.
	 */
	public AudioSource MusicSource;
	/**
	 * Represents the AudioSoruce for playing secondary shoots effects.
	 */
	public AudioSource SecondaryShotSource;

	/**
	* Static SoundManager to pattern Singleton
	*/
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

	/**
	 * Method for playing a effect like shot effects.
	 * You can change Pitch and Volumen for repetitive sounds.
	 * Solution for not tiring the user.
	 * 
	 * @param string with the name of the to reproduce.
	 * @param variationPitch from 0f to 1f, by default 0f
	 * @param variationVolumen from 0f to 1f, by default 0f
	 *
	 */
	public void PlayEffect(string clip, float variationPitch = 0f, float variationVolumen = 0f)
	{
		EffectsSource.volume = MusicEffectVolumen - variationVolumen;
		EffectsSource.pitch =  1f - variationPitch;
		EffectsSource.clip = getClip(clip, musicEffects);
		EffectsSource.Play();
	}

	/**
	 * Método básico para las opciones de los menús ya que no admiten valores por defecto.
	 */
	public void PlayEffect(string clip)
	{
		EffectsSource.clip = getClip(clip, musicEffects);
		EffectsSource.Play();
	}

	/**
	 * Method for playing a secondary effect like secondary shot effects.
	 * You can change Pitch and Volumen for repetitive sounds.
	 * Solution for not tiring the user.
	 * 
	 * @param string with the name of the clip to reproduce.
	 * @param variationPitch by default 0f
	 * @param variationVolumen by default 0f
	 *
	 */
	public void PlaySecondaryEffect(string clip, float variationVolumen = 0f, float variationPitch = 0f)
	{
		SecondaryShotSource.volume = MusicEffectVolumen + variationVolumen;
		SecondaryShotSource.pitch = 1f - variationPitch;
		SecondaryShotSource.clip = getClip(clip, musicEffects);
		SecondaryShotSource.Play();
	}

	/**
	 * Method for playing the music in the game.  
	 * 
	 * @param string with the name of the clip to reproduce.
	 *
	 */
	public void PlayMusic(string clip)
	{
		MusicSource.clip = getClip(clip, musicClips);
		MusicSource.Play();
	}

	/**
	 * TODO:  
	 */
	private AudioClip getClip(string clip, DictionaryClips [] list) 
	{
		AudioClip clipToPlay = null;
		foreach (DictionaryClips dc in list)
		{
			if(dc.name.Equals(clip)) clipToPlay = dc.audioClip;
		}

		return clipToPlay;
	}

	/**
	 * Method to stop the music.  
	 */
	public void StopMusic() => MusicSource.Stop();
	/**
	 * Method to stop the principal effect.  
	 */
	public void StopEffect() => EffectsSource.Stop();
	/**
	 * Method to stop the secondary effect.  
	 */
	public void StopSecondaryEffect() => SecondaryShotSource.Stop();
	 
	/**
	 * Method to establish the lopping of the music.
	 * 
	 *  @param bool 
	 */
	public void LoopMusic(bool looping) => MusicSource.loop = looping;

	/**
	 * Method for change the music volume. The MusicVolumen depens of the PlayerConf.
	 *
	 * @see PlayerConf#MusicVolumen
	 */
	public void ChangeVolumenMusic()
	{
		MusicSource.volume = MusicVolumen;
	}

	/**
	 * Method for change the effects volume. The MusicEffectVolumen depens fo the PlayerConf.
	 *
	 * @see PlayerConf#MusicVolumen
	 */
	public void ChangeVolumenEffects()
	{
		EffectsSource.volume = MusicEffectVolumen;
		SecondaryShotSource.volume = MusicEffectVolumen;
	}

	/**
	 * Method to mute music. 
	 *
	 * @param bool mute
	 */
	public void MuteMusic(bool mute) 
	{
		MusicSource.mute = mute;
	}

	/**
	 * Method to mute effects sounds. 
	 *
	 * @param bool mute
	 */ 
	public void MuteEffectsSounds(bool mute) 
	{
		EffectsSource.mute = mute;
		SecondaryShotSource.mute = mute;
	}

}