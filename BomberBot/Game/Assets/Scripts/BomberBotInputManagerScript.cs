using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NetworkView))]
public class BomberBotInputManagerScript : MonoBehaviour
{
	
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

		//Player Team

		private int _playerTeamIndex;

		public int PlayerTeamIndex {
			get {
				return _playerTeamIndex;
			}
			set {
				_playerTeamIndex = value;
			}
		}


		//Player Name

		private string _playerName;

		public string PlayerName {
			get {
				return _playerName;
			}
			set {
				_playerName = value;
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

		public int _maxAvailableBomb = 2;
		public int _speedBonus = 1;
		public float _timeBeforeMoving = 2f;
		//*************************************//
		

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
			if (Input.GetKeyDown(KeyCode.Z))
			{
				_myNetworkView.RPC("RPC_PlayerWantToGoForward", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.Z))
			{
				_myNetworkView.RPC("RPC_PlayerWantToGoForward", RPCMode.Server, Network.player, false);
			}
			
			//go backward
			if (Input.GetKeyDown(KeyCode.S))
			{
				_myNetworkView.RPC("RPC_PlayerWantToGoBackward", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.S))
			{
				_myNetworkView.RPC("RPC_PlayerWantToGoBackward", RPCMode.Server, Network.player, false);
			}
			
			//go left
			if (Input.GetKeyDown(KeyCode.Q))
			{
				_myNetworkView.RPC("RPC_PlayerWantToGoLeft", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.Q))
			{
				_myNetworkView.RPC("RPC_PlayerWantToGoLeft", RPCMode.Server, Network.player, false);
			}
			
			//go right
			if (Input.GetKeyDown(KeyCode.D ))
			{
				_myNetworkView.RPC("RPC_PlayerWantToGoRight", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.D ))
			{
				_myNetworkView.RPC("RPC_PlayerWantToGoRight", RPCMode.Server, Network.player, false);
			}

			//put attractive bomb
			if (Input.GetKeyDown(KeyCode.L))
			{
				_myNetworkView.RPC("RPC_PlayerWantToPutAttractiveBomb", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.L))
			{
				_myNetworkView.RPC("RPC_PlayerWantToPutAttractiveBomb", RPCMode.Server, Network.player, false);
			}
			
			//put repulsive bomb
			if (Input.GetKeyDown(KeyCode.M))
			{
				_myNetworkView.RPC("RPC_PlayerWantToPutRepulsiveBomb", RPCMode.Server, Network.player, true);
			}
			if (Input.GetKeyUp(KeyCode.M))
			{
				_myNetworkView.RPC("RPC_PlayerWantToPutRepulsiveBomb", RPCMode.Server, Network.player, false);
			}
		}
	}
	
	
	
