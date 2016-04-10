using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public AudioSource audioSrc;
	public AudioClip explosion;
	public AudioClip throwSound;
	public AudioClip chickenSound;
	public AudioClip footStep1;
	public AudioClip footStep2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(AudioClip audioClip) {
		audioSrc.PlayOneShot (audioClip);
	}
}
