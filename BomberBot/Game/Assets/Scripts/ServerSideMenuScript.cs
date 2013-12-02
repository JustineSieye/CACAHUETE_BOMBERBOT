using UnityEngine;
using System.Collections;

public class ServerSideMenuScript : MonoBehaviour {

	public Editable3DTextScript _port;
	// Use this for initialization
	void Awake () {

		_port.TextContent = ""+GameSettingSingleton.Instance.PortToUse;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.startServer)
		{
			GameSettingSingleton.Instance.PortToUse = int.Parse(_port.TextContent);

			Application.LoadLevel("Game");
		}
		else
		{
			if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.mainMenu)
			{
				Application.LoadLevel("MainMenu");
			}

		}
	
	}


}
