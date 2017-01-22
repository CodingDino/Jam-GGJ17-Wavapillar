using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public List<string> StartButtons = new List<string>();
	public List<Respawn> SpawnPoints = new List<Respawn>();
	public List<SpriteRenderer> SpawnEggs = new List<SpriteRenderer>();
	public List<Text> SpawnText = new List<Text>();
	public Text StartText;

	private List<bool> Spawned = new List<bool>();

	// Use this for initialization
	void Start () {
		Spawned.Add(false);
		Spawned.Add(false);
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < StartButtons.Count; ++i)
		{
			if (Input.GetButtonDown(StartButtons[i]))
			{
				Debug.Log("START BUTTON PRESSED: "+StartButtons[i]);
				if (Spawned[i])
				{
					PlayerPrefs.SetInt("Player1",Spawned[0] ? 1 : 0);
					PlayerPrefs.SetInt("Player2",Spawned[1] ? 1 : 0);
					SceneManager.LoadScene("InGame");
				}
				else
				{
					SpawnPoints[i].SpawnPlayer();
					Spawned[i] = true;
					SpawnText[i].gameObject.SetActive(false);
					StartText.gameObject.SetActive(true);
				}

			}
		}
	}


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
		Spawned[_event.player] = false;
		SpawnText[_event.player].gameObject.SetActive(true);
		SpawnEggs[_event.player].gameObject.SetActive(true);

		if (!Spawned[0] && !Spawned[1])
			StartText.gameObject.SetActive(false);
	}
}
