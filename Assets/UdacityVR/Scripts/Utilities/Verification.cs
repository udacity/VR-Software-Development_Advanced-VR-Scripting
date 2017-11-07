using UnityEngine;
using System.Collections;

public class Verification : MonoBehaviour 
{
	private const string _instructions	= "Type your name into the cypher script for verification.";
	
	[Header("Input")]
	public string input 				= "";
	
	[Space(10)]
	[Header("Output")]
	public string output 				= "";

	[Space(10)]
	[Header("Display UI")]
	public InfoCanvas infoCanvas;

	private string 	_cypher;
	private string 	_display;
	private int		_salt;

	void Start () 
	{
		_display			= _instructions;	
		
		infoCanvas.title	= "Cypher";
	}

	void Update () 
	{
		_cypher = Cypher.Encode(input, gameObject.GetHashCode());

		if(name.Length != 0)
		{
			_display 		= input + " : " + _cypher;
			output			= _display;
		}
		else
		{
			_display		= _instructions;
		}
		
		infoCanvas.text	= _display;
	}
}