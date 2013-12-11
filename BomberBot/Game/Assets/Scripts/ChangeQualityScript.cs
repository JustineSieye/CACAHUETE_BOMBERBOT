/* Augustin Gardette */

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
			_qualityField.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
		}
	}
	
	void OnMouseUp()
	{
		if(_action == Action.decrease)
		{
			QualitySettings.DecreaseLevel();
			_qualityField.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
		}
		else
		{
			if(_action == Action.increase)
			{
				QualitySettings.IncreaseLevel();
				_qualityField.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
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
