using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
[InitializeOnLoad]
public class WaypointEditor
{
//	public static bool show_editor 	= false;
//
//	public static bool edit_neighbors 	= false;
//
//	public static GameObject edit_waypoint = null;
//
//
//	public static void DrawArrow(Vector3 start, Vector3 end, Color color)
//	{
//		Vector3 direction	= Vector3.Normalize(start-end);
//		Vector3 orthogonal	= Vector3.Cross(direction, Vector3.up);
//		
//		Vector3 left_arrow	=  orthogonal * 0.125f + direction * 0.125f;
//		Vector3 right_arrow	= -orthogonal * 0.125f + direction * 0.125f;
//		
//		end 				+= direction * 0.25f;
//
//		Debug.DrawLine(start, end, color,0.0f);
//		Debug.DrawLine(end + left_arrow, end, color,0.0f);
//		Debug.DrawLine(end + right_arrow, end, color,0.0f);
//	}
//
//
//	public static void LabelWaypoints()
//	{
//		bool waypoints_exist 				= (Waypoint)GameObject.FindObjectOfType(typeof(Waypoint)) != null;
//		bool camera_is_scene				= Camera.current.name == "SceneCamera";
//
//		if(waypoints_exist && camera_is_scene)
//		{
//			GUISkin prior_skin				= GUI.skin;
//			int prior_font_size				= GUI.skin.font.fontSize;
//			GUI.skin						= (GUISkin)Resources.Load("Editor UI");
//			float height 					= Camera.current.pixelHeight;
//			float offset					= 16.0f;
//
//			
//			if(GUI.Button(new Rect(0.0f, 0.0f, 138.0f, 24.0f), "Toggle Waypoint Editor"))
//			{
//				show_editor 				= !show_editor;
//				edit_neighbors				= false;
//			}
//
//
//			Waypoint[] waypoint 			= (Waypoint[])GameObject.FindObjectsOfType(typeof(Waypoint));
//			if(show_editor)
//			{
//				for(int i = 0; i < waypoint.Length; i++)
//				{
//					if(waypoint[i] != null)
//					{
//						//Get screenspace position for waypoint display rect
//						Vector3 position					= waypoint[i].transform.position;
//						position							= Camera.current.WorldToScreenPoint(position);
//						position.y	 						= height - position.y;
//						Rect box_rect						= new Rect(offset + position.x-4, offset + position.y, 96.0f, 20.0f + 14.0f * waypoint[i].neighborhood.Length);
//
//						//create a button for the background
//						if(GUI.Button(box_rect,""))
//						{
//							if(Selection.activeGameObject == waypoint[i].gameObject)
//							{																
//								if(edit_waypoint == Selection.activeGameObject)
//								{
//									Debug.Log("Disabled Edit");
//									edit_neighbors	= false;
//									edit_waypoint	= null;
//								}
//								else if(!edit_neighbors)
//								{
//									Debug.Log("Set to Edit Mode");
//									edit_waypoint 	= Selection.activeGameObject;
//									edit_neighbors	= true;
//								}
//							}
//							else if(edit_neighbors)
//							{	
//								Debug.Log("Added Neighbor");
//								
//								Waypoint[]	new_neighborhood = new Waypoint[edit_waypoint.GetComponent<Waypoint>().neighborhood.Length + 1];
//
//								for(int j = 0; j < edit_waypoint.GetComponent<Waypoint>().neighborhood.Length; j++)
//								{
//									if(edit_waypoint.GetComponent<Waypoint>().neighborhood[j] != null)
//									{
//										new_neighborhood[j] = edit_waypoint.GetComponent<Waypoint>().neighborhood[j];
//									}
//								}
//
//								new_neighborhood[new_neighborhood.Length-1]			= waypoint[i];	
//								edit_waypoint.GetComponent<Waypoint>().neighborhood	= new_neighborhood;
//								
//								Selection.activeGameObject 							= edit_waypoint;
//								
//								edit_neighbors										= false;
//							}
//							else
//							{
//								Selection.activeGameObject 							= waypoint[i].gameObject;
//							}
//
//
//							//clean neighborhood
//							if(waypoint[i].neighborhood.Length > 0)
//							{
//								//scan for duplicate and count null neighbors
//								for(int j = 0; j < waypoint[i].neighborhood.Length; j++)
//								{
//									for(int k = 0; k < waypoint[i].neighborhood.Length; k++)
//									{
//										if(waypoint[i].neighborhood[j] != null && j != k && waypoint[i].neighborhood[j] == waypoint[i].neighborhood[k] || waypoint[i] == waypoint[i].neighborhood[j])
//										{
//											Debug.Log("Found duplicate " + waypoint[i].neighborhood[j].name);
//											waypoint[i].neighborhood[j] = null;
//										}
//							
//										k = waypoint[i].neighborhood[j] == null ? waypoint[i].neighborhood.Length : k;
//									}
//								}
//								
//								//count missing neighbors
//								int missing		= 0;
//								for(int j = 0; j < waypoint[i].neighborhood.Length; j++)
//								{
//									missing += waypoint[i].neighborhood[j] == null ? 1 : 0;
//								}
//								
//								//count missing neighbors
//								if(missing > 0)
//								{
//									Debug.Log("Found " + missing + " missing neighbors");
//							
//									Waypoint[] new_neighbors = new Waypoint[waypoint[i].neighborhood.Length-missing];
//									
//									int k = 0;
//									for(int j = 0; j < waypoint[i].neighborhood.Length; j++)
//									{
//										if(waypoint[i].neighborhood[j] != null)
//										{
//									 		new_neighbors[k++] = waypoint[i].neighborhood[j];
//										}
//									}
//							
//									waypoint[i].neighborhood = new_neighbors;
//								}
//							}
//						};
//
//
//						bool is_selected 					= Selection.activeGameObject == waypoint[i].gameObject;
//						Color line_color 					= is_selected ? edit_neighbors ?  Color.cyan :  Color.green : Color.magenta * 0.5f;
//						line_color							*= is_selected ? 1.0f 		: 0.75f;
//				
//						//display the list of neighbors
//						string label						= waypoint[i].name;
//						Rect rect							= new Rect(offset + position.x, offset + position.y, 96, 20);
//				
//						GUI.skin.label.normal.textColor 	= is_selected ? line_color : Color.white;
//						GUI.skin.label.fontSize 			= 11;
//						GUI.Label(rect, label);
//						
//						GUI.skin.label.fontSize 			= 9;
//						for(int j = 0; j < waypoint[i].neighborhood.Length; j++)
//						{
//							if(waypoint[i].neighborhood[j] != null)
//							{	
//								DrawArrow(waypoint[i].transform.position, waypoint[i].neighborhood[j].transform.position, line_color);
//								
//								//label neighbor
//								label							= waypoint[i].neighborhood[j].name;
//								rect.y							+= 14;
//								GUI.Label(rect, label);
//
//								//remove neighbor button
//								if(Selection.activeGameObject == waypoint[i].gameObject)
//								{		
//									if(GUI.Button(new Rect(rect.x-14, rect.y, 10, 10), "-"))
//									{
//										waypoint[i].neighborhood[j] = null;
//									}
//								}
//							}
//						}	
//
//
//						if(Selection.activeGameObject == waypoint[i].gameObject && edit_neighbors)
//						{
//							rect			= new Rect(position.x + offset - 4, offset + position.y - 20, 96, 20);
//
//							GUI.Box(rect, "Select Neighbor");
//						}
//						else if(Selection.activeGameObject == waypoint[i].gameObject && !edit_neighbors)
//						{
//							rect			= new Rect(position.x + offset - 4, offset + position.y - 20, 96, 20);
//
//							edit_neighbors 	= GUI.Button(rect, "Add Neighbor");
//							edit_waypoint	= Selection.activeGameObject;
//						}
//					}			
//				}
//			}
//
//			GUI.skin				= prior_skin;	
//			GUI.skin.label.fontSize = prior_font_size;	
//
//			if(!edit_neighbors)
//			{
//				edit_waypoint 		= null;
//			}
//		}
//	}
//
//
//	static WaypointEditor()
//	{
//		if(!EditorApplication.isPlaying)
//		{
//			if(SceneView.onSceneGUIDelegate != OnSceneGUI)
//			{
//				SceneView.onSceneGUIDelegate += OnSceneGUI;
//			}
//		}
//		else if (SceneView.onSceneGUIDelegate == OnSceneGUI)
//		{
//			SceneView.onSceneGUIDelegate -= OnSceneGUI;
//		}
//	}
//	
//
//	private static void OnSceneGUI(SceneView sceneview)
//	{
//		Handles.BeginGUI();
//	
//		if(!UnityEditor.EditorApplication.isPlaying)
//		{
//			LabelWaypoints();
//		}
//
//		Handles.EndGUI();
//	}
}
