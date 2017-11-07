using UnityEngine;
using System.Collections;

public class ExampleFlocking : MonoBehaviour 
{
	public float radius						= 8.0f;
	public bool draw_alignment				= true;
	public bool draw_avoidance				= true;
	public bool draw_cohesion				= true;
	public bool draw_radius					= true;
	public bool pause						= false;

	public float velocity					= 1.0f;
	public float alignment_weight			= 0.25f;
	public float avoidance_weight			= 0.65f;
	public float cohesion_weight			= 0.6f;
	public float bounds						= 64.0f;
	public float smoothing					= 0.125f;
	
	private Vector3 _alignment				= Vector3.zero;
	private Vector3 _avoidance				= Vector3.zero;
	private Vector3 _cohesion				= Vector3.zero;
	private Vector3 _center					= Vector3.zero;

	public float line_length				= 4.0f;
	
	private const int _COUNT				= 128;
	private GameObject[] _flock 			= new GameObject[_COUNT];
	private GameObject[] _flock_last_frame	= new GameObject[_COUNT];
	private GameObject _selection			= null;

	private Color _selection_color	 		= new Color(0.0f, 1.0f, 0.0f, 1.0f);
	private Color _default_color			= new Color(0.0f, 0.0f, 0.0f, 1.0f);
	private Color _neighborhood_color 		= new Color(1.0f, 1.0f, 1.0f, 1.0f);


	private Color _direction_line_color 	= new Color(0.5f, 0.5f, 1.0f, 0.45f);
	private Color _center_line_color 		= new Color(0.5f, 1.0f, 0.5f, 0.45f);
	private Color _avoidance_line_color 	= new Color(1.0f, 0.5f, 0.5f, 0.45f);


	void Start () 
	{
		//initialize the flock array of gameobjects 
		for(int i = 0; i < _flock.Length; i++)
		{
			_flock[i] 							= GameObject.CreatePrimitive(PrimitiveType.Cube);
			_flock[i].transform.localScale		= Vector3.one * 0.5f;
			_flock[i].transform.position 		= Random.insideUnitSphere * 32.0f;
			_flock[i].transform.localRotation 	= Random.rotation;
			_flock[i].transform.parent			= gameObject.transform;
			_flock[i].name						= "Flock " + i;
			_flock[i].GetComponent<MeshRenderer>().material.color = _default_color;
			Destroy(_flock[i].GetComponent<BoxCollider>());
		}

		//set the selection for the debugging visualization
		SetSelection(_flock[0]);

		_alignment 	= _selection.transform.forward;
		_avoidance 	= _selection.transform.forward;
		_center		= _selection.transform.forward;		
	}


	void Update()
	{
		//for each cube check its neighborhood and update it's position based on the last frame
		_flock_last_frame = _flock ;
		for(int i = 0; i < _flock.Length; i++)
		{
			UpdateFlock(_flock[i], _flock_last_frame);
		}
	

		#if (UNITY_EDITOR)
		UpdateSelection();
		#endif
		
	}	