	void FixedUpdate()
	{
		
		if(Network.isServer)
		{
			if(BomberBotsList.Count > 0)
			{
				foreach (var b in BomberBotsList)
				{
					if(!b.Value.BomberbotAnimationScript._shouldBeDead)
					{			
						if(b.Value._wantToGoForward) // go forward
						{
							b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,0,0));
							b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
							_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Key,b.Value.BomberbotTransform.position,b.Value.BomberbotTransform.rotation);

							
						}
						else
						{
							
							if(b.Value._wantToGoBackward) // go forward
							{
								b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,180,0));
								b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
								_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Key,b.Value.BomberbotTransform.position,b.Value.BomberbotTransform.rotation);

								
							}
							else
							{
								if(b.Value._wantToGoRight) //go right
								{
									b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,90,0));
									b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
									_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Key,b.Value.BomberbotTransform.position,b.Value.BomberbotTransform.rotation);

								}
								else
								{
									
									if(b.Value._wantToGoLeft) //go right
									{
										b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,270,0));
										b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
										_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Key,b.Value.BomberbotTransform.position,b.Value.BomberbotTransform.rotation);

									}
								}
							}
						}

						if(b.Value._wantToPutAttrictiveBomb || b.Value._wantToPutRepulsiveBomb) 
						{
							NetworkViewID _netViewBomb = Network.AllocateViewID();
							_myNetworkView.RPC("RPC_InstantiateBomb",RPCMode.All,b.Key,b.Value.BomberbotTransform.position,_netViewBomb);
						}
						
					}
					else
					{

						Debug.Log("DEAD");
						if(b.Value._timeBeforeMoving<=0f)
						{
							b.Value.BomberbotAnimationScript._shouldBeDead = false;
							b.Value._timeBeforeMoving = 2f;
						}
						else
						{
							if(b.Value._timeBeforeMoving == 2f)
							{
								b.Value.BomberbotTransform.rotation = Quaternion.Euler(new Vector3(0,0,0));
								b.Value.BomberbotTransform.position = GetRespawPosition(b.Value.PlayerTeamIndex);
								_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Key,b.Value.BomberbotTransform.position,b.Value.BomberbotTransform.rotation);
								b.Value._timeBeforeMoving -= Time.deltaTime;
							}
							else
							{
								b.Value._timeBeforeMoving -= Time.deltaTime;
							}
						}
					}
				}
				
			}
		}
	}
	void OnConnectedToServer()
	{
		//request for the players list
		if(Network.isClient)
		{

			_myNetworkView.RPC("RPC_SendPlayerConfToServer", RPCMode.Server,GameSettingSingleton.Instance.PlayerName,GameSettingSingleton.Instance.IndexTeamSelected);
			_myNetworkView.RPC("RPC_SendAllPlayers", RPCMode.Server);

		}
	}
	


	[RPC]
	void RPC_SendPlayerConfToServer(string playerName,int playerTeamIndex, NetworkMessageInfo info )
	{
		if(Network.isServer)
		{
			NetworkViewID newViewID = Network.AllocateViewID();
			Vector3 respawn = new Vector3(0,2,0);
			int len;
			//Vector3 respawn = Vector3.up*2;
			 switch (GameSettingSingleton.Instance.Team[playerTeamIndex])
			{
			case "green":
				len = GameSettingSingleton.Instance.GreenHQRespawnPosition.Count;
				respawn += (Vector3)GameSettingSingleton.Instance.GreenHQRespawnPosition[Random.Range(0,len)] ;

				break;

			case "red":
				len = GameSettingSingleton.Instance.RedHQRespawnPosition.Count;
				respawn += (Vector3)GameSettingSingleton.Instance.RedHQRespawnPosition[Random.Range(0,len)];
				
				break;

			case "yellow":
				len = GameSettingSingleton.Instance.YellowHQRespawnPosition.Count;
				respawn += (Vector3)GameSettingSingleton.Instance.YellowHQRespawnPosition[Random.Range(0,len)];
				
				break;

			case "blue":
				len = GameSettingSingleton.Instance.BlueHQRespawnPosition.Count;
				respawn += (Vector3)GameSettingSingleton.Instance.BlueHQRespawnPosition[Random.Range(0,len)];
				
				break;

			}


			_myNetworkView.RPC("RPC_AddPlayerConnected", RPCMode.All, info.sender, respawn, Quaternion.identity, newViewID, playerName, playerTeamIndex);

			Debug.Log("Player " + newViewID.ToString() + " connected from " + info.sender.ipAddress + ":" + info.sender.port);
			Debug.Log("There are now " + Network.connections.Length + " players.");
		}    
	}

	Vector3 GetRespawPosition(int playerTeamIndex)
	{
		Vector3 respawn = new Vector3(0,2,0);
		int len;
		//Vector3 respawn = Vector3.up*2;
		switch (GameSettingSingleton.Instance.Team[playerTeamIndex])
		{
		case "green":
			len = GameSettingSingleton.Instance.GreenHQRespawnPosition.Count;
			respawn += (Vector3)GameSettingSingleton.Instance.GreenHQRespawnPosition[Random.Range(0,len)] ;
			
			break;
			
		case "red":
			len = GameSettingSingleton.Instance.RedHQRespawnPosition.Count;
			respawn += (Vector3)GameSettingSingleton.Instance.RedHQRespawnPosition[Random.Range(0,len)];
			
			break;
			
		case "yellow":
			len = GameSettingSingleton.Instance.YellowHQRespawnPosition.Count;
			respawn += (Vector3)GameSettingSingleton.Instance.YellowHQRespawnPosition[Random.Range(0,len)];
			
			break;
			
		case "blue":
			len = GameSettingSingleton.Instance.BlueHQRespawnPosition.Count;
			respawn += (Vector3)GameSettingSingleton.Instance.BlueHQRespawnPosition[Random.Range(0,len)];
			
			break;
			
		}

		return respawn;
	}

	[RPC]
	void RPC_SendAllPlayers(NetworkMessageInfo info)
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
					_myNetworkView.RPC("RPC_AddPlayerConnected",
					                   info.sender,
					                   b.Value.BomberbotNetPlayer,
					                   b.Value.BomberbotTransform.position,
					                   b.Value.BomberbotTransform.rotation,
					                   playerNetworkView.viewID,
					                   b.Value.PlayerName,
					                   b.Value.PlayerTeamIndex);
				}
				
			}
		}
	}
	
	[RPC]
	void RPC_AddPlayerConnected(NetworkPlayer p,Vector3 playerPosition, Quaternion playerRotation, NetworkViewID newPlayerView,string name,int team)
	{
		if(!BomberBotsList.ContainsKey(p))
		{
			
			BomberBotsList.Add(p, new BomberBot());
			
			
			GameObject go_bb = (GameObject)Instantiate(_BomberbotPrefab, playerPosition, playerRotation);
			go_bb.GetComponent<NetworkView>().viewID = newPlayerView;
			BomberBotsList[p].BomberbotTransform = go_bb.GetComponent<Transform>();

			//go_bb.GetComponentInChildren<TextMesh>().text = newPlayerView.ToString().Remove(0,13)+"P";

			BomberBotsList[p].PlayerName = name;
			BomberBotsList[p].PlayerTeamIndex = team;

			BomberBotsList[p].BomberbotAnimationScript =  go_bb.GetComponent<BomberBotAnimationScript>();
			if(BomberBotsList[p].BomberbotAnimationScript != null)
			{
				BomberBotsList[p].BomberbotAnimationScript.SetPlayerName(name);
				BomberBotsList[p].BomberbotAnimationScript.SetPlayerNameColor(team);


			}
			
			
		}else{
			Debug.Log("Player :"+p.ToString()+" already exist!");
		}
	}
	
	void OnPlayerDisconnected(NetworkPlayer p)
	{
		Destroy(BomberBotsList[p].BomberbotTransform.gameObject);
		BomberBotsList.Remove(p);
		_myNetworkView.RPC("RPC_PlayerDisconnected", RPCMode.Others, p);
		
	}
	
	[RPC]
	void RPC_PlayerDisconnected(NetworkPlayer p)
	{
		Destroy(BomberBotsList[p].BomberbotTransform.gameObject);
		BomberBotsList.Remove(p);
		
	}
	
	#region MOVE_RPC
	
	//send the new player position/rotation to the client
	[RPC]
	void RPC_ClientPlayerUpdate(NetworkPlayer p,Vector3 playerPosition, Quaternion playerRotation)
	{
		if(BomberBotsList.ContainsKey(p))
		{
			BomberBotsList[p].BomberbotTransform.position = playerPosition;
			BomberBotsList[p].BomberbotTransform.rotation = playerRotation;
		}
	}
	
	// go forward
	[RPC]
	void RPC_PlayerWantToGoForward(NetworkPlayer p, bool b)
	{
		
		BomberBotsList[p]._wantToGoForward = b;
		if (Network.isServer)
		{
			
			_myNetworkView.RPC("RPC_PlayerWantToGoForward", RPCMode.Others, p, b);
		}
	}
	
	//go backward
	[RPC]
	void RPC_PlayerWantToGoBackward(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoBackward = b;
		if (Network.isServer)
		{
			_myNetworkView.RPC("RPC_PlayerWantToGoBackward", RPCMode.Others, p, b);
		}
	}
	
	//go right
	[RPC]
	void RPC_PlayerWantToGoRight(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoRight = b;
		if (Network.isServer)
		{
			_myNetworkView.RPC("RPC_PlayerWantToGoRight", RPCMode.Others, p, b);
		}
	}
	
	//go left
	[RPC]
	void RPC_PlayerWantToGoLeft(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoLeft = b;
		if (Network.isServer)
		{
			_myNetworkView.RPC("RPC_PlayerWantToGoLeft", RPCMode.Others, p, b);
		}
	}

	[RPC]
	void RPC_RespawnDeadPlayer(NetworkPlayer p)
	{
	}
	#endregion

	#region RPC_BOMB
	//put an attractive bomb
	[RPC]
	void RPC_PlayerWantToPutAttractiveBomb(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToPutAttrictiveBomb = b;
		if (Network.isServer)
		{
			_myNetworkView.RPC("RPC_PlayerWantToPutAttractiveBomb", RPCMode.Others, p, b);
		}
	}
	
	//put a repulsive bomb
	[RPC]
	void RPC_PlayerWantToPutRepulsiveBomb(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToPutRepulsiveBomb = b;
		if (Network.isServer)
		{

			_myNetworkView.RPC("RPC_PlayerWantToPutRepulsiveBomb", RPCMode.Others, p, b);
		}
	}

	[RPC]
	void RPC_InstantiateBomb(NetworkPlayer p, Vector3 bombPos, NetworkViewID bombViewID)
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
