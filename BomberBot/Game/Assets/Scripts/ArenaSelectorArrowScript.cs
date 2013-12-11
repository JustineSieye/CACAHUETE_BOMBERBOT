/* Gardette Augustin */
using UnityEngine;
using System.Collections;

public class ArenaSelectorArrowScript : MonoBehaviour {

	public enum Arrows {left=1,right=2};
	public TextMesh _arenaName;
	public Arrows _arrow;
	private int _lengthArenaList;
	private TextMesh _textMesh;
	
	// Use this for initialization
	void Start()
	{

		_arenaName.text = "Stage: "+GameSettingSingleton.Instance.ArenaFileList[GameSettingSingleton.Instance.IndexArenaSelected].name;
		_textMesh = this.GetComponent<TextMesh>();
		_lengthArenaList = GameSettingSingleton.Instance.ArenaFileList.Length;

		
	}
	
	void OnMouseUp()
	{
		if(_arrow == Arrows.left)
		{

			GameSettingSingleton.Instance.IndexArenaSelected--;
			GameSettingSingleton.Instance.IndexArenaSelected = (GameSettingSingleton.Instance.IndexArenaSelected<0)?_lengthArenaList-1:GameSettingSingleton.Instance.IndexArenaSelected;
			_arenaName.text = "Stage: "+GameSettingSingleton.Instance.ArenaFileList[GameSettingSingleton.Instance.IndexArenaSelected].name;

		}
		else
		{
			if(_arrow == Arrows.right)
			{
				GameSettingSingleton.Instance.IndexArenaSelected++;
				GameSettingSingleton.Instance.IndexArenaSelected = (GameSettingSingleton.Instance.IndexArenaSelected>_lengthArenaList-1)?0:GameSettingSingleton.Instance.IndexArenaSelected;
				_arenaName.text = "Stage: "+GameSettingSingleton.Instance.ArenaFileList[GameSettingSingleton.Instance.IndexArenaSelected].name;
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

	void Update()
	{
		if(_arenaName.text != "Stage: "+GameSettingSingleton.Instance.ArenaFileList[GameSettingSingleton.Instance.IndexArenaSelected].name)
		{

		}
	}

}