	void UpdateFlock(GameObject current_object, GameObject[] flock)
	{
		//these are the directions gathered to create the new heading
		Vector3 alignment_direction 	= Vector3.zero;
		Vector3 avoidance_direction 	= Vector3.zero;
		Vector3 center_position			= Vector3.zero;
		Vector3 cohesion_direction		= Vector3.zero;

		float neighbor_count 			= 0.0f;
		
		//check all cubes to find the ones in the local neighborhood
		for(int i = 0; i < flock.Length; i++)
		{
			float range 				= Vector3.Distance(current_object.transform.position, flock[i].transform.position);

			//Add the information about this neighbor if it is in range
			if(range < radius)
			{
				neighbor_count++;
			
				alignment_direction 	+= flock[i].transform.forward;
				center_position			+= flock[i].transform.position;
				avoidance_direction		+= current_object.transform.position - flock[i].transform.position;

				//if the current object is the selection, draw debug lines to this particular neighbor
				if(current_object == _selection)
				{
					if(draw_avoidance)	Debug.DrawLine(current_object.transform.position, _flock[i].transform.position, _avoidance_line_color);
					if(draw_cohesion)	Debug.DrawLine(_flock[i].transform.position, _center, _center_line_color);
					if(draw_alignment)	Debug.DrawLine(_flock[i].transform.position, _flock[i].transform.position + _flock[i].transform.forward * 4.0f, _direction_line_color);
				}
			}
		}

		//find the average position of the neighbors to determine the center of the local neighborhood, then find the direction to it from this cube		
		center_position 						= center_position/neighbor_count;
		cohesion_direction						= center_position-current_object.transform.position;
		

		//normalize all the contributions, and then apply their weights
		cohesion_direction						= Vector3.Normalize(cohesion_direction)  * cohesion_weight;
		alignment_direction						= Vector3.Normalize(alignment_direction) * alignment_weight;
		avoidance_direction						= Vector3.Normalize(avoidance_direction) * avoidance_weight;

		//volia, the new flocking vector
		Vector3 new_direction					= Vector3.Normalize(cohesion_direction + alignment_direction + avoidance_direction);

		//a bit of smoothing
		new_direction							= Vector3.Lerp(current_object.transform.forward, new_direction, smoothing);

		if(!pause)
		{
			//apply this direction and move
			current_object.transform.forward 		= Vector3.Normalize(current_object.transform.forward + new_direction);
			current_object.transform.position 		+= current_object.transform.forward * velocity;
			
			
			
			//check to see if it is out of bounds
			if(Vector3.Magnitude(current_object.transform.position) > bounds)
			{
				//if so, point it back inside and move it a bit, too
				current_object.transform.forward 	= Vector3.Normalize(-current_object.transform.forward - new_direction);
				current_object.transform.position 	= Vector3.Lerp(current_object.transform.position, Vector3.ClampMagnitude(current_object.transform.position, bounds)-current_object.transform.forward, velocity);
			}
		}

		SetColor(current_object);

		//draw the rest of the debug lines here
		if(current_object == _selection)
		{
			_center		= center_position;
			_cohesion	= cohesion_direction;
			_alignment	= alignment_direction;
			_avoidance	= avoidance_direction;
			
			Debug.DrawLine(current_object.transform.position, current_object.transform.position + current_object.transform.forward * line_length, Color.green);
			Debug.DrawLine(current_object.transform.position, current_object.transform.position + _selection.transform.forward * line_length, Color.white);
			Debug.DrawLine(current_object.transform.position, current_object.transform.position + _cohesion  * line_length * 0.5f, Color.green);
			Debug.DrawLine(current_object.transform.position, current_object.transform.position + _alignment * line_length * 0.5f, Color.blue);
			Debug.DrawLine(current_object.transform.position, current_object.transform.position + _avoidance * line_length * 0.5f, Color.red);
		}
	}


	void SetSelection(GameObject game_object)
	{
		_selection = game_object;
	}


	void SetColor(GameObject game_object)
	{
		bool in_range_of_selection = Vector3.Distance(_selection.transform.position, game_object.transform.position) < radius;
		
		game_object.GetComponent<MeshRenderer>().material.color = in_range_of_selection ? game_object == _selection ? _selection_color : _neighborhood_color : _default_color;
	}


	#if (UNITY_EDITOR)
	void UpdateSelection ()
	{
		if(UnityEditor.Selection.activeGameObject != _selection)
		{	
			for(int i = 0; i < _flock.Length; i++)
			{
				if(UnityEditor.Selection.activeGameObject == _flock[i])
				{
					SetSelection(_flock[i]);
					break;
				}
			}
		}	
	}


	void OnDrawGizmos()
	{
		if(_selection != null && draw_radius)
		{
			Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.25f);
  			Gizmos.DrawWireSphere(_selection.transform.position, radius);
			Gizmos.DrawWireSphere(Vector3.zero, bounds);
		}
	}
	#endif
}
