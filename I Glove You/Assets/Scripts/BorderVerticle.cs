using UnityEngine;
using System.Collections;

public class BorderVerticle : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 || other.gameObject.layer == 10)
		{
			other.transform.Rotate (0, 0, (360 - other.transform.rotation.eulerAngles.z) - other.transform.rotation.eulerAngles.z);
		}
	}
}
