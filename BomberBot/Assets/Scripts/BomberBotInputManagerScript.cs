using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NetworkView))]
public class BomberBotInputManagerScript : MonoBehaviour {
	
	//player prefab
	[SerializeField]
	private GameObject _BomberbotPrefab;

	public GameObject BomberbotPrefab {
		get {
			return this._BomberbotPrefab;
		}
		set {
			_BomberbotPrefab = value;
		}
	}

 	public GameObject _repulsiveBombPrefab;
	public GameObject _attractiveBombPrefab;

	
	//player move speed
	[SerializeField]
	private float _bomberbotSpeed = 4f;
	
	public float BomberbotSpeed {
		get {
			return this._bomberbotSpeed;
		}
		set {
			_bomberbotSpeed = value;
		}
	}   
	
	//class that contains information / intentions Player
	class BomberBot
	{
		
		//*********Player Components***********//
		
		private Transform _bomberbotTransform;
		
		public Transform BomberbotTransform {
			get {
				return this._bomberbotTransform;
			}
			set {
				_bomberbotTransform = value;
			}
		}
		
		//BomberBotAnimationScript 
		private BomberBotAnimationScript _bomberbotAnimationScript;
		
		public BomberBotAnimationScript BomberbotAnimationScript {
			get {
				return this._bomberbotAnimationScript;
			}
			set {
				_bomberbotAnimationScript = value;
			}
		}		
		
		//Network player
		private NetworkPlayer _bomberbotNetPlayer;
		
		public NetworkPlayer BomberbotNetPlayer {
			get {
				return this._bomberbotNetPlayer;
			}
			set {
				_bomberbotNetPlayer = value;
			}
		}
		
		//*************************************//
		
		//*********Player intentions***********//
		
		//move
		public bool _wantToGoForward = false;
		public bool _wantToGoBackward = false;
		public bool _wantToGoLeft = false;
		public bool _wantToGoRight = false;
		
		//bomb
		public bool _wantToPutAttrictiveBomb = false;
		public bool _wantToPutRepulsiveBomb = false;
		
		//*************************************//
		
		//**********Player stats**************//
		
		public int _maxAvailableBomb = 1;
		//public byte _bonusMask = 0x00;
		public int _speedBonus = 1;
		
		//*************************************//
		
		//public NetworkView _arenaNetworkView;
		//public NetworkView _menuNetworkView;
		
	}
	
	
	
	private Dictionary<NetworkPlayer, BomberBot> _bomberBotsList;
	
	private Dictionary<NetworkPlayer, BomberBot> BomberBotsList {
		get {
			return this._bomberBotsList;
		}
		set {
			_bomberBotsList = value;
		}
	}
	
	private NetworkView _myNetworkView = null;
	
	void Start () {
		BomberBotsList = new Dictionary<NetworkPlayer, BomberBot>();
		_myNetworkView = this.gameObject.GetComponent<NetworkView>();
	}
	
	void Update () {
		if (Network.isClient)
		{
			//go forward
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				_myNetworkView.RPC("PlayerWantToGoForward", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.UpArrow))
			{
				_myNetworkView.RPC("PlayerWantToGoForward", RPCMode.Server, Network.player, false);
			}
			
			//go backward
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				_myNetworkView.RPC("PlayerWantToGoBackward", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.DownArrow))
			{
				_myNetworkView.RPC("PlayerWantToGoBackward", RPCMode.Server, Network.player, false);
			}
			
