/* Augustin Gardette */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class ChangeResolutionScript : MonoBehaviour {

	
	public enum Action {increase=1,decrease=2};
	public TextMesh _ResolutionField;
	public Action _action;

	private Resolution[] _resolutions;
	private Resolution _currentRes;
	private TextMesh _textMesh;

	// Use this for initialization
	void Start()
	{
		_resolutions = Screen.GetResolution;
		_textMesh = this.GetComponent<TextMesh>();
		GameSettingSingleton.Instance.CurrentResolutionIndex = 0;
		if(_action == Action.decrease)
		{
			_currentRes = _resolutions[GameSettingSingleton.Instance.CurrentResolutionIndex];
			_ResolutionField.text = _currentRes.width+" x "+_currentRes.height;
		}

	}
	
	void OnMouseUp()
	{
		if(_action == Action.decrease)
		{
			GameSettingSingleton.Instance.CurrentResolutionIndex = (GameSettingSingleton.Instance.CurrentResolutionIndex==0)?GameSettingSingleton.Instance.CurrentResolutionIndex:GameSettingSingleton.Instance.CurrentResolutionIndex-1;
			_currentRes = _resolutions[GameSettingSingleton.Instance.CurrentResolutionIndex];
			Screen.SetResolution(_currentRes.width,_currentRes.height,Screen.fullScreen);
			_ResolutionField.text = _currentRes.width+" x "+_currentRes.height;
		}
		else
		{
			if(_action == Action.increase)
			{
				GameSettingSingleton.Instance.CurrentResolutionIndex = (GameSettingSingleton.Instance.CurrentResolutionIndex==_resolutions.Length-1)?GameSettingSingleton.Instance.CurrentResolutionIndex:GameSettingSingleton.Instance.CurrentResolutionIndex+1;
				_currentRes = _resolutions[GameSettingSingleton.Instance.CurrentResolutionIndex];
				Screen.SetResolution(_currentRes.width,_currentRes.height,Screen.fullScreen);
				_ResolutionField.text = _currentRes.width+" x "+_currentRes.height;
			}
		}
		
	}
	
	void OnMouseEnter()
	{
		_textMesh.color = Color.gray;
	}
	
	void OnMouseExit()
	{
		_textMesh.color = Color.white;
	}

}
