using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public PlayerControlsUniversal player;
    public GameObject UI;
    public GameObject Intro;

    public string[] dialouges;
    public Text dialougeText;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(StartTutorial());
	}
	
	IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(0.5f);
        Color temp = Intro.GetComponent<SpriteRenderer>().color;
        float alpha=0;
        while(alpha<1)
        {
            temp.a = alpha;
            Intro.GetComponent<SpriteRenderer>().color = temp;
            alpha+=0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        Intro.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(5f);
        
        while (alpha > 0)
        {
            temp.a = alpha;
            Intro.GetComponent<SpriteRenderer>().color = temp;
            alpha -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        Intro.SetActive(false);
        yield return new WaitForSeconds(1f);

        player.gameObject.SetActive(true);
        for (int i = 0; i < dialouges.Length; i++)
        {
            dialougeText.text = dialouges[i];
            yield return new WaitForSeconds(3f);
        }
        
        player.mySpeed = 0;
        player.move = true;
        dialougeText.text = "thats how I move";
        player.mySpeed = 4;
        yield return new WaitForSeconds(4f);
        dialougeText.text = "I am going to the next level, see you there";
        player.move = false;
        player.gameObject.SetActive(false);
        UI.SetActive(true);

    }
}
