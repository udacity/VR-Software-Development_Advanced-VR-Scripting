using UnityEngine;

public class View : MonoBehaviour 
{
	public bool 				focused;	
	public RaycastHit			focus;
	
	public bool 				fade		= false;
	public bool 				mouselook	= false;

	private static Vector2		_mouse;
	private static Vector2		_delta;
	private static Vector2		_sensitivity = new Vector2(2.5f, 0.05f);

	void Start()
	{
		fade = false;
		
		FadeEffect.SetBlack();
		

		if (Application.isEditor)
		{
			mouselook = true;
		}
	}

	void OnGUI()
	{
		if(fade)
		{
			FadeEffect.FadeOut();
		}

		if(!fade)
		{
			FadeEffect.FadeIn();
		}

		if(mouselook)
		{
			MouseLook();
		}
	}

	void MouseLook()
	{
		_mouse 			= UnityEngine.Input.mousePosition;
		_mouse.x		/= Screen.width;
		_mouse.y		/= Screen.height;
		
		_delta.x 		= UnityEngine.Input.GetAxis("Mouse X") * _sensitivity.x;
		_delta.y 		= UnityEngine.Input.GetAxis("Mouse Y") * _sensitivity.y;
		
		if (true) 
		{
			Vector3 position 	= Camera.main.transform.position;

			Camera.main.transform.RotateAround (position, Vector3.up, _delta.x);
			
			Vector3 target 		= position + Camera.main.transform.forward;
			target.y 			= Mathf.Clamp(target.y + _delta.y, position.y - 0.75f, position.y + 0.95f);
			
			Camera.main.transform.LookAt(target);
		}
	}
}
