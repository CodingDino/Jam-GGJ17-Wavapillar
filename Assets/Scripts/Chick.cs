using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour {

	public float CycleOffset = 0;
	public AudioSource cheepAudio;

	// Use this for initialization
	void Start () {
		GetComponent<Animator>().SetFloat("CycleOffset",CycleOffset);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayCheep()
	{
		cheepAudio.Play();
	}
}
