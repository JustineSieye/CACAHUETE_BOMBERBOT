using UnityEngine;
using System.Collections;

public class BomberBotAnimationScript : MonoBehaviour
{
	
	public GameObject _childBomberbot;
	public TextMesh _playerNameTextMesh;
	public float _animationSpeed = 2f;

	private Vector3 _tmpPosition;
	private Transform _transform;
	private Animation _animation;
	 
	public void SetPlayerName(string name)
	{
		_playerNameTextMesh.text = name;
	}


	public void SetPlayerNameColor(int colorIndex)
	{
		_playerNameTextMesh.color = GameSettingSingleton.Instance.TeamColor[colorIndex];
		
	}
	public float AnimationSpeed
	{
		get {
			return this._animationSpeed;
		}
		set {
			_animationSpeed = value;
		}
	}	
	
	// Use this for initialization
	void Start ()
	{
		_transform = this.transform;

		if(this.animation != null)
		{
			_animation = this.animation;
		}
		else
		{
			_animation = _childBomberbot.animation;
		}

		_tmpPosition = _transform.position;
		_animation.wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update ()
	{

		_animation["Walk"].speed = 2f;
		if(Mathf.Abs(_tmpPosition.z - _transform.position.z)>0.1f || Mathf.Abs(_tmpPosition.x - _transform.position.x)>0.1f)
		{
			_animation.CrossFade("Walk");
			_tmpPosition = _transform.position;
		}
		else
		{
			_animation["Walk"].speed = 0f;
		}
		
		
	}
}
