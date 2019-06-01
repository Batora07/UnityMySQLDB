using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
	public Text playerDisplay;
	public Text scoreDisplay;

	private void Awake()
	{
		if(DBManager.username == null)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}

		playerDisplay.text = "Player: " + DBManager.username;
		scoreDisplay.text = "Score: " + DBManager.score;
	}

	public void CallSaveData()
	{
		StartCoroutine(SavePlayerData());
	}

	private IEnumerator SavePlayerData()
	{
		WWWForm form = new WWWForm();
		form.AddField("name", DBManager.username);
		form.AddField("score", DBManager.score);

		UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", form);
		yield return www.SendWebRequest();

		if(www.downloadHandler.text == "0")
		{
			Debug.Log("Game saved");
		}
		else
		{
			Debug.Log("Save failed. Error #" + www.downloadHandler.text);
		}

		DBManager.LogOut();
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public void IncreaseScore()
	{
		DBManager.score++;
		scoreDisplay.text = "Score: " + DBManager.score;
	}
}