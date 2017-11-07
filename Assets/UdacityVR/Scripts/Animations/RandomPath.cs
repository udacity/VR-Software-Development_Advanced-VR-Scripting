using UnityEngine;
using System.Collections;

public class RandomPath : MonoBehaviour 
{
	public float movement_speed			= 5.0f;
	public float turn_speed				= 0.015f;
	public float radius					= 25.0f;
	public int target_duration			= 3;

	private Vector3 _origin;
	private Vector3 _position;
	private Vector3 _direction;	
	private Vector3 _target;
	private Vector3 _direction_to_target;

	public bool 	debug_lines 						= true;


	void Start () 
	{
		_origin 	= gameObject.transform.localPosition;
		_position 	= _origin;
	}
	

	void FixedUpdate () 
	{
		if(Time.frameCount % Mathf.Max(target_duration, 1) == 0)
		{
			_target				= _origin + Random.onUnitSphere * radius;
			
		}
		_direction_to_target	= Vector3.Normalize(_target - _position);
		_direction				= Vector3.Lerp(_direction,_direction_to_target, turn_speed);
	
		_position				+= _direction * movement_speed;
		
//		if(debug_lines)
//		{
//			Debug.DrawLine(_target, _position, Color.gray);		
//			Debug.DrawLine(_target, _origin, Color.green);		
//			Debug.DrawLine(gameObject.transform.position, _position, Color.white, 1.0f);
//		}

		gameObject.transform.localPosition = _position;
	}
}
