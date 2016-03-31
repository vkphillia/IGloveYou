using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashLogin : MonoBehaviour
{
    Color tempC;
    Color tempo;
    public GameObject intro;
    private AsyncOperation async;

    void Start ()
    {
        //tempo = GetComponent<SpriteRenderer>().color;
        //tempC = GetComponent<SpriteRenderer>().color;
        //tempC.a = 0;//removing alpha to make the sprite invisible
        async = SceneManager.LoadSceneAsync("main menu");
        async.allowSceneActivation = false;

        GetComponent<Animator>().enabled = true;
        Invoke("LoadAnim2", 9f);
    }
	
    void Update()
    {
        //just some loading effect //replace this before shipping
        //GetComponent<SpriteRenderer>().color = Color.Lerp(tempC, tempo, Mathf.PingPong(Time.time, 1.5f));
    }

    void LoadAnim2()
    {
        intro.SetActive(true);
        Invoke("LoadMenu", 6f);
        //SceneManager.LoadScene("main menu");
    }

    void LoadMenu()
    {
        async.allowSceneActivation = true;
    }
}
