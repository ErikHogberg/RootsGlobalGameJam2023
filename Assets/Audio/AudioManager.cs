using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AdjustableClip
{
	public AudioClip clip;
	public float amp = 0;
}

[System.Serializable]
public class SfxEvent
{
	public string name;
	public AdjustableClip[] clips;
	[HideInInspector]
	public AudioSource source;
	public float amp = 1;

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
	public AudioManager singleton;

	[SerializeField] 
	private SfxEvent[] sfxEvents;
	
	[SerializeField]
	private MusicSegment[] segments;
	private AudioSource musicSource;
	private float currentIntensity = 0;
	private float minIntensity = 1000; // stupid numbers, but it's for comparisons down the line
	private float maxIntensity = -1000;
	[SerializeField]
	private float musicAmp;
	[SerializeField]
	private float segmentLength = 1f;
	private bool musicPlaying = false;

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

	void UpdateMusic(float intensity) {
		currentIntensity = Mathf.Lerp(currentIntensity, intensity, 0.01f);
	}

	public void StartMusic()
	{
		StartCoroutine("MusicLoop");
		musicPlaying = true;
	}

	public void StopMusic()
	{
		musicPlaying = false;
	}

    private IEnumerator MusicLoop()
    {
		while (musicPlaying) {
			yield return new WaitForSeconds(segmentLength);

			List<MusicSegment> clips = new List<MusicSegment>();
			float intensity = Mathf.FloorToInt(Mathf.Clamp(currentIntensity, minIntensity, maxIntensity));
			MusicSegment nextSeg;
			foreach (var clip in segments) {
				if (clip.intensity == intensity) {
					clips.Insert(0, clip);
				}
			}
			int nextIndex = Random.Range(0, clips.Count);
			
			if (clips.Count == 0) {
				// lil bit of error handling
				Debug.LogWarning("There is no clip with the intensity: " + intensity.ToString());
				yield return new WaitForSeconds(1f);
				continue;
			}
			nextSeg = clips[nextIndex];

			musicSource.PlayOneShot(nextSeg.clip);
		}
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
		musicSource.volume = musicAmp;
		foreach (var seg in segments) {
			if (seg.intensity < minIntensity) {
				minIntensity = seg.intensity; 
			}
			if (seg.intensity > maxIntensity) {
				maxIntensity = seg.intensity;
			}
		}
	}
}
