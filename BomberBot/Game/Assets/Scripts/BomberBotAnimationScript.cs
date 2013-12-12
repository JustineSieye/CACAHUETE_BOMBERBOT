/* Gardette Augustin */

using UnityEngine;
using System.Collections;

public class BomberBotAnimationScript : MonoBehaviour
{
	
	public GameObject _childBomberbot;

	public TextMesh _playerNameTextMesh;
	public float _animationSpeed = 3.6f;
	public float _curanimationSpeed = 3.6f;

	private Vector3 _tmpPosition;
	private Transform _transform;
	private Animation _animation;
	private bool _shouldBeDead = false;

	//player stats
	private int _maxBombeAvailable = 2;

	public bool ShouldBeDead {
		get {
			return _shouldBeDead;
		}
		set {
			_shouldBeDead = value;
		}
	}

	public void SetPlayerName(string name)
	{
		_playerNameTextMesh.text = name;
	}


	public void SetPlayerNameColor(int colorIndex)
	{
		_playerNameTextMesh.color = GameSettingSingleton.Instance.TeamColor[colorIndex];
		foreach(var part in this.transform.GetComponentsInChildren<Renderer>())
		{
			foreach(var mat in part.materials)
			{
				if(mat.name == "mat body 2 (Instance)")
				{
					mat.color = GameSettingSingleton.Instance.TeamColor[colorIndex];
				}
			}

		}
		
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
		this.
		_tmpPosition = _transform.position;
		_animation.wrapMode = WrapMode.Once;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Mathf.Abs(_tmpPosition.z - _transform.position.z)>0.01f || Mathf.Abs(_tmpPosition.x - _transform.position.x)>0.01f)
		{
			if(_animation["Walk"].speed != _animationSpeed)
			{
				_animation["Walk"].speed = _animationSpeed;
			}
			_animation.CrossFade("Walk");
			_tmpPosition = _transform.position;
		}
		else
		{
			_animation["Walk"].speed = 0f;

		}
		
	}

	void OnCollisionEnter(Collision col)
	{
		if(Network.isServer)
		{
			if(col.transform.tag.Contains("Blast"))
			{
				_shouldBeDead = true;
			}
		}
	}



}
