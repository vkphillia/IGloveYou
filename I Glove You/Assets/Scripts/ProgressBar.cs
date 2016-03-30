using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
	int initialRatio;
	float initialScale;

	void Start ()
	{
		initialScale = transform.localScale.x;
	}

	public void SetUpdateBar (int health)
	{
		initialRatio = health;
        
		float rectWidth = health * initialScale / initialRatio;
		transform.localScale = new Vector2 (rectWidth, transform.localScale.y);

	}

	public void UpdateBar (int health)
	{
		float rectWidth = health * initialScale / initialRatio;
		transform.localScale = new Vector2 (rectWidth, transform.localScale.y);
	}
    
}
