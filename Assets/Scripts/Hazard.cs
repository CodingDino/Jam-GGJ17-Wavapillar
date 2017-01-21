using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerKilled : GameEvent
{
	public int player = 0;

	public PlayerKilled(int _player)
	{
		player = _player;
	}
}

public class Hazard : MonoBehaviour {


	public List<string> playerTags = new List<string>();

	void OnTriggerEnter2D(Collider2D otherCollider) {
		Debug.Log("Hazard trigger");
		for (int i = 0; i < playerTags.Count; ++i)
		{
			if (otherCollider.gameObject.tag == playerTags[i])
			{
				Events.Raise(new PlayerKilled(i));
				Debug.Log("KILLED PLAYER "+(i+1));

				// TODO: Body parts separate, fly in random directions, fall through floor, destroyed off screen.
				// TODO: Trigger player flash and death
				// TODO: Trigger respawn
			}
		}
	}
}
