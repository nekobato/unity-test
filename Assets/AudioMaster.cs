using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour {

	private AudioSource[] sources;

	// Use this for initialization
	void Start () {
		sources = gameObject.GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SwitchToJp () {
		StartCoroutine ("FadeInJp", 0.5f);
	}

	public void SwitchToEn () {
		StartCoroutine ("FadeInEn", 0.5f);
	}

	IEnumerator FadeInJp (float duration) {

		AudioSource sourceEn = sources[0];
		AudioSource sourceJp = sources[1];

		// Sync audio time late
		sourceJp.timeSamples = sourceEn.timeSamples - 3000;
		sourceJp.Play();

		float currentTime = 0.0f;
		float waitTime = 0.02f;
		float enFirstVolume = sourceEn.volume;
		float jpFirstVolume = 0;
		while (duration > currentTime) {
			currentTime += Time.fixedDeltaTime;
			sourceEn.volume = Mathf.Clamp01(enFirstVolume * (duration - currentTime) / duration);
			sourceJp.volume = Mathf.Clamp01((currentTime / duration));
			if (sourceEn.volume == 0) {
				sourceEn.Stop();
			}
			yield return new WaitForSeconds(waitTime);
		}
	}

	IEnumerator FadeInEn (float duration) {

		AudioSource sourceEn = sources[0];
		AudioSource sourceJp = sources[1];

		// Sync audio time late
		sourceEn.timeSamples = sourceJp.timeSamples + 3000;
		sourceEn.Play();

		float currentTime = 0.0f;
		float waitTime = 0.02f;
		float enFirstVolume = 0;
		float jpFirstVolume = sourceJp.volume;
		while (duration > currentTime) {
			currentTime += Time.fixedDeltaTime;
			sourceEn.volume = Mathf.Clamp01((currentTime / duration));
			sourceJp.volume = Mathf.Clamp01(jpFirstVolume * (duration - currentTime) / duration);
			if (sourceJp.volume == 0) {
				sourceJp.Stop();
			}
			yield return new WaitForSeconds(waitTime);
		}
	}

}
