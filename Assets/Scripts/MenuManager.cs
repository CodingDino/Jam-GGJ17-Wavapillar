using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public List<string> StartButtons = new List<string>();
	public List<Respawn> SpawnPoints = new List<Respawn>();
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
					// TODO: ENTER GAME
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
}
