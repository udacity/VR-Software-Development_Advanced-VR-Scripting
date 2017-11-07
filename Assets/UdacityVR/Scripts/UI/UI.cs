using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour//, IGvrGazeResponder
{
	public GameObject options_object;
	public GameObject main_object;
	public GameObject scenes_object;
	public GameObject overlay_object;
	public GameObject reticle_object;

//	private int _trigger_count					= 0;
//	private int _trigger_frame_count			= 0;
//	private const int _trigger_frame_window		= 32;
	
	private Material _reticle_material;
//	private Color _reticule_default_color		= new Color(1.0f, 1.0f, 1.0f, 0.5f);

	private UiState	_state 						= UiState.Hidden;
	private UiState	_state_previous				= UiState.Main;	

	private enum UiState
	{
		Main,
		Scenes,
		Options,
		Hidden
	};

	void Start()
	{
//		SetGazedAt(false);

//		_reticle_material = reticle_object.GetComponent<MeshRenderer>().material;
	}

//	void LateUpdate() 
//	{
//		GvrViewer.Instance.UpdateState();
//
//		UpdateTrigger();
//
//		UpdateBackButton();
//
//		UpdateState();
//	}
//
//	void UpdateTrigger()
//	{
//		if(GvrViewer.Instance.Triggered)
//		{
//			_trigger_count++;
//			_trigger_frame_count++;
//
//			_reticle_material.color = Color.HSVToRGB(1.0f-_trigger_count/3.0f, 1.0f, 1.0f);
//		}
//
//		if(_trigger_frame_count > 0)
//		{
//			_trigger_frame_count++;
//			_reticle_material.color = Color.Lerp(_reticule_default_color, _reticle_material.color, 0.94f);
//		}
//		
//		if (_trigger_frame_count > _trigger_frame_window)
//		{
//			switch(_trigger_count)
//			{
//				case 2:
//					break;
//				case 3:
//					break;
//				default:
//					break;
//			}
//
//			_trigger_frame_count	= 0;
//			_trigger_count			= 0;
//
//			_reticle_material.color 	= _reticule_default_color;
//		}
//	}
//
//	void UpdateBackButton()
//	{
//		if (GvrViewer.Instance.BackButtonPressed)
//		{
//			Application.Quit();
//		}
//	}

	void UpdateState()
	{
		switch(_state)
		{
			case UiState.Hidden:
				main_object.SetActive(false);
				scenes_object.SetActive(false);
				options_object.SetActive(false);
				break;
			case UiState.Main:
				main_object.SetActive(true);
				scenes_object.SetActive(false);
				options_object.SetActive(false);
				break;
			case UiState.Scenes:
				main_object.SetActive(false);
				scenes_object.SetActive(true);
				options_object.SetActive(false);
				break;
			case UiState.Options:
				main_object.SetActive(false);
				scenes_object.SetActive(false);
				options_object.SetActive(true);
				break;
			default:
				break;
		}
	}


	public void SetGazedAt(bool gazedAt)	
	{
		//Debug.Log("Gazed At");
		//GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
	}


	public void Reset()
	{
		Debug.Log("Reset");
	}


	public void ToggleHidden()
	{
		if(_state == UiState.Hidden)
		{
			_state = UiState.Main;
		}
		else
		{
			_state = UiState.Hidden;
		}
	}


	public void TransitionToMain()
	{
		SetPreviousState();
		_state = UiState.Main;
	}


	public void TransitionToOptions()
	{
		SetPreviousState();
		_state = UiState.Options;
	}


	public void TransitionToScenes()
	{
		SetPreviousState();
		_state = UiState.Scenes;
	}


	public void TransitionBack()
	{
		SetPreviousState();
	}


	public void SetPreviousState()
	{
		UiState state_temp	= _state;
		_state 				= _state_previous;
		_state_previous 	= state_temp;
	}


	public void LoadScene0()
	{
		SceneManager.LoadScene(0);
	}


	public void LoadScene1()
	{
		SceneManager.LoadScene(1);
	}


	public void LoadScene2()
	{
		SceneManager.LoadScene(2);
	}


	public void LoadScene3()
	{
		SceneManager.LoadScene(3);
	}


	public void LoadScene4()
	{
		SceneManager.LoadScene(4);
	}


	public void ToggleOverlay()
	{
		overlay_object.SetActive(!overlay_object.activeSelf);
	}

//
//	public void ToggleVRMode()
//	{
//		Debug.Log("Toggle Vr Mode");
//	
//		GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
//	}
//
//
//	public void ToggleDistortionCorrection()
//	{
//		Debug.Log("Toggle Distortion Corretion");
//
//		switch(GvrViewer.Instance.DistortionCorrection) 
//		{
//			case GvrViewer.DistortionCorrectionMethod.Unity:
//				GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Native;
//				break;
//			case GvrViewer.DistortionCorrectionMethod.Native:
//				GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.None;
//				break;
//			case GvrViewer.DistortionCorrectionMethod.None:
//			default:
//				GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Unity;
//				break;
//		}
//	}
//
//
//	public void ToggleDirectRender()
//	{
//		Debug.Log("Toggle Direct Render");
//
//		GvrViewer.Controller.directRender = !GvrViewer.Controller.directRender;
//	}
//
//
//	#region IGvrGazeResponder implementation
//	/// Called when the user is looking on a GameObject with this script,
//	/// as long as it is set to an appropriate layer (see GvrGaze).
//	public void OnGazeEnter()
//	{
//		SetGazedAt(true);
//	}
//
//	/// Called when the user stops looking on the GameObject, after OnGazeEnter
//	/// was already called.
//	public void OnGazeExit()
//	{
//		SetGazedAt(false);
//	}
//
//	public void OnGazeTrigger()
//	{
//	
//	}
//	#endregion
}
