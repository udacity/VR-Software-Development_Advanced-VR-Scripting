using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpringJoint))]
[RequireComponent(typeof(MeshRenderer))]
public class Interaction : MonoBehaviour
{
	public float		force					= 32.0f;
	public bool			locked					= false;
	public bool			attached				= true;	
	public int			interactions			= 3;	
	private int			_interactions			= 0;

	private Rigidbody	_rigid_body;
	private bool 		_focused;


	void Start()
	{
		_rigid_body 											= gameObject.GetComponent<Rigidbody>();
		
		gameObject.GetComponent<SpringJoint>().breakForce		= float.PositiveInfinity;
		gameObject.GetComponent<SpringJoint>().breakTorque		= float.PositiveInfinity;

		if(!attached)
		{
			gameObject.GetComponent<SpringJoint>().breakForce 	= 0;
		}
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
			_rigid_body.AddForceAtPosition(Camera.main.transform.forward * -force, gameObject.transform.position - Camera.main.transform.forward * 0.125f);
				
			_interactions++;

			if(_interactions > interactions && attached && !locked)
			{
				_rigid_body.freezeRotation = false;
					
				Detach();
			}
		}
	}


	private void Detach()
	{
		attached 											= false;
		gameObject.GetComponent<SpringJoint>().breakForce	= 0;
	}
}
