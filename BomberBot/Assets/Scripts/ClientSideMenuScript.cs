﻿using UnityEngine;
using System.Collections;

public class ClientSideMenuScript : MonoBehaviour {

	public Editable3DTextScript _port;
	public Editable3DTextScript _ip;
	public Editable3DTextScript _playerName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.joinServer)
		{
			GameSettingSingleton.Instance.PortToUse = int.Parse(_port.TextContent);
			GameSettingSingleton.Instance.IpToConnect = _ip.TextContent;
			GameSettingSingleton.Instance.PlayerName = _playerName.TextContent;
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
