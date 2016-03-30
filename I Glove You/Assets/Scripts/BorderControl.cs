using UnityEngine;
using System.Collections;

public class BorderControl : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8 || other.gameObject.layer == 10 || other.gameObject.layer == 14)
		{
            int temp = Mathf.RoundToInt((other.transform.eulerAngles.z + 180) % 360);
            other.transform.eulerAngles=new Vector3(0,0,temp) ;
        }
    
	}
}
