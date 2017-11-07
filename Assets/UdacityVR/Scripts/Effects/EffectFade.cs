using UnityEngine;
using System.Collections;

public class FadeEffect
{
	private static float _fade			= 1.0f;
	private static float _speed			= .05f;

	private static Material _material 	= null;
	public static Material material
	{
		get
		{
			if(_material == null)
			{
				_material			= new Material(Shader.Find("Fade"));
				_material.hideFlags = HideFlags.DontSave;
			}
			return _material;
		}
	}	

	public static void SetBlack()
	{
		_fade			= 0.0f;
	}


	public static void SetToClear()
	{
		_fade			= 1.0f;
	}


	public static void FadeIn()
	{
		if(_fade < 1.0f)
		{
			_fade = Mathf.Lerp(_fade, 1.0f, _speed);

			_fade = _fade > 0.95f ? 1.0f : _fade;

			material.SetFloat("_Fade", _fade);
			
			Graphics.Blit(null, material);
		}
	}

	public static void FadeOut()
	{
		_fade = Mathf.Lerp(_fade, 0.0f, _speed);

		_fade = _fade < 0.05f ? 0.0f : _fade;

		material.SetFloat("_Fade", _fade);
			
		Graphics.Blit(null, material);
	}
}
