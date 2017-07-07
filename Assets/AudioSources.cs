using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSources : MonoBehaviour {

	AudioSource[] audioSources;

	// Use this for initialization
  /// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		audioSources = this.GetComponents<AudioSource>();
	}
	void Start () {
		audioSources[1].Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
