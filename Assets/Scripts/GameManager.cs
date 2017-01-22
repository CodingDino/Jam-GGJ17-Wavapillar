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
	public ParticleSystem winParticles;
	public List<Color> PlayerColors = new List<Color>();

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
		winText.text = "TEAM "+(_event.player+1)+" WON!";
		winText.color = PlayerColors[_event.player];
		Time.timeScale = 0;
		gameOvered = true;
		winParticles.Play();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		for (int i = 0; i < PauseButtons.Count; ++i)
		{
			if (Input.GetButtonDown(PauseButtons[i]))
			{
				Debug.Log("START BUTTON PRESSED: "+PauseButtons[i]);
				if (gameOvered)
				{
					Time.timeScale = 1.0f;
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
