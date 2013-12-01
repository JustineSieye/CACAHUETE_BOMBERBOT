using UnityEngine;
using System.Collections;

public class SettingMenuScript : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		
	switch(GameSettingSingleton.Instance.CurrentMenuState)
		{
			case(GameSettingSingleton.MenuState.clientMenu):
				if(GUI.Button(new Rect(25,Screen.height-75,100,25),"Setting"))
				{
				GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.settingMenu;
				}
			break;
			
		case(GameSettingSingleton.MenuState.settingMenu):
			DisplaySettingMenu();
			break;
		}
	}
	
	void DisplaySettingMenu()
	{
		if(GUI.RepeatButton(new Rect(100,100,100,25),"Graphics Setting"))
			Debug.Log("GraphicsSetting");
		
		if(GUI.RepeatButton(new Rect(100,150,100,25),"Inputs Setting"))
			Debug.Log("InputsSetting");
	}
}
