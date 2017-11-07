using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Debugging : MonoBehaviour 
{
	/*
	public float embiggen			= 1.15f;
	public float emsmallen			= .999f;
	public float offset				= 1.15f;
	public static RaycastHit hit;			

	public float inside					= 0.0f;
	public float outside					= 8.0f;

	public float speed				= 0.05f;

	public float theta_rate			= 1.0f;
	bool move 						= false;
	
	public static bool 				look;
	public static bool 				forward;
	public static bool 				back;
	public static bool 				left;
	public static bool 				right;
	public static bool 				up;
	public static bool 				down;

	private GameObject _game_object;
	private static Matrix4x4 _matrix_local_to_world;
	
	public const int VERTICES 						= 128;

	public int limit						= VERTICES;

	public Vector3 rgb;

	private static Vector3[] _vertices		= new Vector3[VERTICES];
	
	private static Material _material = null;
	public static Material material
	{
		get
		{
			if(_material == null)
			{
				_material			= new Material(Shader.Find("Trail"));
				_material.hideFlags = HideFlags.DontSave;
			}
			return _material;
		}
	}	


	public void Awake()
	{
		_game_object = new GameObject();
		_game_object.AddComponent<MeshRenderer>();
		_game_object.AddComponent<MeshFilter>();
//		gameObject.AddComponent<MeshFilter>();
		
	}


	void LateUpdate() 
	{
//		/Raycast();
		Disk(_game_object);
		
//		UpdateInput();

		if(move)
		{
//			UpdatePosition();
		}
	}


	void OnPostRender()
	{
		//RenderLines();
	}

	const float PI  = 3.14159265359f;
	const float PHI = 1.61803398875f;
	const float TAU = 6.28318531f;
	
	float Round( float x ) { return Mathf.Floor(x+0.5f); }
	
	float Fract( float a,float b) 
	{ 
		return a*b - Mathf.Floor(a*b); 
	}

	Vector2  Fract( Vector2 a, float b) 
	{
		Vector2 c = a * b;
		return c - new Vector2(Mathf.Floor(c.x), Mathf.Floor(c.x));
	}

	Vector3 id2sf( float i, float n) 
	{
		float phi = TAU*Fract(i, PHI);
		float zi = 1.0f - (2.0f*i+1.0f)/n;
		float sinTheta = Mathf.Sqrt( 1.0f - zi*zi);
		return new Vector3(Mathf.Cos(phi), Mathf.Sin(phi), zi);
	}


	public void Disk(GameObject gameObject)
	{
		

//		gameObject.AddComponent<MeshRenderer>();
//		gameObject.AddComponent<MeshFilter>();

		// Build vertices and UVs
		Vector3[] vertex 		= new Vector3[VERTICES];
		int[] triangle 			= new int[VERTICES*6];
//		Vector2[] uvs 			= new Vector2[VERTICES];
//		Vector4[] tangents 		= new Vector4[VERTICES];
//
//		Vector2 uv_scale 		= new Vector2 (1.0f / (VERTICES - 1.0f),       1.0f / (VERTICES - 1.0f));
//		Vector3 vertex_scale 	= new Vector3 (1.0f / (VERTICES - 1.0f), 1.0f, 1.0f / (VERTICES - 1.0f));

		int j = 0;
		for (int i = 0 ; i < VERTICES-1; i++)
		{
			if(i <= limit)
			{
				vertex[i] 		= id2sf(i, VERTICES);
				
				if(i > 7)
				{
						triangle[j++]	= i;
						triangle[j++]	= i+5;
						triangle[j++]	= i-3;
						triangle[j++]	= i-8;
				
				}
				//Debug.DrawLine(vertex[i], Vector3.zero);
			}
		}

		for (int i = 0 ; i < VERTICES-1; i++)
		{
			if(i <= limit && i < VERTICES - 2)
			{
				if(vertex[i].x != 0.0f)
				{
					Debug.DrawLine(vertex[triangle[3*i]],   vertex[i], Color.red);
					Debug.DrawLine(vertex[triangle[3*i+1]], vertex[i], Color.green);
					Debug.DrawLine(vertex[triangle[3*i+2]], vertex[i], Color.blue);
				}
			}
		}

		if(gameObject.GetComponent<MeshRenderer>() == null)
		{
 			gameObject.AddComponent<MeshRenderer>();
		}
	
		if(gameObject.GetComponent<MeshFilter>() == null)
		{
			gameObject.AddComponent<MeshRenderer>();
		}

		MeshRenderer meshRenderer		= gameObject.GetComponent<MeshRenderer>();
		MeshFilter	meshFilter			= gameObject.GetComponent<MeshFilter>();
		
		if(meshRenderer.material == null)
		{
			meshRenderer.material 			= new Material(Shader.Find("Standard"));
		}

		if(meshFilter.mesh == null)
		{
				meshFilter.mesh = new Mesh();
		}
		
//		Assign the data to the mesh
//		_mesh					= new Mesh();
		 meshFilter.mesh.vertices 			= vertex;
//		 meshFilter.mesh.uv 				= uvs;
		 meshFilter.mesh.triangles			= triangle;
	}

/*
	void RenderLines()
	{
		
		material.SetPass(0);
		
		GL.PushMatrix();
		GL.MultMatrix(Matrix4x4.identity);
		GL.Begin(GL.LINES);
		

		for(int i = 0; i < VERTICES; i++) 
		{
//			Vector4 color = Vector4.Lerp(Flock.color_far, Flock.color_near , 2.0f * Flock.velocity[i/2]/Flock.velocity_multiplier);
//			color.w = 1.0f;
			GL.Color(Color.white);
			GL.Vertex3(_vertices[i].x, _vertices[i].y, _vertices[i].z);
			if(i < VERTICES-1)
			{	
				Debug.DrawLine(_vertices[i], _vertices[i+1]);
			}
		}
//			
//			if(Flock.draw_neighborhood_lines)
//			{
//				Vector3 neighbor = Vector3.zero;
//				
//				for(int i = 0; i < Flock.COUNT; i++) 
//				{
//					if(lines < Flock.MAX_LINES)
//					{
//						int local_neighbor_count = Mathf.Min(Flock.vertex_indices[i, 0], Flock.MAX_NEIGHBOR_LINES);
//					
//						float l		= (float)local_neighbor_count/(float)Flock.max_neighbors;
//						Vector4 color = Vector4.Lerp(Flock.color_far, Flock.color_near , l);
//						
//						for(int j = 0; j < local_neighbor_count; j++)
//						{
//							neighbor	= Flock.prior_position[Flock.vertex_indices[i, j]];
//							
//							if(Vector3.Distance(Flock.prior_position[i], neighbor) < Flock.neighborhood_radius)
//							{
//								GL.Color(color);
//								GL.Vertex3(Flock.prior_position[i].x, Flock.prior_position[i].y, Flock.prior_position[i].z);
//								GL.Vertex3(neighbor.x, neighbor.y, neighbor.z);
//								
//								lines+=2;
//							}
//						}
//					}
//				}
//			}
			
		GL.End();
		GL.PopMatrix();
	} 


	public void UpdateInput()
	{
		forward			= UnityEngine.Input.GetKey(KeyCode.E);
		back			= UnityEngine.Input.GetKey(KeyCode.D);
		left			= UnityEngine.Input.GetKey(KeyCode.S);
		right			= UnityEngine.Input.GetKey(KeyCode.F);
		up				= UnityEngine.Input.GetKey(KeyCode.Q);
		down			= UnityEngine.Input.GetKey(KeyCode.A);

		if(UnityEngine.Input.touchCount >= 1)
		{
			//View.speed 		= 3;
			forward 		= true;
		}

		move 			= forward || back || left || right || up || down;
	}


	public void UpdatePosition()
	{
		Vector3 motion 			= Vector3.zero;
		
		motion += forward	? Camera.main.transform.forward : Vector3.zero;
		motion -= back		? Camera.main.transform.forward : Vector3.zero;
		motion += right		? Camera.main.transform.right 	: Vector3.zero;
		motion -= left		? Camera.main.transform.right 	: Vector3.zero;
		motion += up		? Camera.main.transform.up 		: Vector3.zero;
		motion -= down		? Camera.main.transform.up 		: Vector3.zero;

		motion = Vector3.Normalize(motion);

		gameObject.transform.position += motion;
	}


	private static void Raycast()
	{
		Ray ray 			= new Ray(Camera.main.transform.position, Camera.main.transform.forward);
 		Physics.Raycast (ray, out hit);
	}
}


	public void Disk(GameObject gameObject)
	{
		

//		gameObject.AddComponent<MeshRenderer>();
//		gameObject.AddComponent<MeshFilter>();

		// Build vertices and UVs
		Vector3[] vertex 		= new Vector3[VERTICES];
//		Vector2[] uvs 			= new Vector2[VERTICES];
//		Vector4[] tangents 		= new Vector4[VERTICES];
//
//		Vector2 uv_scale 		= new Vector2 (1.0f / (VERTICES - 1.0f),       1.0f / (VERTICES - 1.0f));
//		Vector3 vertex_scale 	= new Vector3 (1.0f / (VERTICES - 1.0f), 1.0f, 1.0f / (VERTICES - 1.0f));

		float phi				= Mathf.Sqrt(5.0f);
		phi						+= 1.0f;		
		phi						*= 0.5f;

		float theta				= (6.28f)/0.61f;
		float cos 				= Mathf.Cos(theta);
		float sin				= Mathf.Sin(theta);

		// Build triangle indices: 3 
		//indices into vertex array for each triangle
		int[] triangles = new int[(VERTICES - 1) * (VERTICES - 1) * 3];
		

		Vector3[] stack 		= new Vector3[9];
		for(int i = 0; i < 9; i++)		
		{
			stack[i] = Vector3.zero;
		}

		stack[0]			= new Vector3 (0.0f, 0.0f, offset);
		vertex[0]	 		= stack[0];
		
		Vector3[] r 		= new Vector3[VERTICES];
		Vector3[] g 		= new Vector3[VERTICES];
		Vector3[] b 		= new Vector3[VERTICES];

		int ri = 0;
		int gi = 0;
		int bi = 0;
		
		float oembiggen = embiggen;
		for (int i = 0 ; i < VERTICES-1; i++)
		{
			if(i <= limit)
			{
				vertex[i+1] 	= new Vector3(cos * vertex[i].x - sin * vertex[i].z, 0.0f, sin * vertex[i].x + cos * vertex[i].z) * embiggen;
				embiggen 		*= emsmallen;
			}
		}


		int[] ix  = new int[VERTICES];
		int[] iz  = new int[VERTICES];

		float[] x = new float[VERTICES];
		float[] z = new float[VERTICES];

		for(int i = 0; i < VERTICES; i++)
		{
			x[i] = vertex[i].x;
			z[i] = vertex[i].z;
		}

		bool sorted = false;
		while(!sorted)
		{
			bool bubble = false;

			for(int i = 1; i < x.Length; i++)
			{
				if(x[i-1] > x[i])
				{
					bubble 		= true;
					float temp 	= x[i-1];
					x[i-1] 		= x[i];
					x[i] 		= temp;
					break;
				}
				
			}
			sorted = !bubble;
		}

		sorted = false;
		while(!sorted)
		{
			bool bubble = false;

			for(int i = 1; i < z.Length; i++)
			{
				if(z[i-1] > z[i])
				{
					bubble 		= true;
					float temp 	= z[i-1];
					z[i-1] 		= z[i];
					z[i] 		= temp;
					break;
				}
				
			}
			sorted = !bubble;
		}
		
		for(int i = 0; i < x.Length; i++)
		{
			for(int j = 0; j < x.Length; j++)
			{
				if(vertex[j].x == x[i]) ix[i] = j;
				if(vertex[j].z == z[i]) iz[i] = j;
			}
		}

		embiggen = oembiggen;
		int t = 0;
		for (int i = 1 ; i < VERTICES-1; i++)
		{
			Vector3 va = Vector3.zero;
			Vector3 vb = Vector3.zero;
			int ia = 0;
			int ib = 0;
			float minimum = float.MaxValue;
			for(int j = 0; j < vertex.Length; j++)
			{
				if(i != j)
				{
					float distance = Vector3.Distance(vertex[i], vertex[j]);
					if(distance < minimum)
					{	
						vb		= va;
						va		= vertex[j];
						ib 		= ia;
						ia 		= j;
						minimum = distance;
					}
				}
			}
			triangles[t++] = i;
			triangles[t++] = ia;
			triangles[t++] = ib;
			Debug.DrawLine(vertex[i], va, Color.red);
			Debug.DrawLine(vertex[i], vb, Color.green);
			Debug.DrawLine(va, vb, Color.blue);

			//Debug.DrawLine(vertex[ix[i]], vertex[iz[i]], Color.blue);

//			if(i <= limit)
//			{
//				if(i > 2)
//				{
//					Debug.DrawLine(vertex[i], vertex[i-3], Color.red);
//					r[ri++] = vertex[i];
//				}
//				
//				if(i > 4)
//				{
//					Debug.DrawLine(vertex[i], vertex[i-5], Color.green);
//					g[gi++] = vertex[i];
//				}
//				
//				if(i > 7)
//				{
//					Debug.DrawLine(vertex[i], vertex[i-8], Color.blue);
//					b[bi++] = vertex[i];
//				}
//
//				if(i > 7)
//				{
//					triangles[j]		= i - 3;
////					j++;
////					triangles[j]		= Mathf.Max(i - 4, 0);
////					j++;
////					triangles[j]		= Mathf.Max(i - 7, 0);
//				}
//			}
		}
	
		

//		Debug.Log(ri.ToString() + " " + gi.ToString() + " " + bi.ToString() + " ");

		
	
		
		if(gameObject.GetComponent<MeshRenderer>() == null)
		{
 			gameObject.AddComponent<MeshRenderer>();
		}
	
		if(gameObject.GetComponent<MeshFilter>() == null)
		{
			gameObject.AddComponent<MeshRenderer>();
		}

		MeshRenderer meshRenderer		= gameObject.GetComponent<MeshRenderer>();
		MeshFilter	meshFilter			= gameObject.GetComponent<MeshFilter>();
		
		if(meshRenderer.material == null)
		{
			meshRenderer.material 			= new Material(Shader.Find("Standard"));
		}

		if(meshFilter.mesh == null)
		{
				meshFilter.mesh = new Mesh();
		}
		
//		Assign the data to the mesh
//		_mesh					= new Mesh();
		 meshFilter.mesh.vertices 			= vertex;
//		 meshFilter.mesh.uv 				= uvs;
		 meshFilter.mesh.triangles			= triangles;

		 meshFilter.mesh.name 				= "Procedural Mesh";

//		 Auto-calculate vertex normals from the mesh
		 meshFilter.mesh.RecalculateNormals();

//		 Assign tangents after recalculating normals
//		 meshFilter.mesh.tangents 			= tangents;
	}

	*/
}