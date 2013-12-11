/* Augustin Gardette */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class FullScreenScript : MonoBehaviour {
	
	
	private TextMesh _textMesh;
	
	// Use this for initialization
	void Start()
	{
		_textMesh = this.GetComponent<TextMesh>();
		_textMesh.text = ((Screen.fullScreen)?"Windowed":"FullScreen");
	}
	
	void OnMouseUp()
	{
		Screen.fullScreen = !Screen.fullScreen;
		_textMesh.text = ((Screen.fullScreen)?"Windowed":"FullScreen");

	}
	
	void OnMouseEnter()
	{
		_textMesh.color = Color.gray;
	}
	
	void OnMouseExit()
	{
		_textMesh.color = Color.white;
	}

	void Update()
	{
		if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.mainMenu)
		{
			Application.LoadLevel("MainMenu");
		}
	}
	
}
