using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Challenge4 : MonoBehaviour
{
    public PlayerControlsUniversal player;
    public EnemyHolder enemyHolder;

    public GameObject UI;
    public Text filler;
    public Text enemyKilled;

    private int enemyCount;

    void Start()
    {
        enemyKilled.text = "Enemy killed: 0";

        //all these things are not temporary now
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        filler.text = "Challenge 4";
        yield return new WaitForSeconds(1f);
        filler.text = "3";
        yield return new WaitForSeconds(0.5f);
        filler.text = "2";
        yield return new WaitForSeconds(0.5f);
        filler.text = "1";
        yield return new WaitForSeconds(0.5f);
        filler.text = "";

        player.move = true;//enables player movement
        player.AddGlove();

        GameTimer.Instance.timerStarted = true;//starts timer

    }

    void Update()
    {
        // noOfEnemyAlive is set to 0 by Enemy scipt when player triggers enemy
        //timerStarted is set to false by GameTimer when time reaches 0
        if (Challenge.noOfEnemyAlive == 0 && GameTimer.Instance.timerStarted)
        {
            enemyKilled.text = "Enemy killed: " + enemyCount;

            enemyHolder.Spawn(3, true, true);
            Challenge.noOfEnemyAlive++;//increaing no of enemy available in scene
            enemyCount++;//keeping count of enemy spawned in this scene yet
        }

        //if timer has stopped and player is still moving then call for the round stop
        else if (player.move && !GameTimer.Instance.timerStarted)
        {
            StartCoroutine(StopRound());
        }
    }

    //for stoping the player movement and showing challenge complete UI 
    IEnumerator StopRound()
    {
        player.move = false;
        Challenge.noOfEnemyAlive = 0;//reseting

        if (enemyCount - 1 < 3)
        {
            filler.text = "Success comes with great practice";
        }
        else
        {
            filler.text = "Congrats, You Win";
        }

        UI.SetActive(true);//setting challenge complete buttons to active
        yield return new WaitForSeconds(1f);

    }

}
