using UnityEngine;
using System.Collections;

public class ExampleEvent : MonoBehaviour
{
	private bool _focused	= false;


	public void Update()
	{
		 Scale ();
	}


	public void Enter () 
	{
		_focused = true;
	}


	public void Exit()
	{
		_focused = false;
	}


	public void Clicked () 
	{
		if(_focused)
		{
			gameObject.transform.rotation 							= Random.rotation;
			gameObject.GetComponent<MeshRenderer>().material.color 	= Random.ColorHSV();
		}
	}


	public void Scale () 
	{
		if(_focused)
		{
			gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, Vector3.one * 2.0f, 0.1f);
		}
		else
		{
			gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, Vector3.one, 0.1f);
		}
	}
}
