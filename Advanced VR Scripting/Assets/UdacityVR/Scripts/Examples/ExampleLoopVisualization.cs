using UnityEngine;
using System.Collections;

public class ExampleLoopVisualization : MonoBehaviour 
{

	GameObject[] game_object 			= new GameObject[32];

	Vector3[] loop_position 			= new Vector3[32];
	Vector3[] array_position 			= new Vector3[32];

	private int _index 					= 0;
	private float _interpolant			= 0;

	private Vector3 _index_origin		= Vector3.zero;
	bool loop							= false;

	void Start () 
	{
		float interval = 360.0f/32.0f;

		for(int i = 0; i < 32; i++)
		{		
			game_object[i]						= GameObject.CreatePrimitive(PrimitiveType.Cube);
	
			//move the cube around to create the rotated position for the "loop_position" (lazy)
			game_object[i].transform.position 	= new Vector3(0.0f, 8.0f, 16.0f);
			game_object[i].transform.RotateAround(Vector3.zero, Vector3.forward, i * interval);
			loop_position[i]					= game_object[i].transform.position;

			//create an array position and set the cube on that now
			array_position[i]					= new Vector3((float)(i-16), 0.0f, 16.0f);
			game_object[i].transform.position 	= array_position[i];
			game_object[i].transform.rotation	= Quaternion.identity;
		}

		_index_origin 												= game_object[0].transform.position;
		game_object[0].GetComponent<MeshRenderer>().material.color	= Color.green;
	}
	
	void Update () 
	{
		if(loop)
		{
			UpdateElement (game_object, loop_position);

			if(_interpolant > Mathf.PI)
			{
				game_object[_index].transform.position	= _index_origin;
				
				_index++;
				_index_origin							= game_object[_index].transform.position;
				_interpolant 							= 0.0f;
			} 

			Vector3 direction		= Vector3.Normalize(-_index_origin);
			Operate(game_object[_index], _index_origin, direction);

			if(_index == 31)
			{
				loop 	= false;
				_index 	= 0;
			}
		}
		else
		{
			UpdateElement (game_object, array_position);

			if(_interpolant > 0.0f)
			{
				Operate(game_object[_index], _index_origin, -Vector3.up);	
			
				if(_interpolant > Mathf.PI)
				{
					_interpolant 							= 0.0f;
				} 
			}
			
			_index_origin							= game_object[_index].transform.position;
		}
	}

	void Operate (GameObject g, Vector3 origin, Vector3 direction) 
	{
		_interpolant			+= 0.5f;
		
		g.transform.position	= origin - direction * Mathf.Sin(_interpolant) * 2.0f;

		g.transform.Rotate(Vector3.one * Mathf.Sin(_interpolant) * 8.0f);
	}

	void UpdateElement (GameObject[] game_object, Vector3[] position) 
	{
		for(int i = 0; i < 32; i++)
		{	
			game_object[i].transform.position = Vector3.Lerp(game_object[i].transform.position, position[i], 0.5f);
 			SetColor (game_object[i]);
		}
	}

	void SetColor (GameObject g)
	{
		g.GetComponent<MeshRenderer>().material.color = game_object[_index] == g ? Color.green : Color.white;
	}

	void OnGUI()
	{
		if(!loop)
		{
			Rect rect 	= new Rect(0.0f, 0.0f, 24.0f, 24.0f);
			if(GUI.Button(rect, "-"))
			{
		 		_index--;
			}

			rect.x 		+= rect.width;
			GUI.Button(rect, _index.ToString());

			rect.x 		+= rect.width;
			if(GUI.Button(rect, "+"))
			{
		 		_index++;
			}

			_index 		= _index % 32;
			
			rect.x	 	+= rect.width;
			rect.width 	= 72.0f;
			if(GUI.Button(rect, "Operate"))
			{		
				_interpolant = 0.01f;
			}

			rect.x	 	+= rect.width;
			if(GUI.Button(rect, "Run Loop"))
			{
				_index								= 0;
				loop								= true;			
				_index_origin					 	= new Vector3(0.0f, 8.0f, 10.0f);
			}
		}
	}
}
