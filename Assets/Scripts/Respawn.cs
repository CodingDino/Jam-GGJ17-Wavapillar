using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

	public GameObject egg;
	public TextMesh timerText;
	public GameObject caterpillarPrefab;
	public int player;
	public float respawnDuration;
	public bool spawnOnLoad = true;
	public bool autorespawn = true;
	public bool usePrefs = true;
	public AudioSource countAudio;

	private float respawnStart = 0;
	private bool respawning = false;

	// Use this for initialization
	void Start () 
	{
		if (spawnOnLoad)
			StartRespawn();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (respawning)
		{
			float timeLeft = respawnDuration - (Time.time - respawnStart);
			if (timeLeft > 0)
			{
				string timeString = Mathf.CeilToInt(timeLeft).ToString();
				if (timeString != timerText.text)
				{
					timerText.text = timeString;
					timerText.transform.localScale = Vector3.one;
					countAudio.Play();
				}
				else
				{
					timerText.transform.localScale = timerText.transform.localScale - Vector3.one*Time.deltaTime;
				}
			}
			else
			{
				SpawnPlayer();
			}
		}
	}

	public void SpawnPlayer()
	{
		GameObject player = Instantiate(caterpillarPrefab);
		player.transform.position = transform.position;
		egg.SetActive(false);
		timerText.gameObject.SetActive(false);
		respawnStart = 0;
		respawning = false;
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
		if (autorespawn && player == _event.player)
		{
			StartRespawn();
		}
	}

	private void StartRespawn()
	{
		if (usePrefs && PlayerPrefs.GetInt("Player"+(player+1).ToString())==0)
		{
			egg.SetActive(false);
			return;
		}
		
		egg.SetActive(respawnDuration>0);
		timerText.gameObject.SetActive(respawnDuration>0);
		if (respawnDuration>0)
			countAudio.Play();
		timerText.text = ((int)respawnDuration).ToString();
		timerText.transform.localScale = Vector3.one;
		respawnStart = Time.time;
		respawning = true;
	}
}
