using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashLogin : MonoBehaviour
{
    Color tempC;
    Color tempo;

    void Start ()
    {
        tempo = GetComponent<SpriteRenderer>().color;
        tempC = GetComponent<SpriteRenderer>().color;
        tempC.a = 0;//removing alpha to make the sprite invisible

        Invoke("LoadMenu", 3f);
    }
	
    void Update()
    {
        //just some loading effect //replace this before shipping
        GetComponent<SpriteRenderer>().color = Color.Lerp(tempC, tempo, Mathf.PingPong(Time.time, 1.5f));
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("main menu");
    }
}
