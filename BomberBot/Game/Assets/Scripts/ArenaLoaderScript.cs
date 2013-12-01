using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NetworkView))]

public class ArenaLoaderScript : MonoBehaviour {

	
	private Object[] _arenaFileList;

	private ArrayList _arenaElements = new ArrayList();
	private ArrayList _arenaNetworkElements = new ArrayList();
	private Dictionary<NetworkViewID, Vector3> _breakableBlockList;

	private NetworkView _myNetView;
	//HQ Position
	

	private int _arenaCnt = 0;
	
	public GameObject _Flag;
	public GameObject _Ground;
	public GameObject _GroundYellowHQ;
	public GameObject _GroundRedHQ;
	public GameObject _GroundGreenHQ;
	public GameObject _GroundBlueHQ;
	public GameObject _UnbreakableBlock;
	public GameObject _BreakableBlock;
	
	
	private int _BreakBlockRate = 3;
	private Camera _cam;

	

	// Use this for initialization
	void Start () {
		_arenaFileList = Resources.LoadAll("Arena",typeof(TextAsset));
		_breakableBlockList = new Dictionary<NetworkViewID, Vector3>(); 
		_cam = GameObject.Find("Main Camera").camera;
		_myNetView = this.GetComponent<NetworkView>();
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		
		if(Network.peerType != NetworkPeerType.Disconnected)
		{
			if(Network.peerType == NetworkPeerType.Server)
			{
				_arenaCnt = 0;
				foreach(TextAsset arenaFile in _arenaFileList)
				{

					if(GUI.Button(new Rect(25,125+25*_arenaCnt,100,25),arenaFile.name))
					{
						Debug.Log(arenaFile.name);
						
						//DestroyArena();
						//GenerateArena(arenaFile,true);
						GameSettingSingleton.Instance.CurrentLoadedArena = arenaFile.bytes;

						_myNetView.RPC("RPC_DestroyArena", RPCMode.AllBuffered);
						_myNetView.RPC("RPC_GenerateArena", RPCMode.AllBuffered,arenaFile.bytes);





						foreach(var blockPos in _breakableBlockList)
						{
							_myNetView.RPC("GenerateBreakableBlock",RPCMode.AllBuffered,blockPos.Value,blockPos.Key);

						}
					}
					
				_arenaCnt++;
				
				}
			}
		}
		
	}
	
	#region ARENA FUNCTIONS
	
	public void GenerateArena(TextAsset arenaFile)
	{
		
		int arenaWidth = arenaFile.bytes[0];
		int arenaHeight = arenaFile.bytes[1];
		bool generateBreakableBlock = (arenaFile.bytes[2]!=0)?true:false;
		Debug.Log ("width"+arenaWidth);
		Debug.Log ("height"+arenaHeight);
		_cam.transform.position = new Vector3(arenaWidth/2f,arenaWidth,arenaHeight/2f);
		_cam.transform.rotation = Quaternion.Euler(new Vector3(89,0,0));
		
		for(int i = -1;i<=arenaHeight;i++)
		{ 
			for(int j = -1;j<=arenaWidth;j++)
			{
				if(i == -1 || j == -1 || i == arenaHeight || j == arenaWidth)
				{
					_arenaElements.Add(Network.Instantiate(_UnbreakableBlock,new Vector3(j,1,arenaHeight-i),Quaternion.identity,0));
					_arenaElements.Add(Network.Instantiate(_UnbreakableBlock,new Vector3(j,0,arenaHeight-i),Quaternion.identity,0));

				}else{	
					switch(arenaFile.bytes[arenaWidth*(i+1)+j])
					{
						case 1: //unbreakable Block 
						
							_arenaElements.Add(Network.Instantiate(_UnbreakableBlock,new Vector3(j,1,arenaHeight-i),Quaternion.identity,0));
							_arenaElements.Add(Network.Instantiate(_Ground,new Vector3(j,0,arenaHeight-i),Quaternion.identity,0));

						break;
						
						case 3: // Flag
						
							_arenaElements.Add(Network.Instantiate(_Flag,new Vector3(j,1,arenaHeight-i),Quaternion.identity,0));
							_arenaElements.Add(Network.Instantiate(_Ground,new Vector3(j,0,arenaHeight-i),Quaternion.identity,0));

						break;
						
						case 4: // Yellow HQ
						
							_arenaElements.Add(Network.Instantiate(_GroundYellowHQ,new Vector3(j,0,arenaHeight-i),Quaternion.identity,0));
							GameSettingSingleton.Instance.YellowHQRespawnPosition.Add(new Vector3(j,0,arenaHeight-i));

						break;
						
						case 5: //Red HQ
						
							_arenaElements.Add(Network.Instantiate(_GroundRedHQ,new Vector3(j,0,arenaHeight-i),Quaternion.identity,0));
							GameSettingSingleton.Instance.RedHQRespawnPosition.Add(new Vector3(j,0,arenaHeight-i));
							
						
						break;
						
						case 6: //Blue HQ
						
							_arenaElements.Add(Network.Instantiate(_GroundBlueHQ,new Vector3(j,0,arenaHeight-i),Quaternion.identity,0));
							GameSettingSingleton.Instance.BlueHQRespawnPosition.Add(new Vector3(j,0,arenaHeight-i));
						
						break;
						
						case 7: //Green HQ

							_arenaElements.Add(Network.Instantiate(_GroundGreenHQ,new Vector3(j,0,arenaHeight-i),Quaternion.identity,0));
							GameSettingSingleton.Instance.GreenHQRespawnPosition.Add(new Vector3(j,0,arenaHeight-i));

						break;
						
					default:
							if(generateBreakableBlock)
							{
								

								

								if(Random.Range(0,_BreakBlockRate)==2)
								{
									_arenaElements.Add(Network.Instantiate(_BreakableBlock,new Vector3(j,1,arenaHeight-i),Quaternion.identity,0));
									
									
								}

							}
							else
							{
								if(arenaFile.bytes[arenaWidth*(i+1)+j] == 2)
									_arenaElements.Add(Network.Instantiate(_BreakableBlock,new Vector3(j,1,arenaHeight-i),Quaternion.identity,0));
							
							}
							_arenaElements.Add(Network.Instantiate(_Ground,new Vector3(j,0,arenaHeight-i),Quaternion.identity,0));

						break;
						
						
					}
				}
			}
		}
	}
	
