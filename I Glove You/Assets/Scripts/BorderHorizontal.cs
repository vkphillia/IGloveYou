using UnityEngine;
using System.Collections;

public class BorderHorizontal : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 || other.gameObject.layer == 10)
		{
			other.transform.Rotate (0, 0, (180 - other.transform.localRotation.eulerAngles.z) - other.transform.localRotation.eulerAngles.z);
		}

	}


}
