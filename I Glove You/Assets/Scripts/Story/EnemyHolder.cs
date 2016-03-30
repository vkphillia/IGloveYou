using UnityEngine;
using System.Collections;

public class EnemyHolder : MonoBehaviour
{
	public Enemy enemy;
	public Sprite[] enemySprites;


	public void Spawn (int enemyHealth, bool aI, bool hasGlove)
	{
		Enemy enemyCopy = Instantiate (enemy) as Enemy;
		enemyCopy.transform.SetParent (transform);
		enemyCopy.transform.position = new Vector3 (Random.Range (-2.5f, 2.5f), Random.Range (-3.27f, 3.23f));
		enemyCopy.GetComponent<SpriteRenderer> ().sprite = enemySprites [Mathf.FloorToInt (Random.Range (0, enemySprites.Length))];
		//enemyCopy.health = enemyHealth;
		//enemyCopy.AIOn = aI;
		//enemyCopy.hasGlove = hasGlove;

		enemyCopy.gameObject.SetActive (true);
	}


}
