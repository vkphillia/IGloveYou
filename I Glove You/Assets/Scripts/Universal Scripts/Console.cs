#define DEBUG_BUILD
using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {
	
#if DEBUG_BUILD
	public static void Log (object logObject)
	{
		Debug.Log( logObject);
	}
	public static void LogWarning (object logObject)
	{
		Debug.LogWarning (logObject);
	}
	public static void LogError (object logObject)
	{
		Debug.LogError (logObject);
	}
#else
	public static void Log (object logObject){ }
	public static void LogWarning (object logObject){ }
	public static void LogError (object logObject){	}
#endif	
}
