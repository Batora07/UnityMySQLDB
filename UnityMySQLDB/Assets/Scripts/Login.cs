using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
	public InputField nameField;
	public InputField passwordField;
	public Button submitButton;

	public void CallLogin()
	{
		StartCoroutine(LoginPlayer());
	}

	private IEnumerator LoginPlayer()
	{
		WWWForm form = new WWWForm();
		form.AddField("name", nameField.text);
		form.AddField("password", passwordField.text);

		string url = "http://localhost/sqlconnect/login.php";
		UnityWebRequest www = UnityWebRequest.Post(url, form);
		yield return www.SendWebRequest();

		if(www.downloadHandler.text[0] == '0')
		{
			DBManager.username = nameField.text;
			DBManager.score = int.Parse(www.downloadHandler.text.Split('\t')[1]);
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
		else
		{
			Debug.Log("User login failed. Error #" + www.downloadHandler.text);
		}
	}

	public void VerifyInputs()
	{
		// if pass & username don't require a certain type of chars then test it below
		submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
	}
}
