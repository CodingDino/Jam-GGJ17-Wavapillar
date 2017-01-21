using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FoodEaten : GameEvent
{
	public int player = 0;

	public FoodEaten(int _player)
	{
		player = _player;
	}
}

public class Food : MonoBehaviour {

	public float hoverDrift = 5;

	public MoveToTarget moveToTarget = null;

	public List<string> playerTags = new List<string>();

	private bool movingUp = true;

	public GameObject particlePrefab;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!moveToTarget.hasTarget)
		{
			movingUp = !movingUp;
			moveToTarget.MoveTo(transform.position + Vector3.up*hoverDrift*(movingUp?1:-1));
		}
	}
		
	void OnTriggerEnter2D(Collider2D otherCollider) {
		for (int i = 0; i < playerTags.Count; ++i)
		{
			if (otherCollider.gameObject.tag == playerTags[i])
			{
				Events.Raise(new FoodEaten(i));
				Debug.Log("EATEN BY PLAYER "+(i+1));
				GameObject particles = Instantiate(particlePrefab);
				particles.transform.position = transform.position;
				Destroy(gameObject);
			}
		}
	}
}
