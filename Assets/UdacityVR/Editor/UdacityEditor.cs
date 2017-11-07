//using UnityEngine;
//using System.Collections;
//
//[ExecuteInEditMode]
//[RequireComponent(typeof(Waypoint))]
//public class WaypointVisualization : MonoBehaviour
//{
//	public Vector3 offset 			= new Vector3(0.0f, 0.5f, 0.0f);
//
//	void Update()
//	{
//		Waypoint[] neighbor = gameObject.GetComponent<Waypoint>().neighbor;
//
//		for(int i = 0; i < neighbor.Length; i++)
//		{
//			if(neighbor[i] != null)
//			{
//				Debug.DrawLine(gameObject.transform.position + offset, neighbor[i].transform.position,Color.green);
//			}
//		}
//	}
//}


using UnityEditor;
using UnityEngine;

////[ExecuteInEditMode]
[InitializeOnLoad]
public class SceneGUI
{
//	static SceneGUI()
//	{
//		if(EditorApplication.isPlaying)
//		{
//			EditorApplication.update = ToggleOnScene;
//		}
//
//		EditorApplication.playmodeStateChanged = ToggleOnScene;
//	}
//	
//	public static void ToggleOnScene()
//	{
//		if(EditorApplication.isPlaying)
//		{
//			if(SceneView.onSceneGUIDelegate != OnScene)
//			{
//				SceneView.onSceneGUIDelegate += OnScene;
//				Debug.Log("Scene GUI : Enabled");
//			}
//		}
//		else if (SceneView.onSceneGUIDelegate == OnScene)
//		{
//			SceneView.onSceneGUIDelegate -= OnScene;
//			Debug.Log("Scene GUI : Disabled");
//		}
//	}
//
//	private static void OnScene(SceneView sceneview)
//	{
//		Handles.BeginGUI();
//		
//		DisplayVertexIndex();
//		
//		Handles.EndGUI();
//	}
//
//	private static void DisplayVertexIndex()
//	{
//  		
//		if(Main.screen_vertex != null && Camera.current != null)
//		{
//			float aspect = Camera.current.aspect;
//			float height = Camera.current.pixelHeight;
//			for(int i = 0; i < Main.screen_vertex.Length; i++)
//			{
//				Vector3 vertex	= Camera.current.WorldToScreenPoint(Main.screen_vertex[i]);
//				GUI.skin.font.material.color = Color.white;	
//				GUI.skin.label.fontSize = 12;
//				string label	= i.ToString();
//				GUI.Label(new Rect(vertex.x, height - vertex.y, 128, 16), label);
//				
//				GUI.skin.label.fontSize = 8;
//				label	= Main.screen_triangles[i * 6 + 0].ToString() + ",";
//				label	+= Main.screen_triangles[i * 6 + 1].ToString() + ",";
//				label	+= Main.screen_triangles[i * 6 + 2].ToString() + "\n";
//				label	+= Main.screen_triangles[i * 6 + 3].ToString() + ",";
//				label	+= Main.screen_triangles[i * 6 + 4].ToString() + ",";
//				label	+= Main.screen_triangles[i * 6 + 5].ToString();
//				GUI.Label(new Rect(vertex.x, height - vertex.y + 12, 128, 32), label);
//				
//			}
//		}
//	}
}