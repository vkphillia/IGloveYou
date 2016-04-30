using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Challenge5 : MonoBehaviour
{
    public PlayerControlsUniversal player;
    public Text filler;

    void Start()
    {
        //all these things are temporary
        
        StartCoroutine(stopRound());
    }

    //temporay codes just to give an idea
    IEnumerator stopRound()
    {
        filler.text = "Challenge 5";
        yield return new WaitForSeconds(1f);
        filler.text = "3";
        yield return new WaitForSeconds(0.5f);
        filler.text = "2";
        yield return new WaitForSeconds(0.5f);
        filler.text = "1";
        yield return new WaitForSeconds(0.5f);
        filler.text = "Time to be the boss";
        player.move = true;
        yield return new WaitForSeconds(7f);
        player.move = false;
        filler.text = "Great\n You are a Hero";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("story main");
    }

}
