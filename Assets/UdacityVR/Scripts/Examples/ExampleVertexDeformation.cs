using UnityEngine;
using System.Collections;

public class ExampleVertexDeformation : MonoBehaviour 
{
	public Material ocean_material;

	void Start () 
	{
		Ocean.gameObject.SetActive(true);	
		Ocean.gameObject.transform.position 	= new Vector3(0.0f, 0.0f, 0.0f);
		Ocean.gameObject.transform.localScale 	= Vector3.one * 32.0f;

		Ocean.gameObject.GetComponent<MeshRenderer>().material = ocean_material;	
	}
}
