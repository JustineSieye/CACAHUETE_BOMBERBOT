using UnityEngine;
using System.Collections;

public class DestroyBombScript : MonoBehaviour {

	public float _timeToLive = 2.5f;
	public GameObject _explosionBlast;
	public GameObject _implosionBlast;
	private GameObject _bomb;
	private bool _moveRight;
	private bool _moveTop;
	private bool _moveLeft;
	private bool _moveBottom;
	private NetworkView _myNetView;

	// Use this for initialization
	void Start () 
	{
		_moveRight = false;
		_moveTop = false;
		_moveLeft = false;
		_moveBottom = false;
		_bomb = this.gameObject;
		_myNetView = this.GetComponent<NetworkView>();
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		if (Network.isServer) 
		{
			_timeToLive -= Time.deltaTime;

			if (_timeToLive <= 0.0f) 
			{
				_myNetView.RPC ("DestroyBomb", RPCMode.All);
			}
		}

		//Move bomb if she collids with an implosion

		if(_moveTop)
		{
			_bomb.transform.position = new Vector3 (_bomb.transform.position.x, _bomb.transform.position.y, _bomb.transform.position.z + 1);
			_moveTop = false;
			
		}
		else
		{
			if(_moveBottom)
			{
				_bomb.transform.position = new Vector3 (_bomb.transform.position.x, _bomb.transform.position.y, _bomb.transform.position.z - 1);
				_moveBottom = false;
			}
			else
			{
				if(_moveLeft)
				{
					_bomb.transform.position = new Vector3 (_bomb.transform.position.x - 1, _bomb.transform.position.y, _bomb.transform.position.z);
					_moveLeft = false;
				}
				else
				{
					if(_moveRight)
					{
						_bomb.transform.position = new Vector3 (_bomb.transform.position.x + 1, _bomb.transform.position.y, _bomb.transform.position.z);
						_moveRight = false;
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (Network.isServer) 
		{
			Debug.Log("something collids with this bomb");
			if(col.gameObject.tag.Contains("RepulsiveBlast"))
			{
				_timeToLive = 0.0f;
			}
			else
			{
				if(col.gameObject.tag.Contains("AttractiveBlast"))
				{
					_myNetView.RPC ("MoveBomb", RPCMode.All,col.gameObject.tag);
				}
			}
		}
	}

	[RPC]
	void DestroyBomb()
	{
		GameObject topBlast,bottomBlast,leftBlast,rightBlast;

		// if this object is an repulsive bomb
		if(this.gameObject.CompareTag("RepulsiveBomb"))
		{

			Instantiate (_explosionBlast, this.transform.position, Quaternion.identity);

			topBlast = (GameObject)Instantiate (_explosionBlast, this.transform.position + Vector3.forward, Quaternion.identity);
			topBlast.tag = "TopRepulsiveBlast";

			bottomBlast = (GameObject)Instantiate (_explosionBlast, this.transform.position + Vector3.back, Quaternion.identity);
			bottomBlast.tag = "BottomRepulsiveBlast";

			leftBlast = (GameObject)Instantiate (_explosionBlast, this.transform.position + Vector3.left, Quaternion.identity);
			leftBlast.tag = "LeftRepulsiveBlast";

			rightBlast = (GameObject)Instantiate (_explosionBlast, this.transform.position + Vector3.right, Quaternion.identity);
			rightBlast.tag = "RightRepulsiveBlast";
		}
		else
		{
			// if this object is an attractive bomb
			if(this.gameObject.CompareTag("AttractiveBomb"))
			   {
				
				topBlast = (GameObject)Instantiate (_implosionBlast, this.transform.position + Vector3.forward, Quaternion.identity);
				topBlast.tag = "TopAttractiveBlast";
				
				bottomBlast = (GameObject)Instantiate (_implosionBlast, this.transform.position + Vector3.back, Quaternion.identity);
				bottomBlast.tag = "BottomAttractiveBlast";
				
				leftBlast = (GameObject)Instantiate (_implosionBlast, this.transform.position + Vector3.left, Quaternion.identity);
				leftBlast.tag = "LeftAttractiveBlast";
				
				rightBlast = (GameObject)Instantiate (_implosionBlast, this.transform.position + Vector3.right, Quaternion.identity);
				rightBlast.tag = "RightAttractiveBlast";
			}
		}

		Destroy (_bomb);

	}

	[RPC]
	void MoveBomb(string colTag)
	{
		

		if( colTag == "LeftAttractiveBlast")
		{
			_moveRight = true;
			Debug.Log("right move bomb");
		}
		else
		{
			if(colTag == "RightAttractiveBlast")
			{
				_moveLeft = true;
				Debug.Log("left move bomb");
			}
			else
			{
				if(colTag == "BottomAttractiveBlast")
				{
					_moveTop = true;
					Debug.Log("top move bomb");
				}
				else
				{
					if(colTag == "TopAttractiveBlast")
					{
						_moveBottom = true;
						Debug.Log("bottom move bomb");
					}
				}
			}
		}
	}

}
