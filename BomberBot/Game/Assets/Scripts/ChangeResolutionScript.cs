using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class ChangeResolutionScript : MonoBehaviour {

	
	public enum Action {increase=1,decrease=2};
	public TextMesh _ResolutionField;
	public Action _action;

	private Resolution[] _resolutions = Screen.GetResolution;
	private Resolution _currentRes;
	private TextMesh _textMesh;
	private int _currentResolutionIndex;
	// Use this for initialization
	void Start()
	{
		_textMesh = this.GetComponent<TextMesh>();
		_currentResolutionIndex = FindCurrentResolutionIndex();
		if(_action == Action.decrease)
		{
			_currentResolutionIndex = FindCurrentResolutionIndex();
			_currentResolutionIndex = FindCurrentResolutionIndex();

			_ResolutionField.text = _currentRes.width+" x "+_currentRes.height;
		}

	}
	
	void OnMouseUp()
	{
		if(_action == Action.decrease)
		{
			_currentResolutionIndex = (_currentResolutionIndex==0)?_currentResolutionIndex:_currentResolutionIndex-1;

			_currentRes = _resolutions[_currentResolutionIndex];
			Screen.SetResolution(_currentRes.width,_currentRes.height,Screen.fullScreen);
			_ResolutionField.text = _currentRes.width+" x "+_currentRes.height;
		}
		else
		{
			if(_action == Action.increase)
			{
				_currentResolutionIndex = (_currentResolutionIndex==_resolutions.Length-1)?_currentResolutionIndex:_currentResolutionIndex+1;
				_currentRes = _resolutions[_currentResolutionIndex];
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

	int FindCurrentResolutionIndex()
	{
		int index = 0;
		bool _findCurrentResolutionIndex = false;
		
		while(!_findCurrentResolutionIndex)
		{
			if(_resolutions[index].width == _currentRes.width && _resolutions[index].height == _currentRes.height)
			{
				_findCurrentResolutionIndex = true;

			}
			else
			{
				index ++;
			}

		}

		return index;
	}



}
