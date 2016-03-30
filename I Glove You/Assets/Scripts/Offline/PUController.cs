using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PUController : MonoBehaviour
{
	[HideInInspector]
	public GameObject PU;
	public List<GameObject> PUList = new List<GameObject> ();
	public GameObject glove;

	//PU spawning
	public List<Transform> spawnPointsArr = new List<Transform> ();
	public List<Transform> spawnPointsArrTemp = new List<Transform> ();

	private float[] PUWeightTable = null;


	void Awake ()
	{
		// Table to Store probability of PUS
		CreateWeightTable ();
		OfflineManager.SpwanFirstGlove += Spawn;
	}

	void Start ()
	{
		OfflineManager.Instance.PUPicked = true;

	}

	void Update ()
	{
		if (OfflineManager.Instance.currentState == GameState.Playing)
		{
			if (OfflineManager.Instance.PUPicked)
			{
				StartCoroutine (SpawnPUCoroutine ());
			}
			if (OfflineManager.Instance.glovePicked)
			{
				StartCoroutine (SpawnGloveCoroutine ());
			}

		}
		else
		{
			StopCoroutine (SpawnPUCoroutine ());
			StopCoroutine (SpawnGloveCoroutine ());
		}
	}

	//spawn power ups code
	public IEnumerator SpawnPUCoroutine ()
	{
		OfflineManager.Instance.PUPicked = false;
		int PUIndex = GetPUIndex ();
		PU = PUList [PUIndex];

		yield return new WaitForSeconds (2f);
		PU.SetActive (true);
		SpawnAnything (PU);
	}

	//Helps pick PU based on it probability
	private int GetPUIndex ()
	{
		//generate a random number between 0 -1
		float t = Random.value;
		float q = 0.0f;
		
		for (int i = 0; i < PUList.Count; i++)
		{
			//increment q with the weight of the current PU
			q += PUWeightTable [i];
			if (t <= q)
			{
				return i;
			}
		}
		return 0;
	}

	private void CreateWeightTable ()
	{
		int noOfPU = PUList.Count;
		int i = 0;
		int sum = 0;
		
		//create a table of the length of the number of PU
		PUWeightTable = new float[noOfPU];

		for (i = 0; i < noOfPU; i++)
		{
			PowerUp PUScript = PUList [i].GetComponent<PowerUp> ();
		
			if (PUList != null)
			{
				sum += PUScript.weight;

				//store the weight in the weight table
				PUWeightTable [i] = (float)PUScript.weight;
			}
		}
		for (i = 0; i < noOfPU; i++)
		{
			PUWeightTable [i] /= sum;
		}
	}

	//glove
	void SpawnGlove ()
	{
		glove.SetActive (true);
		SpawnAnything (glove);
	}

	//spawn gloves code
	public IEnumerator SpawnGloveCoroutine ()
	{
		OfflineManager.Instance.glovePicked = false;
		yield return new WaitForSeconds (7f);
		SpawnGlove ();
	}

	//for first glove triggered through event
	void Spawn ()
	{
		Invoke ("SpawnGlove", 4f);
		OfflineManager.Instance.glovePicked = false;
	}


	public void SpawnAnything (GameObject spawnObj)
	{
		if (spawnPointsArrTemp.Count > 0)
		{
			StartCoroutine (ReAddSpawnPoint ());
		}
		int _randomPos = Random.Range (0, spawnPointsArr.Count);
		spawnObj.transform.position = spawnPointsArr [_randomPos].position;
		spawnPointsArrTemp.Add (spawnPointsArr [_randomPos]);
		spawnPointsArr.RemoveAt (_randomPos);
	}

	//resize both spawn point array after every 5 seconds
	public IEnumerator ReAddSpawnPoint ()
	{
		yield return new WaitForSeconds (5f);
		spawnPointsArr.Add (spawnPointsArrTemp [0]);
		spawnPointsArrTemp.RemoveAt (0);
	}


	void OnDestroy ()
	{
		OfflineManager.SpwanFirstGlove -= Spawn;
	}





}
