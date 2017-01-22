using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour {

	public Animator animator;

	public float tongueInterval = 5.0f;

	public AudioSource tongueAudio;
	public AudioSource croakAudio;

	private float lastTongue = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= lastTongue + tongueInterval)
		{
			lastTongue = Time.time;
			animator.SetTrigger("Tongue");
			tongueAudio.Play();
		}
	}

	public void PlayCroak()
	{
		croakAudio.Play();
	}
}
