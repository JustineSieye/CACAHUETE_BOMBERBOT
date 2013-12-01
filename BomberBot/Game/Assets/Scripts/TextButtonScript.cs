using UnityEngine;
using System.Collections;

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
			//this.renderer.materials[0] = null;
		}

	}
	
	void OnMouseExit()
	{
		if(_isTextButton)
		{
			_textMesh.color = _normalColor;
			
		}else
		{
			//this.renderer.materials[0] = null;
		}	

	}
	
	void OnMouseUp()
	{
		Debug.Log(_actionButton);
		GameSettingSingleton.Instance.CurrentMenuState = _actionButton;
	
	}
		
}
