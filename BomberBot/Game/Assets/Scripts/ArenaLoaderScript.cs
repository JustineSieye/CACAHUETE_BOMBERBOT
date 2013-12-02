using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NetworkView))]

public class ArenaLoaderScript : MonoBehaviour {

	private ArrayList _arenaElements = new ArrayList();
	private Dictionary<NetworkViewID, Vector3> _breakableBlockList;

	private NetworkView _myNetView;
	public GameObject _Flag;
	public GameObject _Ground;
	public GameObject _GroundYellowHQ;
	public GameObject _GroundRedHQ;
	public GameObject _GroundGreenHQ;
	public GameObject _GroundBlueHQ;
	public GameObject _UnbreakableBlock;
	public GameObject _BreakableBlock;
		
	private Camera _cam;

	// Use this for initialization
	void Start ()
	{
		_breakableBlockList = new Dictionary<NetworkViewID, Vector3>(); 
		_cam = GameObject.Find("Main Camera").camera;
		_myNetView = this.GetComponent<NetworkView>();


		//Generate Arena
		if(Network.isServer)
		{
			ResetRespawnPosition();
			_myNetView.RPC("RPC_DestroyArena", RPCMode.All);
			_myNetView.RPC("RPC_GenerateArena", RPCMode.All,GameSettingSingleton.Instance.CurrentLoadedArena);
			
			foreach(var blockPos in _breakableBlockList)
			{
				_myNetView.RPC("GenerateBreakableBlock",RPCMode.All,blockPos.Value,blockPos.Key);
				
			}
		}
	}
	
	#region ARENA FUNCTIONS
	

	public void DestroyArena()
	{
		foreach(GameObject elt in _arenaElements)
		{
			GameObject.Destroy(elt);
			
		}
		_arenaElements.Clear();

	}

	[RPC]
	public void RPC_GenerateArena(byte[] arenaFile)
	{

		 
		int arenaWidth = arenaFile[0];
		int arenaHeight = arenaFile[1];
		Debug.Log ("width"+arenaWidth);
		Debug.Log ("height"+arenaHeight);
		
		
		for(int i = -1;i<=arenaHeight;i++)
		{ 
			for(int j = -1;j<=arenaWidth;j++)
			{
				if(i == -1 || j == -1 || i == arenaHeight || j == arenaWidth)
				{
					_arenaElements.Add(Instantiate(_UnbreakableBlock,new Vector3(j,1,arenaHeight-i),Quaternion.identity));
					_arenaElements.Add(Instantiate(_UnbreakableBlock,new Vector3(j,0,arenaHeight-i),Quaternion.identity));

				}
				else
				{	
					//reads a byte that will determine which element should be instantiated
					switch(arenaFile[arenaWidth*(i+1)+j])
					{
					case 0: // basic ground
						_arenaElements.Add(Instantiate(_Ground,new Vector3(j,0,arenaHeight-i),Quaternion.identity));
						break;

					case 1: //unbreakable Block 
					
						_arenaElements.Add(Instantiate(_UnbreakableBlock,new Vector3(j,1,arenaHeight-i),Quaternion.identity));
						_arenaElements.Add(Instantiate(_Ground,new Vector3(j,0,arenaHeight-i),Quaternion.identity));

					break;
					
					case 2: //breakable block
						NetworkViewID _breakBlockNetView = Network.AllocateViewID();
						_breakableBlockList.Add(_breakBlockNetView,new Vector3(j,1,arenaHeight-i));
						_arenaElements.Add(Instantiate(_Ground,new Vector3(j,0,arenaHeight-i),Quaternion.identity));


						break;
					case 3: // Flag
					
						_arenaElements.Add(Instantiate(_Flag,new Vector3(j,1,arenaHeight-i),Quaternion.identity));
						_arenaElements.Add(Instantiate(_Ground,new Vector3(j,0,arenaHeight-i),Quaternion.identity));

					break;
					
					case 4: // Yellow HQ
					
						_arenaElements.Add(Instantiate(_GroundYellowHQ,new Vector3(j,0,arenaHeight-i),Quaternion.identity));
						if(Network.peerType == NetworkPeerType.Server)
							GameSettingSingleton.Instance.YellowHQRespawnPosition.Add(new Vector3(j,0,arenaHeight-i));

					break;
					
					case 5: //Red HQ
					
						_arenaElements.Add(Instantiate(_GroundRedHQ,new Vector3(j,0,arenaHeight-i),Quaternion.identity));
						if(Network.peerType == NetworkPeerType.Server)
							GameSettingSingleton.Instance.RedHQRespawnPosition.Add(new Vector3(j,0,arenaHeight-i));
							
					
					break;
					
					case 6: //Blue HQ
					
						_arenaElements.Add(Instantiate(_GroundBlueHQ,new Vector3(j,0,arenaHeight-i),Quaternion.identity));
						if(Network.peerType == NetworkPeerType.Server)
							GameSettingSingleton.Instance.BlueHQRespawnPosition.Add(new Vector3(j,0,arenaHeight-i));
						
					break;
					
					case 7: //Green HQ

						_arenaElements.Add(Instantiate(_GroundGreenHQ,new Vector3(j,0,arenaHeight-i),Quaternion.identity));
						if(Network.peerType == NetworkPeerType.Server)
							GameSettingSingleton.Instance.GreenHQRespawnPosition.Add(new Vector3(j,0,arenaHeight-i));

					break;
					}
				}
			}
		}
		
		
		_cam.transform.position = new Vector3(arenaWidth/2f,arenaWidth,arenaHeight/2f);
		_cam.transform.rotation = Quaternion.Euler(new Vector3(89,0,0));
	}
	
	[RPC]
	public void GenerateBreakableBlock(Vector3 blockPosition,NetworkViewID id)
	{
	
			//Debug.Log("GenerateBreakableBlock");
			GameObject newBreakBlock = (GameObject)Instantiate(_BreakableBlock,blockPosition,Quaternion.identity);
			newBreakBlock.networkView.viewID = id;
			_arenaElements.Add(newBreakBlock);

	}

	[RPC]
	void RPC_SendUpdatedArena(NetworkMessageInfo info){
	
		_myNetView.RPC("RPC_GenerateArena", info.sender,GameSettingSingleton.Instance.CurrentLoadedArena);
	
		foreach(var blockPos in _breakableBlockList)
		{
			_myNetView.RPC("GenerateBreakableBlock",info.sender,blockPos.Value,blockPos.Key);
		}
	}

	[RPC]
	public void RPC_DestroyArena()
	{
		foreach(GameObject elt in _arenaElements)
		{
			GameObject.Destroy(elt);
			
		}
		_arenaElements.Clear();

	}
	
	#endregion

	void OnDisconnectedFromServer()
	{
		
		//_myNetView.RPC("RPC_DestroyArena", RPCMode.AllBuffered);
		DestroyArena();

	}
	
	void OnApplicationQuit()
	{
		if(Network.connections.Length > 0)
		{
			DestroyArena();
		}
	}

	void OnConnectedToServer()
	{
		//request for the players list
		if(Network.isClient)
		{
			_myNetView.RPC("RPC_SendUpdatedArena", RPCMode.Server);
		}
	}
		
	
	void ResetRespawnPosition()
	{
		if(Network.isServer)
		{
			GameSettingSingleton.Instance.GreenHQRespawnPosition.Clear();
			GameSettingSingleton.Instance.YellowHQRespawnPosition.Clear();
			GameSettingSingleton.Instance.RedHQRespawnPosition.Clear();
			GameSettingSingleton.Instance.BlueHQRespawnPosition.Clear();
		}
	}
	
	
}
