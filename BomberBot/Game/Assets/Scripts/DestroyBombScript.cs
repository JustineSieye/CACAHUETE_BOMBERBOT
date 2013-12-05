using UnityEngine;
using System.Collections;

public class DestroyBombScript : MonoBehaviour {

	public float _timeToLive = 2.5f;
	public int _blastPower = 1;
	private string _colliderTag;

	public GameObject _explosionBlast;
	public GameObject _implosionBlast;
	private GameObject _bomb;
	private GameObject _owner;

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
	void FixedUpdate () 
	{ 
		if (Network.isServer) 
		{
			_timeToLive -= Time.deltaTime;

			if (_timeToLive <= 0.0f) 
			{
				_myNetView.RPC ("RPC_DestroyBomb", RPCMode.All);
			}
			else
			{
		

		//Move bomb if she collids with an implosion

				if(_moveTop)
				{
					_bomb.transform.position = new Vector3 (_bomb.transform.position.x, _bomb.transform.position.y, _bomb.transform.position.z + 1);
					_myNetView.RPC("RPC_SendNewBombPositionToClient",RPCMode.Others,_bomb.transform.position);
					_moveTop = false;
				}
				else
				{
					if(_moveBottom)
					{
						_bomb.transform.position = new Vector3 (_bomb.transform.position.x, _bomb.transform.position.y, _bomb.transform.position.z - 1);
						_myNetView.RPC("RPC_SendNewBombPositionToClient",RPCMode.Others,_bomb.transform.position);
						_moveBottom = false;
					}
					else
					{
						if(_moveLeft)
						{
							_bomb.transform.position = new Vector3 (_bomb.transform.position.x - 1, _bomb.transform.position.y, _bomb.transform.position.z);
							_myNetView.RPC("RPC_SendNewBombPositionToClient",RPCMode.Others,_bomb.transform.position);
							_moveLeft = false;
						}
						else
						{
							if(_moveRight)
							{
								_bomb.transform.position = new Vector3 (_bomb.transform.position.x + 1, _bomb.transform.position.y, _bomb.transform.position.z);
								_myNetView.RPC("RPC_SendNewBombPositionToClient",RPCMode.Others,_bomb.transform.position);
								_moveRight = false;
							}
						}
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (Network.isServer) 
		{
			_colliderTag = col.gameObject.tag;
			
			if(_colliderTag.Contains("RepulsiveBlast"))
			{
				_timeToLive = 0.0f;
			}
			else
			{
				if(_colliderTag.Contains("AttractiveBlast"))
				{
					_moveRight = (_colliderTag == "LeftAttractiveBlast");
					_moveLeft = (_colliderTag == "RightAttractiveBlast");
					_moveTop = (_colliderTag == "BottomAttractiveBlast");
					_moveBottom = (_colliderTag == "TopAttractiveBlast");
				}
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (Network.isServer) 
		{
			_colliderTag = col.gameObject.tag;

			if(_colliderTag.Contains("RepulsiveBlast"))
			{
				_timeToLive = 0.0f;
			}
			else
			{
				if(_colliderTag.Contains("AttractiveBlast"))
				{
					_moveRight = ( _colliderTag == "LeftAttractiveBlast");
					_moveLeft = (_colliderTag == "RightAttractiveBlast");
					_moveTop = (_colliderTag == "BottomAttractiveBlast");
					_moveBottom = (_colliderTag == "TopAttractiveBlast");
				}
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(Network.isServer)
		{
			if(col.collider.tag == "Player")
			{
				this.collider.isTrigger = false;
				_myNetView.RPC("RPC_OnPlayerExitBombPosition",RPCMode.Others);
			}
		}
	}

	[RPC]
	void RPC_DestroyBomb()
	{
		GameObject topBlast,bottomBlast,leftBlast,rightBlast;

		// if this object is an repulsive bomb
		if(this.gameObject.CompareTag("RepulsiveBomb"))
		{

			Instantiate (_explosionBlast, this.transform.position, Quaternion.identity);

			for(int i = 1 ; i<=_blastPower;i++)
			{
				topBlast = (GameObject)Instantiate (_explosionBlast, this.transform.position + Vector3.forward*i, Quaternion.identity);
				topBlast.tag = "TopRepulsiveBlast";

				bottomBlast = (GameObject)Instantiate (_explosionBlast, this.transform.position + Vector3.back*i, Quaternion.identity);
				bottomBlast.tag = "BottomRepulsiveBlast";

				leftBlast = (GameObject)Instantiate (_explosionBlast, this.transform.position + Vector3.left*i, Quaternion.identity);
				leftBlast.tag = "LeftRepulsiveBlast";

				rightBlast = (GameObject)Instantiate (_explosionBlast, this.transform.position + Vector3.right*i, Quaternion.identity);
				rightBlast.tag = "RightRepulsiveBlast";
			}
		}
		else
		{
			// if this object is an attractive bomb
			if(this.gameObject.CompareTag("AttractiveBomb"))
			   {
				for(int i = _blastPower; i>0;i--)
				{
					topBlast = (GameObject)Instantiate (_implosionBlast, this.transform.position + Vector3.forward*i, Quaternion.identity);
					topBlast.tag = "TopAttractiveBlast";
					
					bottomBlast = (GameObject)Instantiate (_implosionBlast, this.transform.position + Vector3.back*i, Quaternion.identity);
					bottomBlast.tag = "BottomAttractiveBlast";
					
					leftBlast = (GameObject)Instantiate (_implosionBlast, this.transform.position + Vector3.left*i, Quaternion.identity);
					leftBlast.tag = "LeftAttractiveBlast";
					
					rightBlast = (GameObject)Instantiate (_implosionBlast, this.transform.position + Vector3.right*i, Quaternion.identity);
					rightBlast.tag = "RightAttractiveBlast";
				}
			}
		}

		Destroy(_bomb);

	}

	[RPC]
	void RPC_SendNewBombPositionToClient(Vector3 newPos)
	{
		this.transform.position = newPos;

	}

	[RPC]
	void RPC_OnPlayerExitBombPosition()
	{
		this.collider.isTrigger = false;
	}


	void SetBombBlastPower(int power)
	{
		_blastPower = power;
	}

	void SetBombOwner(GameObject obj)
	{
		_owner = obj;

	}

}
