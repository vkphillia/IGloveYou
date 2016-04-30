using UnityEngine;
using System.Collections;

public class FlyingText : MonoBehaviour
{

	public TextMesh myBlackText;
	public TextMesh myGreenText;
	private Animator myAnim;

	void Awake ()
	{
		myAnim = GetComponent<Animator> ();
	}

	void OnEnable ()
	{
		myAnim.Play ("FlyingText_Fly");
		StartCoroutine (RemoveText ());

	}


	IEnumerator RemoveText ()
	{
		yield return new WaitForSeconds (0.5f);
		transform.parent = null;
		GameObjectPool.GetPool ("FlyingTextPool").ReleaseInstance (transform);
	}
}
