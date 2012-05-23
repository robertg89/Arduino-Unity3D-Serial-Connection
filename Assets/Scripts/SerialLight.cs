using UnityEngine;
using System.Collections;
using System.IO.Ports;//.Ports;

public class SerialLight : MonoBehaviour {

	
	SerialPort stream = new SerialPort("COM3", 9600); //Set the port (com4) and the baud rate (9600, is standard on most devices)
	float[] lastRot = {0,0,0}; //Need the last rotation to tell how far to spin the camera
	
	public float maxValue = 700;
	public float minValue = 200;
	
	public Transform cameraContainer;
	
	float currentValue = 0.0f;
	GameObject pointLight;

	void Start () {
		pointLight = GameObject.Find("Point light").gameObject;
		stream.Open(); //Open the Serial Stream.
	}

	// Update is called once per frame
	void Update () {
		if (stream.IsOpen){
			string value = stream.ReadLine(); //Read the information
			//Debug.Log("value "+value);
			if (value.Length > 0){
				currentValue = 	float.Parse(value);
				float intensity = ((currentValue-minValue) / (maxValue - minValue));
				print(intensity);
				pointLight.light.intensity = Mathf.Max(0, 3 * intensity);		
			}
		}
		
//		cameraContainer.Rotate(Vector3(0,1,0));
		cameraContainer.Rotate(new Vector3 (0f, 1.5f, 0f) * Time.deltaTime, Space.World);
		
//		string[] vec3 = value.Split(','); //My arduino script returns a 3 part value (IE: 12,30,18)
//		if(vec3[0] != "" && vec3[1] != "" && vec3[2] != "") //Check if all values are recieved
//		{
//			transform.Rotate(			//Rotate the camera based on the new values
//								float.Parse(vec3[0])-lastRot[0],
//								float.Parse(vec3[1])-lastRot[1],
//								float.Parse(vec3[2])-lastRot[2],
//								Space.Self
//							);
//			lastRot[0] = float.Parse(vec3[0]);  //Set new values to last time values for the next loop
//			lastRot[1] = float.Parse(vec3[1]);
//			lastRot[2] = float.Parse(vec3[2]);
//			stream.BaseStream.Flush(); //Clear the serial information so we assure we get new information.
//		}
	}

	void OnGUI()
	{
//		string newString = "Connected: " + transform.rotation.x + ", " + transform.rotation.y + ", " + transform.rotation.z;
		string newString = "Connected: " + currentValue;
		GUI.Label(new Rect(10,10,300,100), newString); //Display new values
		// Though, it seems that it outputs the value in percentage O-o I don't know why.
	}
}
 