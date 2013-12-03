using UnityEngine;
using System.Collections;

public class FlagMovementScript : MonoBehaviour
{

	private GameObject _flag;
	private string _colliderTag;
	private bool _moveRight;
	private bool _moveTop;
	private bool _moveLeft;
	private bool _moveBottom;
	private NetworkView _myNetView;
	private Vector3 oldPos;



	// Use this for initialization
	void Start () 
	{
	 	_moveRight = false;
		_moveTop = false;
		_moveLeft = false;
		_moveBottom = false;
		_flag = this.gameObject;
		_myNetView = this.GetComponent<NetworkView>();
		oldPos = this.transform.position;

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{ 
		if(Network.isServer)
		{
			if(_moveTop)
			{
				_flag.transform.position = new Vector3 (_flag.transform.position.x, _flag.transform.position.y, _flag.transform.position.z + 1);
				UpdateFlagPositionInLoadedArenaTab(oldPos,this.transform.position);
				oldPos = this.transform.position;
				_myNetView.RPC("RPC_SendNewFlagPositionToClient",RPCMode.Others,_flag.transform.position);
				_moveTop = false;
			}
			else
			{
				if(_moveBottom)
				{
					_flag.transform.position = new Vector3 (_flag.transform.position.x, _flag.transform.position.y, _flag.transform.position.z - 1);
					UpdateFlagPositionInLoadedArenaTab(oldPos,this.transform.position);
					oldPos = this.transform.position;
					_myNetView.RPC("RPC_SendNewFlagPositionToClient",RPCMode.Others,_flag.transform.position);
					_moveBottom = false;

				}
				else
				{
					if(_moveLeft)
					{
						_flag.transform.position = new Vector3 (_flag.transform.position.x - 1, _flag.transform.position.y, _flag.transform.position.z);
						UpdateFlagPositionInLoadedArenaTab(oldPos,this.transform.position);
						oldPos = this.transform.position;
						_myNetView.RPC("RPC_SendNewFlagPositionToClient",RPCMode.Others,_flag.transform.position);
						_moveLeft = false;
					}
					else
					{
						if(_moveRight)
						{
							_flag.transform.position = new Vector3 (_flag.transform.position.x + 1, _flag.transform.position.y, _flag.transform.position.z);
							UpdateFlagPositionInLoadedArenaTab(oldPos,this.transform.position);
							oldPos = this.transform.position;
							_myNetView.RPC("RPC_SendNewFlagPositionToClient",RPCMode.Others,_flag.transform.position);
							_moveRight = false;
						}
					}
				}
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(Network.isServer)
		{
			_colliderTag = col.gameObject.tag;

			_moveRight = (_colliderTag == "RightRepulsiveBlast" || _colliderTag == "LeftAttractiveBlast");

			_moveLeft = (_colliderTag == "LeftRepulsiveBlast" || _colliderTag == "RightAttractiveBlast");

			_moveTop = (_colliderTag == "TopRepulsiveBlast" || _colliderTag == "BottomAttractiveBlast");

			_moveBottom = (_colliderTag == "BottomRepulsiveBlast" || _colliderTag == "TopAttractiveBlast");
		
		}
	}

	void OnCollisionEnter (Collision col)
	{
//		if(this.rigidbody.constraints != RigidbodyConstraints.FreezeAll)
//			this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;

		if(Network.isServer)
		{
			_colliderTag = col.gameObject.tag;
			
			_moveRight = (_colliderTag == "RightRepulsiveBlast" || _colliderTag == "LeftAttractiveBlast");
			
			_moveLeft = (_colliderTag == "LeftRepulsiveBlast" || _colliderTag == "RightAttractiveBlast");
			
			_moveTop = (_colliderTag == "TopRepulsiveBlast" || _colliderTag == "BottomAttractiveBlast");
			
			_moveBottom = (_colliderTag == "BottomRepulsiveBlast" || _colliderTag == "TopAttractiveBlast");
			
		}
	}	


	
	[RPC]

	void RPC_SendNewFlagPositionToClient(Vector3 newPos)
	{
		_flag.transform.position = newPos;
	}


	void UpdateFlagPositionInLoadedArenaTab(Vector3 prevPos,Vector3 newPos)
	{
		if(Network.isServer)
		{
			int arenaWidth = GameSettingSingleton.Instance.CurrentLoadedArena[0];
			int arenaHeight = GameSettingSingleton.Instance.CurrentLoadedArena[1];

			int prevIndex = arenaWidth*(arenaHeight-(int)prevPos.z+1)+(int)prevPos.x;
			int currentIndex = arenaWidth*(arenaHeight-(int)newPos.z+1)+(int)newPos.x;

			//Debug.Log ("[OLD] Flag move from ("+prevPos.x +","+prevPos.z +") --- bytes["+prevIndex+"] = "+GameSettingSingleton.Instance.CurrentLoadedArena[prevIndex]+" ");
			//Debug.Log ("[OLD] To ("+newPos.x +","+newPos.z +") --- bytes["+currentIndex+"] = "+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" "); 

			GameSettingSingleton.Instance.CurrentLoadedArena[prevIndex]= byte.Parse("0");
			GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("3");

			//Debug.Log ("[NEW] Flag move from ("+prevPos.x +","+prevPos.z +") --- bytes["+prevIndex+"] = "+GameSettingSingleton.Instance.CurrentLoadedArena[prevIndex]+" ");
			//Debug.Log ("[NEW] ("+newPos.x +","+newPos.z +") --- bytes["+currentIndex+"] = "+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" "); 
		}
	}
	
}
