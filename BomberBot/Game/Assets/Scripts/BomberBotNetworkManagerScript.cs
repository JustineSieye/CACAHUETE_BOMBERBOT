/* Gardette Augustin */

using UnityEngine;
using System.Collections;

public class BomberBotNetworkManagerScript : MonoBehaviour
{
	
	public string _ip = "127.0.0.1";
	public int _port = 25001;

	private GameSettingSingleton.MenuState _mymenuState;

	void Awake()
	{
		GameSettingSingleton.Instance.PortToUse = _port;
		GameSettingSingleton.Instance.IpToConnect = _ip;
	}

	void Start()
	{
		_mymenuState = GameSettingSingleton.Instance.CurrentMenuState;

		if(_mymenuState == GameSettingSingleton.MenuState.startServer)
		{
			StartServer();
		}
		else
		{
			if(_mymenuState == GameSettingSingleton.MenuState.joinServer)
			{
				JoinServer();
			}
		}
	}

	void Update()
	{
		if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.logout)
		{
			Logout();
		}
	}

	void JoinServer()
	{
		var port = GameSettingSingleton.Instance.PortToUse;
		var ip = GameSettingSingleton.Instance.IpToConnect;
		Network.Connect(ip,port);

	}

	void StartServer()
	{

		var useNat = !Network.HavePublicAddress();
		var port = GameSettingSingleton.Instance.PortToUse;
		var maxPlayer = GameSettingSingleton.Instance.MaxPlayerNumber;
		Network.InitializeSecurity();
		NetworkConnectionError error = Network.InitializeServer(maxPlayer,port,useNat);

		if(NetworkConnectionError.NoError != error){
			GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.serverMenu;
			Application.LoadLevel("ServerMenu");
		}

	}

	void Logout()
	{

		Network.Disconnect(250);

		if(_mymenuState == GameSettingSingleton.MenuState.joinServer)
		{
			GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.clientMenu;
			Application.LoadLevel("ClientMenu");

		}
		else
		{
			if(_mymenuState == GameSettingSingleton.MenuState.startServer)
			{

				GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.serverMenu;
				Application.LoadLevel("ServerMenu");

			}
		}
	}

	void OnFailedToConnect(NetworkConnectionError error)
	{
	
			GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.clientMenu;
			Application.LoadLevel("ClientMenu");
	
	}

	void OnDisconnectedFromServer(NetworkDisconnection info) {
		GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.clientMenu;
		Application.LoadLevel("ClientMenu");
	}
	
}