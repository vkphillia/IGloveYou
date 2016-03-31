using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Disclaimer : MonoBehaviour
{

	Color tempC;
	Color tempo;

	void Start ()
	{
		tempo = GetComponent<Text> ().color;
		tempC = GetComponent<Text> ().color;
		tempC.a = 0;//removing alpha to make the sprite invisible

		//Invoke ("LoadMenu", 4f);
	}

	void Update ()
	{
		//just some loading effect //replace this before shipping

	}

	public void LoadMenu ()
	{
		SceneManager.LoadScene ("");
	}


}
