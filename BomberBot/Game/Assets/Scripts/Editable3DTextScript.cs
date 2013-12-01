using UnityEngine;
using System.Collections;

public class Editable3DTextScript : MonoBehaviour {

	private bool _isEditing;
	public int _cursorIndex;
	private TextMesh _textMesh ;
	public string _textContent;

	public string TextContent {
		get {
			return _textContent;
		}
		set {
			_textContent = value;
		}
	}


	public string _diplayedText;
	public string _cursor = "|";
	public string _strInput;
	public float _blinkCursorTime = 1.0f;
	// Use this for initialization
	void Start () {
		_isEditing = false;
		_cursorIndex = 0;
		_textMesh = this.GetComponent<TextMesh>();
		_textContent = _textMesh.text;
	}
	
	// Update is called once per frame
	void Update () {

		if(_isEditing)
		{
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{

				_cursorIndex--;
				_cursorIndex = (_cursorIndex<0)?0:_cursorIndex;
				Debug.Log("leftArrow " + _cursorIndex+" "+_textContent.Length);
			}
			else
			{
				if(Input.GetKeyDown(KeyCode.RightArrow))
				{

					_cursorIndex++;
					_cursorIndex = (_cursorIndex>_textContent.Length)?_textContent.Length:_cursorIndex;
					Debug.Log("rightArrow " + _cursorIndex+" "+_textContent.Length);

				}
				else
				{
					if(Input.GetKeyDown(KeyCode.Backspace))
					{
						Debug.Log("Backspace " + _cursorIndex+" "+_textContent.Length);
						_cursorIndex--;
						if(_cursorIndex<0)
							_cursorIndex = 0;
						else
						{

							_textContent = _textContent.Remove(_cursorIndex,1);
						}


						Debug.Log("Backspace " + _cursorIndex+" "+_textContent.Length);
					}
					else
					{
						if(Input.GetKeyDown(KeyCode.Delete))
						{
							if(_cursorIndex>0)
							{
								Debug.Log("Delete " + _cursorIndex+" "+_textContent.Length);
								_textContent = _textContent.Remove(_cursorIndex,1);
							}

						}
						else
						{
							_strInput = Input.inputString;
							_textContent = _textContent.Insert(_cursorIndex,_strInput);
							_cursorIndex += _strInput.Length;
						}
					}
				}
			}

			if(_blinkCursorTime > 0.0f)
			{

				_blinkCursorTime -= Time.deltaTime;


			}
			else
			{
				_blinkCursorTime = 1.0f;
				_cursor = (_cursor == " ")?"|":" ";
			}


			_diplayedText = _textContent.Insert(_cursorIndex,_cursor);
			_textMesh.text = _diplayedText;
		}else
			_textMesh.text = _textContent;

	}

	void OnMouseUp()
	{
		_isEditing = !_isEditing;
		_cursorIndex = 0;

	}




}
