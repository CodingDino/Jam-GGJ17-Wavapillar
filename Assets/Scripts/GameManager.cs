using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWon : GameEvent
{
	public int player = 0;

	public GameWon(int _player)
	{
		player = _player;
	}
}


public class GameManager : MonoBehaviour {

	public GameObject winScreen;
	public Text winText;

	void OnEnable()
	{
		Events.AddListener<GameWon>(OnGameWon);
	}

	void OnDisable()
	{
		Events.RemoveListener<GameWon>(OnGameWon);
	}

	private void OnGameWon(GameWon _event)
	{
		winScreen.SetActive(true);
		winText.text = "PLAYER "+(_event.player+1)+" WON!";
		Time.timeScale = 0;

		// TODO: play again or main menu
	}
}
