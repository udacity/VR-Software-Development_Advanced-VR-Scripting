using UnityEngine;
using System.Collections;

public class ExampleRayCast : MonoBehaviour 
{
	private RaycastHit	_hit;

	private bool 		_focused = false;
	

	void Update () 
	{
		Raycast();

		Scale();
		
		if(Input.anyKeyDown == true)
		{
			Clicked();
		}
	}


	private void Raycast()
	{
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

		Debug.DrawLine(ray.origin, ray.direction * 128.0f);

		Physics.Raycast(ray, out _hit);

		_focused = _hit.collider != null ? _hit.collider.gameObject == gameObject : false;
	}


	private void Scale () 
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


	private void Clicked()
	{
		if(_focused)
		{
			gameObject.transform.rotation 							= Random.rotation;
			gameObject.GetComponent<MeshRenderer>().material.color 	= Random.ColorHSV();
		}
	}
}
