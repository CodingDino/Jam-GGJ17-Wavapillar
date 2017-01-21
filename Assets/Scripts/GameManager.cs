using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWon : GameEvent
{
	public int player = 0;

	public GameWon(int _player)
	{
		player = _player;
	}
}


public class GameManager : MonoBehaviour {

	public List<string> PauseButtons = new List<string>();
	public GameObject winScreen;
	public Text winText;
	public GameObject pauseScreen;
	private bool gameOvered = false;
	private bool paused = false;

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

	void Update()
	{
		for (int i = 0; i < PauseButtons.Count; ++i)
		{
			if (Input.GetButtonDown(PauseButtons[i]))
			{
				Debug.Log("START BUTTON PRESSED: "+PauseButtons[i]);
				if (gameOvered)
				{
					SceneManager.LoadScene("Title");
				}
				else
				{
					Pause(!paused);
				}

			}
		}
	}

	void Pause(bool _pause)
	{
		Debug.Log("SETTING PAUSE TO "+_pause);
		paused = _pause;
		pauseScreen.SetActive(paused);
		Time.timeScale = paused ? 0 : 1;
	}
}
