using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class TeamSelectorArrowScript : MonoBehaviour 
{

	public enum Arrows {left=1,right=2};
	public GameObject _teamSelector;
	public Arrows _arrow;

	private TextMesh _textMesh;

	// Use this for initialization
	void Start()
	{
		_textMesh = this.GetComponent<TextMesh>();
		Debug.Log(GameSettingSingleton.Instance.IndexTeamSelected);
		GameSettingSingleton.Instance.IndexTeamSelected = 0;
	}

	void OnMouseUp()
	{
		if(_arrow == Arrows.left)
		{
			_teamSelector.transform.Rotate(new Vector3(0,-90,0));
			GameSettingSingleton.Instance.IndexTeamSelected--;
			GameSettingSingleton.Instance.IndexTeamSelected = (GameSettingSingleton.Instance.IndexTeamSelected<0)?3:GameSettingSingleton.Instance.IndexTeamSelected;

		}
		else
		{
			if(_arrow == Arrows.right)
			{
				_teamSelector.transform.Rotate(new Vector3(0,90,0));
				GameSettingSingleton.Instance.IndexTeamSelected++;
				GameSettingSingleton.Instance.IndexTeamSelected = (GameSettingSingleton.Instance.IndexTeamSelected>3)?0:GameSettingSingleton.Instance.IndexTeamSelected;
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
