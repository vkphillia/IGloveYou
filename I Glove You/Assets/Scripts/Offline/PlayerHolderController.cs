﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class PlayerHolderController : MonoBehaviour
{
	[HideInInspector]	
	public bool hit;

	[HideInInspector]	
	public bool hitter;

	//[HideInInspector]
	public int myHealth;

	[HideInInspector]
	public int roundWins;

	[HideInInspector]
	public bool hasGlove;

	public Sprite[] mySprites;
	public Animator myPunchAnim;

	private SpriteRenderer mySprite;

	public Text myWinText_HUD;
	public Text myHealthText_HUD;




	public float mySpeed;

	private Vector3 force;
	private bool PUHitter;

	//for puff effect when punch hits PU
	private Transform myPooledPunchPU_FX;
	private PunchPU_FX PunchPU_Obj;

	//for hit effect when punch hits opponent
	private Transform myPooledHit_FX;
	private GetHit_FX Hit_Obj;

	//for flying text
	private Transform myPooledFT;
	private FlyingText FT_Obj;

	//for health
	public Transform myFlyingTextSpawnPoint;
	//new health meter
	public Image myHealthBar;

	//new punch anim
	public Sprite myPunchSprite;
	private Sprite myOriginalSprite;

	void Start ()
	{
		mySprite = GetComponent<SpriteRenderer> ();
		myOriginalSprite = mySprite.sprite;
		myHealth = OfflineManager.Instance.MaxHealth;
		mySpeed = OfflineManager.Instance.MaxSpeed;
		myHealthText_HUD.text = myHealth.ToString ();

	}

	void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -2.7f, 2.7f), Mathf.Clamp (transform.position.y, -3.8f, 3.7f), 0);


			if (!hit && !hitter && !PUHitter)
			{
				transform.position += transform.up * Time.deltaTime * mySpeed;
			}
			else if (hit)
			{
				transform.position += transform.up * Time.deltaTime * (mySpeed + 2);
				StartCoroutine (MakeHitFalse ());

			}
			else if (hitter)
			{
				transform.position += transform.up * Time.deltaTime * (-mySpeed + 1);
				StartCoroutine (MakeHitterFalse ());
			}
			else if (PUHitter)
			{
				transform.position += transform.up * Time.deltaTime * (mySpeed + .5f);
				StartCoroutine (MakePUHitterFalse ());
			}

		}
		else if (OfflineManager.Instance.currentState == GameState.MatchOver)
		{
			if (roundWins == 2)
			{
				transform.position += transform.up * Time.deltaTime * mySpeed;
				transform.Rotate (0, 0, 5);
			}
			else
			{
				gameObject.SetActive (false);
			}
		}
	}



	void OnTriggerEnter2D (Collider2D other)
	{
		if (this.gameObject.layer == 8 && other.gameObject.layer == 11) // this = player1, other= player2
		{   
			//Debug.Log ("Player 1 gets punched");
			getPunched (other.transform);
			OfflineManager.Instance.PlayerHolder2.Punch ();

		}
         
		if (this.gameObject.layer == 10 && other.gameObject.layer == 9)
		{
			//Debug.Log ("Player 2 gets punched");
			getPunched (other.transform);
			OfflineManager.Instance.PlayerHolder1.Punch ();
		}	
	}


	//this ensures that the player is going in its forward direction after being punched
	IEnumerator MakeHitFalse ()
	{
		yield return new WaitForSeconds (.5f);
		hit = false;
	}

	//this ensures that the player is going int its forward direction after hitting other player
	IEnumerator MakeHitterFalse ()
	{
		yield return new WaitForSeconds (.5f);
		hitter = false;
	}

	IEnumerator MakePUHitterFalse ()
	{
		yield return new WaitForSeconds (.5f);
		PUHitter = false;
	}

	//Reset on new Round/Match
	public void ResetPlayer ()
	{
		gameObject.SetActive (true);		
		myWinText_HUD.text = roundWins.ToString ();
		hit = false;
		hitter = false;
		myHealth = OfflineManager.Instance.MaxHealth;
		myHealthText_HUD.text = myHealth.ToString ();
		myPunchAnim.gameObject.SetActive (false);
		//HitEffectSprite.enabled = false;
		mySpeed = OfflineManager.Instance.MaxSpeed;
		hasGlove = false;
		myHealthBar.fillAmount = 1;
	}

	public void getPunched (Transform t)
	{
		hit = true;
		SpawnHit_FX ();
		transform.rotation = t.rotation; 
		if (myHealth > 0)
		{
			AlterHealth (-1);
		}	
	}


	//punch other objects and player
	public void Punch ()
	{
		hitter = true;
		StartCoroutine (PlayPunchAnim ());
		SoundsController.Instance.PlaySoundFX ("Punch", 0.6f);

	}

	public void PunchPUS (Transform PU)
	{
		PUHitter = true;
		StartCoroutine (PlayPunchAnim ());
		SpawnPunchPU_FX (PU);
		SoundsController.Instance.PlaySoundFX ("BreakPU", 0.5f);

	}

	IEnumerator PlayPunchAnim ()
	{
		int randPunch = Random.Range (0, 2);
		if (randPunch == 0)
		{
			myPunchAnim.Play ("Punch_Hit");
		
		}
		else
		{
			myPunchAnim.Play ("Punch_Hit2");
		}
		yield return new WaitForSeconds (.5f);
		myPunchAnim.Play ("Punch_Idle");
	}

	//removes glove from player when other player get glove
	public void LoseGlove ()
	{
		hasGlove = false;
		mySprite.sprite = myOriginalSprite;
		myPunchAnim.gameObject.SetActive (false);
	}

	//adds glove to player when other player loses glove
	public void AddGlove ()
	{
		SoundsController.Instance.PlaySoundFX ("GlovePick", 0.6f);
		hasGlove = true;
		mySprite.sprite = myPunchSprite;
		myPunchAnim.gameObject.SetActive (true);
		myPunchAnim.Play ("Punch_Idle");
	}

	//increase or decreases the health of the player based on the amount
	public void AlterHealth (int amount)
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			myPooledFT = GameObjectPool.GetPool ("FlyingTextPool").GetInstance ();
			FT_Obj = myPooledFT.GetComponent<FlyingText> ();
			//FT_Obj.transform.SetParent (this.transform);
			FT_Obj.transform.position = myFlyingTextSpawnPoint.position;
			FT_Obj.transform.rotation = myFlyingTextSpawnPoint.rotation;
			//new health bar

			if (amount > 0)
			{
				FT_Obj.myGreenText.color = Color.green;
				FT_Obj.myBlackText.text = "+" + amount.ToString ();
				FT_Obj.myGreenText.text = "+" + amount.ToString ();
			}
			else
			{
				FT_Obj.myGreenText.color = Color.red;
				FT_Obj.myBlackText.text = amount.ToString ();
				FT_Obj.myGreenText.text = amount.ToString ();
			}

			if ((myHealth + amount) > OfflineManager.Instance.MaxHealth)
			{
				myHealth = OfflineManager.Instance.MaxHealth;
				myHealthBar.fillAmount = 1f; 

			}
			else if ((myHealth + amount) <= 0)
			{
				myHealth = 0;
				myHealthBar.fillAmount = 0f; 

				//code for checking who wins the round and stops the round
				OfflineManager.Instance.CheckRoundStatus ();
			}
			else
			{
				myHealth += amount;
				myHealthBar.fillAmount = (float)(myHealth) / OfflineManager.Instance.MaxHealth; 

				//only play sound when adding health
				if (amount > 0)
				{
					SoundsController.Instance.PlaySoundFX ("HealthUp", 0.6f); 
					StartCoroutine (ChangeColor (Color.green));
				}
				else
				{
					StartCoroutine (ChangeColor (Color.red));
					StartCoroutine (ChangeHealthBarColor ());
				}
			}

		}



		myHealthText_HUD.text = myHealth.ToString ();
	}

	IEnumerator ChangeHealthBarColor ()
	{
		myHealthBar.color = Color.red;
		yield return new WaitForSeconds (.3f);
		myHealthBar.color = Color.green;
	}


	IEnumerator ChangeColor (Color C)
	{
		mySprite.color = C;
		yield return new WaitForSeconds (.5f);
		mySprite.color = Color.white;
	}

	//Spawining from game object pools
	public void SpawnPunchPU_FX (Transform PU)
	{
		myPooledPunchPU_FX = GameObjectPool.GetPool ("PunchPUPool").GetInstance ();
		PunchPU_Obj = myPooledPunchPU_FX.GetComponent<PunchPU_FX> ();
		PunchPU_Obj.transform.position = PU.position;
		PunchPU_Obj.transform.rotation = Quaternion.identity;
	}

	public void SpawnHit_FX ()
	{
		myPooledHit_FX = GameObjectPool.GetPool ("GetHitPool").GetInstance ();
		Hit_Obj = myPooledHit_FX.GetComponent<GetHit_FX> ();
		Hit_Obj.transform.position = this.transform.position;
		Hit_Obj.transform.rotation = Quaternion.identity;
	}


}