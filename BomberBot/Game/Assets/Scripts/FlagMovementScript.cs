using UnityEngine;
using System.Collections;

public class FlagMovementScript : MonoBehaviour {

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
	void Update () 
	{ 
		if(_moveTop)
		{
			_flag.transform.position = new Vector3 (_flag.transform.position.x, _flag.transform.position.y, _flag.transform.position.z + 1);
			_moveTop = false;
			if(Network.isServer)
			{
				UpdateFlagPositionInLoadedArenaTab(oldPos,this.transform.position);
				oldPos = this.transform.position;
			}

		}
		else
		{
			if(_moveBottom)
			{
				_flag.transform.position = new Vector3 (_flag.transform.position.x, _flag.transform.position.y, _flag.transform.position.z - 1);
				_moveBottom = false;
				if(Network.isServer)
				{
					UpdateFlagPositionInLoadedArenaTab(oldPos,this.transform.position);
					oldPos = this.transform.position;
				}
			}
			else
			{
				if(_moveLeft)
				{
					_flag.transform.position = new Vector3 (_flag.transform.position.x - 1, _flag.transform.position.y, _flag.transform.position.z);
					_moveLeft = false;
					if(Network.isServer)
					{
						UpdateFlagPositionInLoadedArenaTab(oldPos,this.transform.position);
						oldPos = this.transform.position;
					}
				}
				else
				{
					if(_moveRight)
					{
						_flag.transform.position = new Vector3 (_flag.transform.position.x + 1, _flag.transform.position.y, _flag.transform.position.z);
						_moveRight = false;
						if(Network.isServer)
						{
							UpdateFlagPositionInLoadedArenaTab(oldPos,this.transform.position);
							oldPos = this.transform.position;
						}
					}
				}
			}
		}

	}

	void OnTriggerEnter (Collider col)
	{
		Debug.Log("onCollisionEnter Flag");
		if(Network.isServer)
		{
			Debug.Log("onCollisionEnter Flag");
			if(col.gameObject.tag.Contains("Blast"))
			{
				Debug.Log("blast touch flag");
				_colliderTag = col.gameObject.tag;
				_myNetView.RPC ("MoveFlag", RPCMode.All,_colliderTag);
			}
		}
	}

	void OnCollisionEnter (Collision col)
	{
		if(Network.isServer)
		{
			if(col.gameObject.tag.Contains("Blast"))
			{
				_colliderTag = col.gameObject.tag;

				_myNetView.RPC ("MoveFlag", RPCMode.All,_colliderTag);
			}
		}
	}


	
	[RPC]
	void MoveFlag(string colTag)
	{

		//Debug.Log (coll.gameObject.name);

		if(colTag == "RightRepulsiveBlast" || colTag == "LeftAttractiveBlast")
		{
			_moveRight = true;
			Debug.Log("right move flag");
		}
		else
		{
			if(colTag == "LeftRepulsiveBlast" || colTag == "RightAttractiveBlast")
			{
				_moveLeft = true;
				Debug.Log("left move flag");
			}
			else
			{
				if(colTag == "TopRepulsiveBlast" || colTag == "BottomAttractiveBlast")
				{
					_moveTop = true;
					Debug.Log("top move flag");
				}
				else
				{
					if(colTag == "BottomRepulsiveBlast" || colTag == "TopAttractiveBlast")
					{
						_moveBottom = true;
						Debug.Log("bottom move flag");
					}
				}
			}
		}




		
	}


	void UpdateFlagPositionInLoadedArenaTab(Vector3 prevPos,Vector3 newPos)
	{
		if(Network.isServer)
		{
			int arenaWidth = GameSettingSingleton.Instance.CurrentLoadedArena[0];
			int arenaHeight = GameSettingSingleton.Instance.CurrentLoadedArena[1];

			int prevIndex = arenaWidth*(arenaHeight-(int)prevPos.z+1)+(int)prevPos.x;
			int currentIndex = arenaWidth*(arenaHeight-(int)newPos.z+1)+(int)newPos.x;

			Debug.Log ("[OLD] Flag move from ("+prevPos.x +","+prevPos.z +") --- bytes["+prevIndex+"] = "+GameSettingSingleton.Instance.CurrentLoadedArena[prevIndex]+" ");
			Debug.Log ("[OLD] To ("+newPos.x +","+newPos.z +") --- bytes["+currentIndex+"] = "+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" "); 

			GameSettingSingleton.Instance.CurrentLoadedArena[prevIndex]= byte.Parse("0");
			GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("3");

			Debug.Log ("[NEW] Flag move from ("+prevPos.x +","+prevPos.z +") --- bytes["+prevIndex+"] = "+GameSettingSingleton.Instance.CurrentLoadedArena[prevIndex]+" ");
			Debug.Log ("[NEW] ("+newPos.x +","+newPos.z +") --- bytes["+currentIndex+"] = "+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" "); 
		}
	}
	
}