	public void DestroyArena()
	{
		foreach(GameObject elt in _arenaElements)
		{
			GameObject.Destroy(elt);
			
		}
		_arenaElements.Clear();
		
		if(Network.isServer)
		{
			foreach(GameObject eltNet in _arenaNetworkElements)
			{
				Network.Destroy(eltNet);
				
			}
			_arenaNetworkElements.Clear();
		}
	}
	
	#endregion
	
	#region RPC ARENA FUNCTIONS
	
	[RPC]
	public void RPC_GenerateArena(byte[] arenaFile)
	{
		
		int arenaWidth = arenaFile[0];
		int arenaHeight = arenaFile[1];
		bool generateBreakableBlock = (arenaFile[2]!=0)?true:false;
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

				}else{	
					switch(arenaFile[arenaWidth*(i+1)+j])
					{
						case 1: //unbreakable Block 
						
							_arenaElements.Add(Instantiate(_UnbreakableBlock,new Vector3(j,1,arenaHeight-i),Quaternion.identity));
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
						
					default:
						
						if(generateBreakableBlock)
						{
							if(Network.peerType == NetworkPeerType.Server)
							{
								int currentIndex = arenaWidth*(i+1)+j;
								GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("0");
								if(Random.Range(0,_BreakBlockRate)==2)
								{
									NetworkViewID _breakBlockNetView = Network.AllocateViewID();
									_breakableBlockList.Add(_breakBlockNetView,new Vector3(j,1,arenaHeight-i));
									GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("2");

								}
							}
						}
						else
						{
							if(arenaFile[arenaWidth*(i+1)+j] == 2)
							{
								NetworkViewID _breakBlockNetView = Network.AllocateViewID();
								_breakableBlockList.Add(_breakBlockNetView,new Vector3(j,1,arenaHeight-i));
							}
						}

						
						_arenaElements.Add(Instantiate(_Ground,new Vector3(j,0,arenaHeight-i),Quaternion.identity));
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
	public void RPC_DestroyArena()
	{
		foreach(GameObject elt in _arenaElements)
		{
			GameObject.Destroy(elt);
			
		}
		_arenaElements.Clear();
		
		if(Network.isServer)
		{
			foreach(GameObject eltNet in _arenaNetworkElements)
			{
				Network.Destroy(eltNet);
				
			}
			_arenaNetworkElements.Clear();
		}
	}
	
	#endregion
	void OnDisconnectedFromServer()
	{
		
		//_myNetView.RPC("RPC_DestroyArena", RPCMode.AllBuffered);
		DestroyArena();
		ResetRespawnPosition();
	}
	
	void OnApplicationQuit()
	{
		if(Network.connections.Length > 0)
		{
			DestroyArena();
			//_myNetView.RPC("RPC_DestroyArena", RPCMode.AllBuffered);
		}
	}
	
	void ResetRespawnPosition()
	{
		GameSettingSingleton.Instance.GreenHQRespawnPosition.Clear();
		GameSettingSingleton.Instance.YellowHQRespawnPosition.Clear();
		GameSettingSingleton.Instance.RedHQRespawnPosition.Clear();
		GameSettingSingleton.Instance.BlueHQRespawnPosition.Clear();
	}
	
	
}
