using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdjustableClip
{
	public AudioClip clip;
	public float amp = 1f;
}

[System.Serializable]
public class SfxEvent
{
	public string name;
	public AdjustableClip[] clips;
	[HideInInspector]
	public AudioSource source;
	public float amp = 1f;

	public void Setup(GameObject gameObject) {
		source = gameObject.AddComponent<AudioSource>();
		source.volume = amp;
	}

	public void Emit() {
		var clipIndex = Random.Range(0, clips.Length);
		source.PlayOneShot(clips[clipIndex].clip, clips[clipIndex].amp);
	}
}

[System.Serializable]
public class MusicSegment
{
	public int intensity;
	public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
	[HideInInspector]
	public AudioManager singleton;

	[SerializeField] 
	private SfxEvent[] sfxEvents;
	

	[SerializeField]
	private AudioClip musicClip;
	[SerializeField]
	private MusicSegment[] segments; //unused
	private AudioSource musicSource;
	[SerializeField]
	private float currentIntensity = 0;
	private float minIntensity = 1000; // stupid numbers, but it's for comparisons down the line
	private float maxIntensity = -1000;
	[SerializeField]
	private float musicAmp;
	// [SerializeField]
	// private float segmentLength = 1f;

	public void Sfx(string name)
	{
		for (int i = 0; i < sfxEvents.Length; i++){
			var sfxEvent = sfxEvents[i];
			if ( name == sfxEvent.name) {
				sfxEvent.Emit();
				return;
			}
		}

		Debug.LogWarning(name + " does not exist as an sfx");
	}

	public void UpdateMusic(float intensity) {
		currentIntensity = intensity; // Mathf.Lerp(currentIntensity, intensity, 0.01f);
	}

	public void StartMusic()
	{
		musicSource.Play();
	}

	public void StopMusic()
	{
		musicSource.Stop();
	}


	void Awake() {
		if (singleton == null) {
			singleton = this;
		} else {
			Destroy(this.gameObject);
			return;
		}

		foreach (var sfx in sfxEvents)
		{
			sfx.Setup(gameObject);	
		}

		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.loop = true;
		musicSource.volume = musicAmp;
		musicSource.clip = musicClip;
		foreach (var seg in segments) {
			if (seg.intensity < minIntensity) {
				minIntensity = seg.intensity; 
			}
			if (seg.intensity > maxIntensity) {
				maxIntensity = seg.intensity;
			}
		}
	}

	void Start() {
		StartMusic();
	}

	void Update() {

	}
}
