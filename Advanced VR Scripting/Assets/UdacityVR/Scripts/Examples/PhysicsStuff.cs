using UnityEngine;
using System.Collections;

public class PhysicsStuff : MonoBehaviour 
{
	private Rigidbody _rigid_body;

	public float force = 1.0f;
	public float radius = 1.0f;

	// Use this for initialization
	void Start () 
	{
		_rigid_body = gameObject.GetComponent<Rigidbody>();

		
	}

	public void Clicked () 
	{
		Vector3 explositon_position = gameObject.transform.position - Camera.main.transform.forward;
		_rigid_body.AddExplosionForce(force, explositon_position, radius);
	}
}
