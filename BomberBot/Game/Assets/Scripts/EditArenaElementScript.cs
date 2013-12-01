using UnityEngine;
using System.Collections;

public class EditArenaElementScript : MonoBehaviour {


	private int _indexElement;

	public int IndexElement {
		get {
			return _indexElement;
		}
		set {
			_indexElement = value;
		}
	}

	private int _valueElement;

	public int ValueElement {
		get {
			return _valueElement;
		}
		set {
			_valueElement = value;
		}
	}

	private bool _isUpdated = true;

	public bool IsUpdated {
		get {
			return _isUpdated;
		}
		set {
			_isUpdated = value;
		}
	}

	void OnMouseUp()
	{
		_valueElement ++;
		if(_valueElement>7)
		{
			_valueElement = 0;
		}
		Debug.Log(_valueElement+" "+GameSettingSingleton.Instance.CurrentLoadedArena[_indexElement]);
	
		GameSettingSingleton.Instance.CurrentLoadedArena[_indexElement] = byte.Parse(""+_valueElement);
		Debug.Log(_valueElement+" "+GameSettingSingleton.Instance.CurrentLoadedArena[_indexElement]);

		_isUpdated = false;
		GameSettingSingleton.Instance.UpdateArenaViewer = true;

	}
}
