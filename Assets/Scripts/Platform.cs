using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public float speed = 1;
	public float bounds = 5;

	private bool movingUp = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + Vector3.up*Time.deltaTime*speed*(movingUp?1:-1);
		if (movingUp && transform.position.y > bounds)
			movingUp = false;
		if (!movingUp && transform.position.y < -bounds)
			movingUp = true;
	}
}
