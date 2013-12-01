using UnityEngine;
using System.Collections;

public class BomberBotAnimationScript : MonoBehaviour {
	
	
	public bool isMoving =  false;
	
	private Vector3 _tmpPosition;
	private Transform _transform;
	private Animation _animation;
	public GameObject childBomberbot;
	public float _animationSpeed = 1f;
	
	
	public float AnimationSpeed {
		get {
			return this._animationSpeed;
		}
		set {
			_animationSpeed = value;
		}
	}	
	
	// Use this for initialization
	void Start () {
		_transform = this.transform;
		if(this.animation != null)
			_animation = this.animation;
		else
			_animation = childBomberbot.animation;
		_tmpPosition = _transform.position;
		_animation.wrapMode = WrapMode.Once;
	}
	
	// Update is called once per frame
	void Update () {
		_animation["Walk"].speed = 1f;
		if(_tmpPosition.z != _transform.position.z || _tmpPosition.x != _transform.position.x)
		{
			_animation.CrossFade("Walk");
			_tmpPosition = _transform.position;
			isMoving = true;
		}else{
			_animation["Walk"].speed = 0f;
			isMoving =  false;
		}
		
		
	}
}
