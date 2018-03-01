using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
	public static MusicManager instance;

	public bool playMusic = true;
	public bool playSoundEffect = true;

	public AudioClip music;
	public AudioClip[] soundEffects;

	private float organicVolumeMin = 0.75f;
	private float organicVolumeMax = 1.50f;
	private GameObject effectsGO;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			
			playMusic = true;
			playSoundEffect = true;

			AudioSource _audioSource = gameObject.GetComponent<AudioSource>();

			if(_audioSource == null)
			{
				gameObject.AddComponent<AudioSource>();
				_audioSource = gameObject.GetComponent<AudioSource>();
				_audioSource.loop = true;
				_audioSource.playOnAwake = true;

				if(music != null)
				{
					_audioSource.Play();
				}
			}

		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

	public void PlayMusic()
	{
		if(!playMusic)
			return;
	}

	public void PlaySFX( string audioClipName, Vector3 position )
	{
		if(!playSoundEffect)
			return;

		for(int i = 0; i < soundEffects.Length; i++)
		{
			if(soundEffects[i].name == audioClipName)
			{
				float volume = Random.Range(organicVolumeMin, organicVolumeMax);
				AudioSource.PlayClipAtPoint( soundEffects[i], position, volume );
				break;
			}
		}
	}
}
