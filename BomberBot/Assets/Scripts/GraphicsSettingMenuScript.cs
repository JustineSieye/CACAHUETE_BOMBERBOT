using UnityEngine;
using System.Collections;

public class GraphicsSettingMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		string[] _quality = QualitySettings.names;
		Resolution[] _resolutions = Screen.GetResolution;
		

		Screen.SetResolution(800,600,false);
		Resolution defaultResolution = new Resolution();
		Debug.Log(GameSettingSingleton.Instance.CurrentMenuState );

	}
	
	// Update is called once per frame
	void Update () {
	 
		
		
	}
	
	void OnGUI()
	{
		
			
	}
	
	
}
