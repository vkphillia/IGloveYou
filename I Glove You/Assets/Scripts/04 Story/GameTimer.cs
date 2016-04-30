using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float totalTime;
    public Text timerUI;

    [HideInInspector]
    public bool timerStarted;


    //Static Singleton Instance
    public static GameTimer _Instance = null;

    //property to get instance
    public static GameTimer Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (GameTimer)FindObjectOfType(typeof(GameTimer));
            }
            return _Instance;
        }
    }

    void Start()
    {
        timerUI.text = "0";
    }

    // Update is called once per frame
    void Update ()
    {
	    if(timerStarted && totalTime>0)
        {
            totalTime = totalTime - Time.deltaTime;
            timerUI.text = totalTime.ToString("0");
        }
        else if(timerStarted && totalTime<=0)
        {
            timerStarted = false;
            timerUI.text = "0";
        }
	}
}
