/**Augustin GARDETTE **/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MoveScript : MonoBehaviour {
	
	private Rigidbody _rigibody; //
	
   	
	public float _speed = 2f; // move speed
	public float _speedAnim = 1f;
	
	public Transform _characterTransform;
	
	private Transform _transform;

	private float verticalAxisValue;
	private float horizontalAxisValue;
	
	private bool _goForward = false;
	private bool _goBackward = false;
	private bool _goRight = false;
	private bool _goLeft = false;
	
	// Use this for initialization
	void Start () {
		
		_transform = this.transform;
			// Make the rigidbody not change rotation
		this.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
		if(_characterTransform == null)
			_characterTransform = _transform;
		
	}
	
	void FixedUpdate(){
		this.animation["Walk"].speed = _speedAnim;
//		if(tim<=1f){
//			Debug.Log(tim);
//			_transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
//		    _transform.position += _characterTransform.forward * _speed * Time.deltaTime;
//			if(!this.animation.isPlaying)
//				this.animation.Play("Walk");
//			tim+=Time.fixedDeltaTime;
//		}
		
		//move
		
		if(_goForward) // go forward
		{
			_transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
		    _transform.position += _characterTransform.forward * _speed * Time.deltaTime;
			this.animation.CrossFade("Walk");
			
		}else{
		
			if(_goBackward) // go forward
			{
				_transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
			    _transform.position += _characterTransform.forward * _speed * Time.deltaTime;
				this.animation.CrossFade("Walk");
			}
			
			else{
				if(_goRight) //go right
				{
					_transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
			    	_transform.position += _characterTransform.forward * _speed * Time.deltaTime;
					this.animation.CrossFade("Walk");

				}else{
		
					if(_goLeft) //go right
					{
						_transform.rotation = Quaternion.Euler(new Vector3(0,270,0));
						_transform.position += _characterTransform.forward * _speed * Time.deltaTime;
						this.animation.CrossFade("Walk");
					}else{
						this.animation["Walk"].speed = 0f;
					}
				}
			}
			
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		

	


			verticalAxisValue = Input.GetAxisRaw("Vertical");
			horizontalAxisValue = Input.GetAxisRaw("Horizontal");

			//move
			_goForward = (verticalAxisValue>0f?true:false);
			_goBackward = (verticalAxisValue<0f?true:false);
			_goRight = (horizontalAxisValue>0f?true:false);
			_goLeft = (horizontalAxisValue<0f?true:false);
						
	
			
		}
		


	
		
	
}
