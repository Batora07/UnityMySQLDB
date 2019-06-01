using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Registration : MonoBehaviour
{
	public InputField nameField;
	public InputField passwordField;
	public Button submitButton;

	public void CallRegister()
	{
		StartCoroutine(Register());
	}

    private IEnumerator Register()
	{
		WWWForm form = new WWWForm();
		form.AddField("name", nameField.text);
		form.AddField("password", passwordField.text);

		string url = "http://localhost/sqlconnect/register.php";
		UnityWebRequest www = UnityWebRequest.Post(url, form);
		yield return www.SendWebRequest(); 

		if(www.downloadHandler.text == "0")
		{
			Debug.Log("User created successfully.");
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
		else if(www.isNetworkError || www.isHttpError)
		{
				Debug.Log("Network Error : " + www.error);
		}
		else
		{
			Debug.Log("User creation failed. Error #" + www.downloadHandler.text);
		}
	}

	public void VerifyInputs()
	{
		// if pass & username don't require a certain type of chars then test it below
		submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
	}
}
