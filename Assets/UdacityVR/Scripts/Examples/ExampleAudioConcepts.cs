using UnityEngine;
using System;

public class ExampleAudioConcepts : MonoBehaviour
{
	public GameObject source_object;
		
	public double frequency 									= 440;			//the pitch of the sound 440 is an "A" note
	public double gain 											= 0.05;			//the volume

	public double falloff 										= 0.00125;		//the master volume adjustment
	public double doppler_effect								= 0.00125;		//the amount of pitch shift for moving sounds

	//the sound generations functions shift these variables to create the sound wave
	private double _phase										= 0.0;			//this is our current position as we move around the sound wave
	private double _amplitude									= 0.0;			//the "volume" of the sound function (which then is adjusted by the gain)
	private double _frequency 									= 0.0;			//the wavelength (which is what determines "pitch")
	private double _theta										= 0.0;			//if the wave were considered like a circle, this is size of the arc per step that we move along to generate the buffer
	private double _doppler										= 0.0;			//this is the "group" volume of the sound
	private double _theta_last_frame							= 0.0;			//for smoothing

	private double _sample_rate;												//the sample rate of the audio system (it ends up being like a quality level)

	//vectors for generating the volume and pitch per ear
	private static double _gain_master							= 0.0125;		//the master volume adjustment
	private static double _gain_left							= 0.0;			//left channel gain adjustment
	private static double _gain_right							= 0.0;			//right channel gain adjustment

	private static Vector3 _position_of_sound 					= Vector3.zero;
	private static Vector3 _direction_to_sound					= Vector3.zero;
	private static Vector3 _direction_of_left_ear				= Vector3.zero;
	private static Vector3 _direction_of_right_ear				= Vector3.zero;
	private static double 	_distance_to_sound					= 0.0;
	private static double  _distance_to_sound_last_frame 		= 0.0;

	public float 	stereo_seperation							= 1.5f;
	public bool 	debug_lines 								= true;

	[SerializeField]
	private int _buffer_length									= 0;
	

	void Awake()
	{
		if(source_object == null)
		{
			source_object			= GameObject.CreatePrimitive(PrimitiveType.Sphere);
			source_object.name 		= "Audio Source";
		}

		_sample_rate			= AudioSettings.outputSampleRate;
		
		_frequency				= frequency;
	}


	void OnAudioFilterRead(float[] buffer, int channels)
	{
		GenerateSineWave(buffer, channels);
	}


	void FixedUpdate()
	{
		SetSourcePosition();

		SetGain();

		SetDoppler();

		SetTheta();

		DebugLines();

		_frequency				= frequency;
	}


	void DebugLines()
	{
		Vector3 position 		= gameObject.transform.position;
		Debug.DrawLine(position, position + _direction_of_left_ear * 4.0f, Color.blue);
		Debug.DrawLine(position, position + _direction_of_right_ear * 4.0f, Color.red);
	
		float phase 			= (float)_phase;
		float theta 			= (float)_theta;
		float amplitude 		= (float)_amplitude;

		Color color				= Color.Lerp(Color.blue, Color.red, (float)_gain_right);
		color.a					= 0.125f;
		Vector3 ortho 			= Vector3.Cross(_direction_to_sound, -Vector3.up);
		
		
		for (int i = 0; i < _buffer_length; i++)
		{
			phase					+= theta;
			phase					= Mathf.Repeat(phase, Mathf.PI * 2.0f);
			
			amplitude		 		= Mathf.Sin(phase);
			
			Vector3 new_position 	= Vector3.Lerp(position, _position_of_sound, Mathf.Repeat(((float)phase/6.28f)*(float)_frequency, 1.0f));
			
			if(i % 4 == 0)
			{
				Debug.DrawLine(new_position, new_position + ortho * amplitude * 2.0f, color);
			}
		}
	}


	//update the position of the sound, and figure out the direction from it to this audio listener
	private void SetSourcePosition()
	{
		_position_of_sound 				= source_object.transform.position;	
	}


	//per channel volume
	private void SetGain()
	{
		//now generate the direction of our "ears"
		//forward plus or minus right will give the 45 degree angles away from our view  (this could be adjusted per ear, if you wanted to simulate being a cat...)
		_direction_of_left_ear			= Vector3.Normalize(gameObject.transform.forward - gameObject.transform.right * stereo_seperation);
		_direction_of_right_ear			= Vector3.Normalize(gameObject.transform.forward + gameObject.transform.right * stereo_seperation);


		//adjust the master volume gain per ear by how much the ear is facing the sound source
		_gain_master					= Math.Max(gain - falloff * _distance_to_sound, 0.0);
		
		float dot_left					= Vector3.Dot(_direction_to_sound, _direction_of_left_ear);
		float dot_right					= Vector3.Dot(_direction_to_sound, _direction_of_right_ear);

		_gain_left						= Math.Max( dot_left, Math.Min(dot_left, Mathf.Abs(dot_right)));
		_gain_right						= Math.Max(dot_right, Math.Min(dot_right, Mathf.Abs(dot_left)));
	}


	//simple doppler effect pitch shifting
	private void SetDoppler()
	{
		//figure out if the sound is moving, and adjust the pitch accordingly 
		//imagine the sound as ripples shifting across a pool of water; now put the pool of water on a car and they all move together 
		//if you were in the pool, they would seem to be the same wave, but if you are an outside observer, it seems like they arrive faster and closer together
		//that adjusts the frequency of the sound (thereby raising or lowering the pitch)
		_doppler						= Lerp(_distance_to_sound_last_frame - _distance_to_sound, _doppler, 0.0125);
		_distance_to_sound_last_frame	= _distance_to_sound;
		_frequency						= Lerp(frequency, _frequency + _doppler, doppler_effect);
	}


	//the sound frequency interval
	private void SetTheta()
	{
		//get the distance and direction to the sound
		_direction_to_sound				= Vector3.Normalize(_position_of_sound - gameObject.transform.position);
		_distance_to_sound				= Vector3.Distance(_position_of_sound, gameObject.transform.position);

		//theta is the "angle" for this frequency of sound
		//as we go along (around) the sound wave we update in increments of theta
		_theta 							= (_frequency * 2 * Math.PI) / _sample_rate;
		_theta							= Lerp(_theta, _theta_last_frame, 0.05);
		_theta_last_frame				= _theta;
	}


	private void GenerateSineWave(float[] buffer, int channels)
	{
		if(_buffer_length == 0) _buffer_length = buffer.Length;
		
		double amplitude 	= 0;
		for (int i = 0; i < buffer.Length; i = i + channels)
		{
			//generate the wave amplitude at this position
			amplitude	= Math.Sin(_phase);

			// if we have stereo, generate the audio and adjust the gain per ear
			if (channels == 2)
			{
				buffer[i]		= (float)(_gain_master *  _gain_left * .5 * amplitude);
				buffer[i + 1]	= (float)(_gain_master * _gain_right * .5 * amplitude);
			}
			else
			{
				buffer[i]		= (float)(_gain_master * amplitude);
			}


			//move along the wave
			_phase		= _phase + _theta;


			//the phase loops around the sine wave, which is 2 PI for a total rotation
			if (_phase * 0.5 > Math.PI)
			{
				_phase = 0.0;
			}
		}
	}


	//linear interpolation (for smoothing between frames)
	private double Lerp(double x, double y, double a)
	{
		return x * (1.0 - a) + y * a;
	}
}

