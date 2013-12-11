/* Gardette Augustin */

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
		public int _bombBlastPower = 2;
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
			foreach (var b in BomberBotsList)
			{
				if(!b.Value.BomberbotAnimationScript.ShouldBeDead)
				{			
					if(b.Value._wantToGoForward) // go forward
					{
						b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation  = Quaternion.Euler(new Vector3(0,0,0));
						b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
						b.Value.BomberbotTransform.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX ;
						_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Value.BomberbotNetPlayer,b.Value.BomberbotTransform.position,b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation,true);
					}
					else
					{
						
						if(b.Value._wantToGoBackward) // go forward
						{
							b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation = Quaternion.Euler(new Vector3(0,180,0));
							b.Value.BomberbotTransform.position -= b.Value.BomberbotTransform.forward * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
							b.Value.BomberbotTransform.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX ;
							_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Value.BomberbotNetPlayer,b.Value.BomberbotTransform.position,b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation,true);
						}
						else
						{
							if(b.Value._wantToGoRight) //go right
							{
								b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation = Quaternion.Euler(new Vector3(0,90,0));
								b.Value.BomberbotTransform.position += b.Value.BomberbotTransform.right * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
								b.Value.BomberbotTransform.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ ;
								_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Value.BomberbotNetPlayer,b.Value.BomberbotTransform.position,b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation,false);
							}
							else
							{
								
								if(b.Value._wantToGoLeft) //go right
								{
									b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation  = Quaternion.Euler(new Vector3(0,270,0));
									b.Value.BomberbotTransform.position -= b.Value.BomberbotTransform.right * _bomberbotSpeed * b.Value._speedBonus * Time.deltaTime;
									b.Value.BomberbotTransform.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ ;
									_myNetworkView.RPC("RPC_ClientPlayerUpdate",RPCMode.Others,b.Value.BomberbotNetPlayer,b.Value.BomberbotTransform.position,b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation,false);
								}
							}
						}
					}

					if(b.Value._wantToPutAttrictiveBomb || b.Value._wantToPutRepulsiveBomb) 
					{
						NetworkViewID _netViewBomb = Network.AllocateViewID();
						_myNetworkView.RPC("RPC_InstantiateBomb",RPCMode.All,b.Value.BomberbotNetPlayer,b.Value.BomberbotTransform.position,_netViewBomb);
					}
				}
				else
				{
					if(b.Value._timeBeforeMoving<=0f)
					{
						b.Value.BomberbotAnimationScript.ShouldBeDead = false;
						b.Value._timeBeforeMoving = 2f;
					}
					else
					{
						if(b.Value._timeBeforeMoving == 2f)
						{
							b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
							b.Value.BomberbotTransform.position = GetRespawPosition(b.Value.PlayerTeamIndex);

							_myNetworkView.RPC("RPC_ClientPlayerUpdate",
							                   RPCMode.Others,
							                   b.Value.BomberbotNetPlayer,
							                   b.Value.BomberbotTransform.position,
							                   b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation,
							                   true);

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
	void OnConnectedToServer()
	{
		//request for the players list
		if(Network.isClient)
		{
			_myNetworkView.RPC("RPC_SendPlayerConfToServer", RPCMode.Server,GameSettingSingleton.Instance.PlayerName,GameSettingSingleton.Instance.IndexTeamSelected,Network.player);
			_myNetworkView.RPC("RPC_SendAllPlayers", RPCMode.Server,Network.player);
		}
	}
	


	[RPC]
	void RPC_SendPlayerConfToServer(string playerName,int playerTeamIndex,NetworkPlayer netPlayer, NetworkMessageInfo info )
	{
		if(Network.isServer)
		{
			NetworkViewID newViewID = Network.AllocateViewID();
			Vector3 respawn = GetRespawPosition(playerTeamIndex);

			_myNetworkView.RPC("RPC_AddPlayerConnected", RPCMode.All, netPlayer, respawn, Quaternion.identity, newViewID, playerName, playerTeamIndex);

		}    
	}

	Vector3 GetRespawPosition(int playerTeamIndex)
	{
		Vector3 respawn = new Vector3(0,1,0);
		int len;

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
	void RPC_SendAllPlayers(NetworkPlayer netPlayerSender,NetworkMessageInfo info)
	{
		// Send all players already connected to the new connected player
		if(Network.isServer)
		{
			foreach(var b in BomberBotsList)
			{
				NetworkView playerNetworkView = b.Value.BomberbotTransform.gameObject.GetComponent<NetworkView>();
				NetworkPlayer netPlayer = b.Key;
				
				if(b.Value.BomberbotNetPlayer.ToString() != netPlayerSender.ToString())
				{
					_myNetworkView.RPC("RPC_AddPlayerConnected",
					                   netPlayerSender,
					                   b.Value.BomberbotNetPlayer,
					                   b.Value.BomberbotTransform.position,
					                   b.Value.BomberbotAnimationScript._childBomberbot.transform.localRotation,
					                   playerNetworkView.viewID,
					                   b.Value.PlayerName,
					                   b.Value.PlayerTeamIndex);
				}
			}
		}
	}
	
	[RPC]
	void RPC_AddPlayerConnected(NetworkPlayer p,Vector3 playerPosition, Quaternion playerRotation, NetworkViewID newPlayerViewID,string name,int team)
	{
		if(!BomberBotsList.ContainsKey(p))
		{
			BomberBot bb = new BomberBot();

			GameObject go_bb = (GameObject)Instantiate(_BomberbotPrefab, playerPosition, playerRotation);
			go_bb.GetComponent<NetworkView>().viewID = newPlayerViewID;
			bb.BomberbotTransform = go_bb.GetComponent<Transform>();
			bb.BomberbotNetPlayer = p;
			bb.PlayerName = name;
			bb.PlayerTeamIndex = team;

			bb.BomberbotAnimationScript =  go_bb.GetComponent<BomberBotAnimationScript>();
			if(bb.BomberbotAnimationScript != null)
			{
				bb.BomberbotAnimationScript.SetPlayerName(name+" "+p.ToString());
				bb.BomberbotAnimationScript.SetPlayerNameColor(team);
			}
			BomberBotsList.Add(p, bb);
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
	void RPC_ClientPlayerUpdate(NetworkPlayer p,Vector3 playerPosition, Quaternion playerRotation,bool freezeX)
	{
		if(BomberBotsList.ContainsKey(p))
		{
			BomberBotsList[p].BomberbotTransform.position = playerPosition;
			BomberBotsList[p].BomberbotAnimationScript._childBomberbot.transform.localRotation  = playerRotation;

			if(freezeX)
			{
				BomberBotsList[p].BomberbotTransform.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX ;
			}
			else
			{
				BomberBotsList[p].BomberbotTransform.rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ ;
			}
		}
	}
	
	// go forward
	[RPC]
	void RPC_PlayerWantToGoForward(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoForward = b;
	}
	
	//go backward
	[RPC]
	void RPC_PlayerWantToGoBackward(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoBackward = b;
	}
	
	//go right
	[RPC]
	void RPC_PlayerWantToGoRight(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoRight = b;
	}
	
	//go left
	[RPC]
	void RPC_PlayerWantToGoLeft(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToGoLeft = b;
	}

	#endregion

	#region RPC_BOMB
	//put an attractive bomb
	[RPC]
	void RPC_PlayerWantToPutAttractiveBomb(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToPutAttrictiveBomb = b;
		if(Network.isServer)
			_myNetworkView.RPC("RPC_PlayerWantToPutAttractiveBomb", RPCMode.Others, p, b);

		
	}
	
	//put a repulsive bomb
	[RPC]
	void RPC_PlayerWantToPutRepulsiveBomb(NetworkPlayer p, bool b)
	{
		BomberBotsList[p]._wantToPutRepulsiveBomb = b;
		if(Network.isServer)
			_myNetworkView.RPC("RPC_PlayerWantToPutRepulsiveBomb", RPCMode.Others, p, b);
	}

	[RPC]
	void RPC_InstantiateBomb(NetworkPlayer p, Vector3 bombPos, NetworkViewID bombViewID)
	{
		if(BomberBotsList[p]._wantToPutRepulsiveBomb)
		{
			GameObject bomb = (GameObject)Instantiate(_repulsiveBombPrefab,RoundPostion(bombPos), Quaternion.identity);
			bomb.networkView.viewID = bombViewID;
			bomb.SendMessage("SetBombBlastPower",BomberBotsList[p]._bombBlastPower);
			bomb.SendMessage("SetBombOwner",BomberBotsList[p].BomberbotTransform.gameObject);
			BomberBotsList[p]._wantToPutRepulsiveBomb = false;	
		}
		else
		{
			if(BomberBotsList[p]._wantToPutAttrictiveBomb)
			{
				GameObject bomb = (GameObject)Instantiate (_attractiveBombPrefab,RoundPostion(bombPos), Quaternion.identity);
				bomb.networkView.viewID = bombViewID;
				bomb.SendMessage("SetBombBlastPower",BomberBotsList[p]._bombBlastPower);
				bomb.SendMessage("SetBombOwner",BomberBotsList[p].BomberbotTransform.gameObject);
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
