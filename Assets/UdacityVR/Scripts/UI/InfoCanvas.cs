using UnityEngine;
using System.Collections;

public class InfoCanvas : MonoBehaviour
{
	//list of collision objects (one is already attached to the prefab)
	public GameObject[] 		collision;
	
	private CanvasRenderer[]	_canvas_renderer;

	private float 				_opacity		= 1.0f;
	private bool 				_fade			= false;
	private float 				_fade_rate		= 0.125f;

	private UnityEngine.UI.Text _text;
	private UnityEngine.UI.Text _title;


	public string text
	{
		get
		{
			return _text.text;
		}
		set
		{
			_text.text = value;
		}
	}

	public string title
	{
		get
		{
			return _title.text;
		}
		set
		{
			_title.text = value;
		}
	}

	void Awake()
	{
		_canvas_renderer 		= gameObject.GetComponentsInChildren<CanvasRenderer>();
		UnityEngine.UI.Text[] t = gameObject.GetComponentsInChildren<UnityEngine.UI.Text>();

		_text					= t[0];
		_title 					= t[1];
	}


	void Update()
	{	
		Ray ray			= new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		RaycastHit hit	= new RaycastHit();
		Physics.Raycast(ray, out hit);
	
		//check to see if we are looking at one of this objects colliders
		_fade	= false;
		for(int i = 0; i < collision.Length; i++)
		{
			if(collision[i] != null)
			{
				if(hit.rigidbody == collision[i].GetComponent<Rigidbody>())
				{
					_fade = true;
				}
			}
		}	

		Fade();
	}


	private void Fade()
	{
		//fade to opaque or invisible based on the _fade state
		_opacity = !_fade ? Mathf.Lerp(_opacity, 0.0f, _fade_rate) : Mathf.Lerp(_opacity, 1.0f, _fade_rate);
		_opacity = Mathf.Clamp01(_opacity);
		
		for(int i = 0; i < _canvas_renderer.Length; i++)
		{
			_canvas_renderer[i].SetAlpha(_opacity);
		}
	}

}
