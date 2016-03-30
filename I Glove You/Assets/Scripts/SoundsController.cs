using UnityEngine;
using System.Collections;

public class SoundsController : MonoBehaviour
{
	public AudioSource bgMusic;

	private Object[] sounds;
	private AudioSource[] audioSource = new AudioSource[5];


	//Static Singleton Instance
	public static SoundsController _Instance = null;

	//property to get instance
	public static SoundsController Instance {
		get {
			//if we do not have Instance yet
			if (_Instance == null)
			{
				_Instance = (SoundsController)FindObjectOfType (typeof(SoundsController));
			}
			return _Instance;
		}
	}
    
	//initialization all the sound effects
	void Start ()
	{
		sounds = Resources.LoadAll ("Sounds", typeof(AudioClip));
		audioSource = GetComponents<AudioSource> ();

		//for debuggin only
		//for (int i=0;i<sounds.Length;i++)
		//{
		//Debug.Log(sounds[i].name+"\n");
		//}    
	}

	void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing && !bgMusic.isPlaying)
		{
			//Debug.Log ("playing");
			bgMusic.Play ();
		}
		else if (OfflineManager.Instance.currentState != GameState.Playing && bgMusic.isPlaying)
		{
			//Debug.Log ("not playing");
			bgMusic.Stop ();
		}
	}
	//call this function with a parameter of the sound name as in resource folder
	public void PlaySoundFX (string sfxName, float vol)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sfxName == sounds [i].name)
			{
				if (!audioSource [0].isPlaying)
				{
					audioSource [0].clip = sounds [i] as AudioClip;
					audioSource [0].volume = vol;
					audioSource [0].Play ();
				}
				else if (!audioSource [1].isPlaying)
				{
					audioSource [1].clip = sounds [i] as AudioClip;
					audioSource [1].volume = vol;
					audioSource [1].Play ();
				}
				else if (!audioSource [2].isPlaying)
				{
					audioSource [2].clip = sounds [i] as AudioClip;
					audioSource [2].volume = vol;
					audioSource [2].Play ();
				}
				else if (!audioSource [3].isPlaying)
				{
					audioSource [3].clip = sounds [i] as AudioClip;
					audioSource [3].volume = vol;
					audioSource [3].Play ();
				}
				else
				{
					audioSource [4].clip = sounds [i] as AudioClip;
					audioSource [4].volume = vol;
					audioSource [4].Play ();
				}
				break;
			}
            
		}
	}

	public void StopSoundFX (string sfxName, float vol)
	{
		for (int i = 0; i < sounds.Length; i++)
		{
			if (sfxName == sounds [i].name)
			{
				if (!audioSource [0].isPlaying)
				{
					audioSource [0].clip = sounds [i] as AudioClip; //why setting clip if we are going to kill this sound
					audioSource [0].volume = vol;//why setting volume?
					audioSource [0].Stop ();
				}
				else if (!audioSource [1].isPlaying)
				{
					audioSource [1].clip = sounds [i] as AudioClip;
					audioSource [1].volume = vol;
					audioSource [1].Stop ();
				}
				else if (!audioSource [2].isPlaying)
				{
					audioSource [2].clip = sounds [i] as AudioClip;
					audioSource [2].volume = vol;
					audioSource [2].Stop ();
				}
				else if (!audioSource [3].isPlaying)
				{
					audioSource [3].clip = sounds [i] as AudioClip;
					audioSource [3].volume = vol;
					audioSource [3].Stop ();
				}
				else
				{
					audioSource [4].clip = sounds [i] as AudioClip;
					audioSource [4].volume = vol;
					audioSource [4].Stop ();
				}

				break;
			}
            
		}
	}
}
