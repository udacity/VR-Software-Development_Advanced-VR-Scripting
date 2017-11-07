using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LeafAnimation : MonoBehaviour 
{
	public float blend						= 0.95f;
	public float force						= 0.95f;
	public float speed						= 1.0f;

	private Vector3 _seed					= Vector3.zero;

	private Quaternion _origional_rotation 	= Quaternion.identity;
	void Start() 
	{
		_seed				= Random.onUnitSphere;
		_origional_rotation = gameObject.transform.rotation;
	}

	void Update() 
	{
		force 			= Mathf.Clamp01(force);
		float time		= Time.time * speed;
		float phase		= Mathf.Cos(time+Mathf.Sin(time*0.5f)+Mathf.Sin(time*0.3f));
		
		gameObject.transform.Rotate(_seed * phase * force);

		gameObject.transform.rotation = Quaternion.Lerp(_origional_rotation, gameObject.transform.rotation, blend);
	}
}


