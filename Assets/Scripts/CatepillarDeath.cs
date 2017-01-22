using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatepillarDeath : MonoBehaviour {

	public int player;
	public List<GameObject> segments = new List<GameObject>();
	public AudioSource deathAudio;


	void OnEnable()
	{
		Events.AddListener<PlayerKilled>(OnPlayerKilled);
	}

	void OnDisable()
	{
		Events.RemoveListener<PlayerKilled>(OnPlayerKilled);
	}

	private void OnPlayerKilled(PlayerKilled _event)
	{
		if (player == _event.player)
		{
			for (int i = 0; i < segments.Count; ++i)
			{
				Joint2D joint = segments[i].GetComponent<Joint2D>();
				if (joint != null)
				{
					joint.enabled = false;
				}
				Collider2D collider = segments[i].GetComponent<Collider2D>();
				if (collider != null)
				{
					collider.enabled = false;
				}
				Rigidbody2D body = segments[i].GetComponent<Rigidbody2D>();
				if (body != null)
				{
					body.isKinematic = false;
					body.AddForce(85f*(new Vector2(Random.Range(0f,1f),Random.Range(0f,1f))).normalized);
				}
				CaterpillerMovement movement = segments[i].GetComponent<CaterpillerMovement>();
				if (movement)
				{
					movement.enabled = false;
				}
				segments[i].transform.SetParent(null);
			}
			deathAudio.Play();
			Destroy(gameObject);
		}
	}
}
