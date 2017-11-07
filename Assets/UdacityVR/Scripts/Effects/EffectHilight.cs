using UnityEngine;
using System.Collections;

public class EffectHilight : MonoBehaviour
{
	public Color		hilight_color			= Color.white;
	public float 		speed					= 0.01f;

	private	Color		_origional_color		= Color.black;
	private float		_hilight				= 0.0f;
	private float		_hilight_fade_speed		= 0.05f;
	private bool 		_focused 				= false;
	private Material	_material;


	void Start()
	{
		_material			= gameObject.GetComponent<MeshRenderer>().material;	
		_origional_color	= _material.color;
	}


	void Update()
	{
		if(_focused)
		{
			_hilight 		= 0.5f;
		}	
			
		_material.color		= Color.Lerp(_origional_color, hilight_color, _hilight);
		_hilight 			-= _hilight_fade_speed;
	}


	public void Enter () 
	{
		_focused = true;
	}


	public void Exit()
	{
		_focused = false;
	}
}
