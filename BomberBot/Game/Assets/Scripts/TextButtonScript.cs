/* Augustin Gardette */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class TextButtonScript : MonoBehaviour {
	
	public bool _isTextButton = true;
	public Color _normalColor;
	public Color _hoverColor;
	private TextMesh _textMesh;

	public GameSettingSingleton.MenuState _actionButton;
	
	void Start()
	{
		_hoverColor.a = 1f;
		_normalColor.a = 1f;

		if(_isTextButton)
		{
			_textMesh = this.GetComponent<TextMesh>();
			_textMesh.color = _normalColor;

		}else
		{
			//this.renderer.materials[0] = null;
		}	
	}



	void OnMouseEnter()
	{
		if(_isTextButton)
		{
			_textMesh.color = _hoverColor;
		}else
		{
			this.GetComponentsInChildren<MeshRenderer>()[0].renderer.materials[0].color = _hoverColor;
			this.GetComponentsInChildren<MeshRenderer>()[1].renderer.materials[0].color = _hoverColor;
		}

	}
	
	void OnMouseExit()
	{
		if(_isTextButton)
		{
			_textMesh.color = _normalColor;
			
		}else
		{
			this.GetComponentsInChildren<MeshRenderer>()[0].renderer.materials[0].color = _normalColor;
			this.GetComponentsInChildren<MeshRenderer>()[1].renderer.materials[0].color = _normalColor;
		}	

	}
	
	void OnMouseUp()
	{
		GameSettingSingleton.Instance.CurrentMenuState = _actionButton;
		GameSettingSingleton.Instance.MenuStateHasChanged  = true;
	}
		
}
