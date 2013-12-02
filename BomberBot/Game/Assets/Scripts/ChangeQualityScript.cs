using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class ChangeQualityScript : MonoBehaviour {

	
	public enum Action {increase=1,decrease=2};
	public TextMesh _qualityField;
	public Action _action;
	
	private TextMesh _textMesh;
	
	// Use this for initialization
	void Start()
	{
		_textMesh = this.GetComponent<TextMesh>();
		
		if(_action == Action.decrease)
		{
			_qualityField.text = " Players";
		}
	}
	
	void OnMouseUp()
	{
		if(_action == Action.decrease)
		{
			GameSettingSingleton.Instance.MaxPlayerNumber--;
			GameSettingSingleton.Instance.MaxPlayerNumber = (GameSettingSingleton.Instance.MaxPlayerNumber<0)?0:GameSettingSingleton.Instance.MaxPlayerNumber;
			_qualityField.text = GameSettingSingleton.Instance.MaxPlayerNumber+" Players";
		}
		else
		{
			if(_action == Action.increase)
			{
				GameSettingSingleton.Instance.MaxPlayerNumber++;
				GameSettingSingleton.Instance.MaxPlayerNumber = (GameSettingSingleton.Instance.MaxPlayerNumber>32)?32:GameSettingSingleton.Instance.MaxPlayerNumber;
				_qualityField.text = ""+GameSettingSingleton.Instance.MaxPlayerNumber+" Players";
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
