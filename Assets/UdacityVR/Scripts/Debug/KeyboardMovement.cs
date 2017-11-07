using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyboardMovement : MonoBehaviour 
{
	public float speed				= 0.05f;

	bool move 						= false;
	
	public static bool 				look;
	public static bool 				forward;
	public static bool 				back;
	public static bool 				left;
	public static bool 				right;
	public static bool 				up;
	public static bool 				down;


	void LateUpdate() 
	{
 		if (Application.isEditor)
		{
			UpdateInput();

			if(move)
			{
				UpdatePosition();
			}
		}
	}		


	public void UpdateInput()
	{
		forward			= UnityEngine.Input.GetKey(KeyCode.E);
		back			= UnityEngine.Input.GetKey(KeyCode.D);
		left			= UnityEngine.Input.GetKey(KeyCode.S);
		right			= UnityEngine.Input.GetKey(KeyCode.F);
		up				= UnityEngine.Input.GetKey(KeyCode.Q);
		down			= UnityEngine.Input.GetKey(KeyCode.A);

		if(UnityEngine.Input.touchCount >= 1)
		{
			//View.speed 		= 3;
			forward 		= true;
		}

		move 			= forward || back || left || right || up || down;
	}


	public void UpdatePosition()
	{
		Vector3 motion 			= Vector3.zero;
		
		motion += forward	? Camera.main.transform.forward : Vector3.zero;
		motion -= back		? Camera.main.transform.forward : Vector3.zero;
		motion += right		? Camera.main.transform.right 	: Vector3.zero;
		motion -= left		? Camera.main.transform.right 	: Vector3.zero;
		motion += up		? Camera.main.transform.up 		: Vector3.zero;
		motion -= down		? Camera.main.transform.up 		: Vector3.zero;

		motion = Vector3.Normalize(motion);

		gameObject.transform.position += motion;
	}
}
