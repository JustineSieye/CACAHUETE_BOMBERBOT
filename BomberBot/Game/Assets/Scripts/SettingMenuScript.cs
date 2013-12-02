using UnityEngine;
using System.Collections;

public class SettingMenuScript : MonoBehaviour {

	private bool _inSettingMenu = false;
	// Use this for initialization
	void Start () {

		foreach( var child in this.GetComponentsInChildren<Transform>())
		{
			if(child.renderer)
			{
				child.renderer.enabled = false;

			}
			if(child.collider)
			{
				child.collider.enabled = false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			_inSettingMenu = !_inSettingMenu;
			foreach( var child in this.GetComponentsInChildren<Transform>())
			{
				if(child.renderer)
				{
					child.renderer.enabled = _inSettingMenu;
				}
				if(child.collider)
				{
					child.collider.enabled = _inSettingMenu;
				}
			}
		}
		
	}

	void DisplaySettingsMenu()
	{

	}
}
