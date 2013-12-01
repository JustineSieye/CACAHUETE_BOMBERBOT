﻿using UnityEngine;
using System.Collections;

public class ChoseMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.clientMenu)
		{
			GameSettingSingleton.Instance.IsServer = false;
			Application.LoadLevel("ClientMenu");
		}
		else{
			if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.serverMenu)
			{
				GameSettingSingleton.Instance.IsServer = true;
				Application.LoadLevel("ServerMenu");
			}
		}

	}
}