			//go left
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				_myNetworkView.RPC("PlayerWantToGoLeft", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.LeftArrow))
			{
				_myNetworkView.RPC("PlayerWantToGoLeft", RPCMode.Server, Network.player, false);
			}
			
			//go right
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				_myNetworkView.RPC("PlayerWantToGoRight", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				_myNetworkView.RPC("PlayerWantToGoRight", RPCMode.Server, Network.player, false);
			}

			//put attractive bomb
			if (Input.GetKeyDown(KeyCode.L))
			{
				_myNetworkView.RPC("PlayerWantToPutAttractiveBomb", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.L))
			{
				_myNetworkView.RPC("PlayerWantToPutAttractiveBomb", RPCMode.Server, Network.player, false);
			}
			
			//put repulsive bomb
			if (Input.GetKeyDown(KeyCode.M))
			{
				_myNetworkView.RPC("PlayerWantToPutRepulsiveBomb", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.M))
			{
				_myNetworkView.RPC("PlayerWantToPutRepulsiveBomb", RPCMode.Server, Network.player, false);
			}
		}
	}
	
	
	
	void FixedUpdate()
	{
		
		if(Network.isServer){
			if(BomberBotsList.Count > 0)
			{
				foreach (var b in BomberBotsList)
				{
					if(b.Value.BomberbotAnimationScript != null)
						b.Value.BomberbotAnimationScript.AnimationSpeed = _bomberbotSpeed * b.Value._speedBonus * 0.9f;
					
					if(b.Value._wantToGoForward) // go forward
					{
						b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,0,0));
						b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
						
					}
					else
					{
						
						if(b.Value._wantToGoBackward) // go forward
						{
							b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,180,0));
							b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
							
						}
						else
						{
							if(b.Value._wantToGoRight) //go right
							{
								b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,90,0));
								b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
							}
							else
							{
								
								if(b.Value._wantToGoLeft) //go right
								{
									b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,270,0));
									b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
								}
							}
						}
					}

					if(b.Value._wantToPutAttrictiveBomb || b.Value._wantToPutRepulsiveBomb) 
					{
						NetworkViewID _netViewBomb = Network.AllocateViewID();
						_myNetworkView.RPC("InstantiateBomb",RPCMode.All,b.Key,b.Value.BomberbotTransform.position,_netViewBomb);
					}
					
					_myNetworkView.RPC("ClientPlayerUpdate",RPCMode.Others,b.Key,b.Value.BomberbotTransform.position,b.Value.BomberbotTransform.rotation);
				}
				
			}
		}
	}
	void OnConnectedToServer()
	{
		//request for the players list
		
		_myNetworkView.RPC("SendAllPlayers", RPCMode.Server);
	}
	
	void OnPlayerConnected(NetworkPlayer p)
	{
		// TODO allow player to select his team
		if(Network.isServer)
		{
			NetworkViewID newViewID = Network.AllocateViewID();
			//Vector3 respawn = (Vector3)GameSettingSingleton.Instance.GreenHQRespawnPosition[Random.Range(0,GameSettingSingleton.Instance.GreenHQRespawnPosition.Count)]+new Vector3(0,2,0);
			Vector3 respawn = Vector3.up*2;
			
			_myNetworkView.RPC("AddPlayerConnected", RPCMode.All, p, respawn, Quaternion.identity, newViewID);
			Debug.Log("Player " + newViewID.ToString() + " connected from " + p.ipAddress + ":" + p.port);
			Debug.Log("There are now " + Network.connections.Length + " players.");
		}       
	}
	
	[RPC]
	void SendAllPlayers(NetworkMessageInfo info)
	{
		// Send all players already connected to the new connected player
		if(Network.isServer)
		{
			foreach(var b in BomberBotsList)
			{
				NetworkView playerNetworkView = b.Value.BomberbotTransform.gameObject.GetComponent<NetworkView>();
				NetworkPlayer netPlayer = b.Key;
				
				if(netPlayer.ToString() != info.sender.ToString())
				{
					_myNetworkView.RPC("AddPlayerConnected", info.sender, b.Value.BomberbotNetPlayer, b.Value.BomberbotTransform.position,b.Value.BomberbotTransform.rotation, playerNetworkView.viewID);
				}
				
			}
		}
	}
	
	[RPC]
	void AddPlayerConnected(NetworkPlayer p,Vector3 playerPosition, Quaternion playerRotation, NetworkViewID newPlayerView)
	{
		if(!BomberBotsList.ContainsKey(p))
		{
			
			BomberBotsList.Add(p, new BomberBot());
			
			
			GameObject go_bb = (GameObject)Instantiate(_BomberbotPrefab, playerPosition, playerRotation);
			go_bb.GetComponent<NetworkView>().viewID = newPlayerView;
			BomberBotsList[p].BomberbotTransform = go_bb.GetComponent<Transform>();
			go_bb.GetComponentInChildren<TextMesh>().text = newPlayerView.ToString().Remove(0,13)+"P";
			
			if(go_bb.GetComponent<BomberBotAnimationScript>())
				BomberBotsList[p].BomberbotAnimationScript =  go_bb.GetComponent<BomberBotAnimationScript>();
			
			
		}else{
			Debug.Log("Player :"+p.ToString()+" already exist!");
		}
	}
	
	void OnPlayerDisconnected(NetworkPlayer p)
	{
		Destroy(BomberBotsList[p].BomberbotTransform.gameObject);
		BomberBotsList.Remove(p);
		_myNetworkView.RPC("PlayerDisconnected", RPCMode.Others, p);
		
	}
	
	[RPC]
	void PlayerDisconnected(NetworkPlayer p)
	{
		Destroy(BomberBotsList[p].BomberbotTransform.gameObject);
		BomberBotsList.Remove(p);
		
	}
	
	#region MOVE_RPC
	
	//send the new player position/rotation to the client
	[RPC]
	void ClientPlayerUpdate(NetworkPlayer p,Vector3 playerPosition, Quaternion playerRotation)
	{
		if(BomberBotsList.ContainsKey(p))
		{
			BomberBotsList[p].BomberbotTransform.position = playerPosition;
			BomberBotsList[p].BomberbotTransform.rotation = playerRotation;
		}
	}
	
	// go forward
	[RPC]
	void PlayerWantToGoForward(NetworkPlayer p, bool b)
	{
		
		BomberBotsList[p]._wantToGoForward = b;
		if (Network.isServer)
		{
			
			_myNetworkView.RPC("PlayerWantToGoForward", RPCMode.Others, p, b);
		}
	}
	
	//go backward
	[RPC]
	void PlayerWantToGoBackward(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoBackward = b;
		if (Network.isServer)
		{
			_myNetworkView.RPC("PlayerWantToGoBackward", RPCMode.Others, p, b);
		}
	}
	
	//go right
	[RPC]
	void PlayerWantToGoRight(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoRight = b;
		if (Network.isServer)
		{
			_myNetworkView.RPC("PlayerWantToGoRight", RPCMode.Others, p, b);
		}
	}
	
	//go left
	[RPC]
	void PlayerWantToGoLeft(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoLeft = b;
		if (Network.isServer)
		{
			_myNetworkView.RPC("PlayerWantToGoLeft", RPCMode.Others, p, b);
		}
	}
	
	#endregion

	#region RPC_BOMB
	//put an attractive bomb
	[RPC]
	void PlayerWantToPutAttractiveBomb(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToPutAttrictiveBomb = b;
		if (Network.isServer)
		{
			_myNetworkView.RPC("PlayerWantToPutAttractiveBomb", RPCMode.Others, p, b);
		}
	}
	
	//put a repulsive bomb
	[RPC]
	void PlayerWantToPutRepulsiveBomb(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToPutRepulsiveBomb = b;
		if (Network.isServer)
		{

			_myNetworkView.RPC("PlayerWantToPutRepulsiveBomb", RPCMode.Others, p, b);
		}
	}

	[RPC]
	void InstantiateBomb(NetworkPlayer p, Vector3 bombPos, NetworkViewID bombViewID)
	{
		if(BomberBotsList[p]._wantToPutRepulsiveBomb)
		{
			GameObject bomb = (GameObject)Instantiate(_repulsiveBombPrefab,RoundPostion(bombPos), Quaternion.identity);
			bomb.networkView.viewID = bombViewID;
			BomberBotsList[p]._wantToPutRepulsiveBomb = false;

		}
		else
		{
			if(BomberBotsList[p]._wantToPutAttrictiveBomb)
			{
				GameObject bomb = (GameObject)Instantiate (_attractiveBombPrefab,RoundPostion(bombPos), Quaternion.identity);
				bomb.networkView.viewID = bombViewID;
				BomberBotsList[p]._wantToPutAttrictiveBomb = false;
			}
		}
	}

	#endregion

	Vector3 RoundPostion(Vector3 pos)
	{
		return new Vector3 (Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
	}

}
