using UnityEngine;
using System.Collections;

public class BorderHorizontal : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D other)
	{

		if (other.gameObject.layer == 8)
		{
			GameManager.Instance.players [0].transform.Rotate (0, 0, (180 - other.transform.rotation.eulerAngles.z) - other.transform.rotation.eulerAngles.z);
		}
		if (other.gameObject.layer == 9)
		{
			GameManager.Instance.players [1].transform.Rotate (0, 0, (180 - other.transform.rotation.eulerAngles.z) - other.transform.rotation.eulerAngles.z);
		}

	}


}
