using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{
    
	//void Update ()
	//{
 //       if (GameManager.Instance.currentState == GameState.Playing)
 //       {
 //           MobileControls();
 //       }
        
	//}
    
	//void MobileControls ()
	//{
 //       //only for two player mode
 //       if (GameManager.Instance.currentMode == GameMode.TwoPlayer)
 //       {
 //           int count = Input.touchCount;
 //           for (int i = 0; i < count; i++)
 //           {
 //               Touch touch = Input.GetTouch(i);

 //               if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
 //               {
 //                   MoveClockWise(OfflineManager.Instance.PlayerHolder1.transform);
 //               }

 //               if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
 //               {
 //                   MoveAntiClockWise(OfflineManager.Instance.PlayerHolder1.transform);
 //               }

 //               if (touch.position.x < Screen.width / 2 && touch.position.y > Screen.height / 2)
 //               {
 //                   MoveAntiClockWise(OfflineManager.Instance.PlayerHolder2.transform);
 //               }

 //               if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 2)
 //               {
 //                   MoveClockWise(OfflineManager.Instance.PlayerHolder2.transform);
 //               }
 //           }
 //       }
 //       //for single player mode
 //       else if (GameManager.Instance.currentMode == GameMode.SinglePlayer)
 //       {
 //           int count = Input.touchCount;
 //           for (int i = 0; i < count; i++)
 //           {
 //               Touch touch = Input.GetTouch(i);

 //               if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
 //               {
 //                   MoveClockWise(transform);
 //               }

 //               if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
 //               {
 //                   MoveAntiClockWise(transform);
 //               }
 //           }
 //       }
 //   }

	//void MoveClockWise (Transform t)
	//{
	//	t.Rotate (0, 0, 5);
	//}

	//void MoveAntiClockWise (Transform t)
	//{
	//	t.Rotate (0, 0, -5);
	//}
}
