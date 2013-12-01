using UnityEngine;
using System.Collections;

public class BomberBotNetworkManagerScript : MonoBehaviour {
	
	public string ip = "127.0.0.1";
	public int port = 25001;
	
	public Texture yellowTeamTexture;
	public Texture redTeamTexture;
	public Texture greenTeamTexture;
	public Texture blueTeamTexture;
	
	private string portStr = "25001";
	//private bool isLoadedArena = false;
	

	void Start()
	{
		if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.joinServer)
		{
			StartServer();
		}
		else
		{
			if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.startServer)
			{
				JoinServer();
			}
		}


	}
		
	void DisplayServerSetting()
	{
		if(Network.peerType == NetworkPeerType.Disconnected)
			{
				GUI.Label(new Rect(25,25,100,25),"Port:");
				portStr = GUI.TextArea(new Rect(25,50,100,25),portStr);
					
				if(GUI.Button(new Rect(25,75,100,25),"Start Server"))
				{
					if(portStr != "")
						port = int.Parse(portStr);
					
					var useNat = !Network.HavePublicAddress();
					Network.InitializeSecurity();
					Network.InitializeServer(10,port,useNat);
				}
			}
			else
			{
	
				if(Network.peerType == NetworkPeerType.Server)
				{
					GUI.Label(new Rect(25,25,100,25),"Server "+port);
					GUI.Label(new Rect(25,50,100,25),"Connections: " + Network.connections.Length);
					
					if(GUI.Button(new Rect(25,75,100,25),"Logout"))
					{
						Logout();
					}
				}
			}
	}
	
	void DisplayClientSetting()
	{
		if(Network.peerType == NetworkPeerType.Disconnected)
			{
				GUI.Label(new Rect(25,25,100,25),"Ip:");
				ip = GUI.TextArea(new Rect(25,50,100,25),ip);

				GUI.Label(new Rect(25,75,100,25),"Port:");
				portStr = GUI.TextArea(new Rect(25,100,100,25),portStr);
				
				GUI.Label(new Rect(25,125,100,25),"Player name:");
				GameSettingSingleton.Instance.PlayerName = GUI.TextArea(new Rect(25,150,100,25),"Player name");
			
				if(GUI.Button(new Rect(25,175,100,25),"Connect"))
				{
					if(portStr != "")
						port = int.Parse(portStr);
					
					Network.Connect(ip,port);
				}
			}
			else
			{
	
				if(Network.peerType == NetworkPeerType.Client)
				{
					if(GUI.Button(new Rect(25,25,100,25),"Logout"))
					{
						Logout();
					}
				}
			}
	}

	void JoinServer()
	{
		var useNat = !Network.HavePublicAddress();
		var port = GameSettingSingleton.Instance.PortToUse;
		Network.InitializeSecurity();
		Network.InitializeServer(10,port,useNat);

	}

	void StartServer()
	{
		var port = GameSettingSingleton.Instance.PortToUse;
		var ip = GameSettingSingleton.Instance.IpToConnect;
		Network.Connect(ip,port);

	}

	void Logout()
	{

		Network.Disconnect(250);
		
		if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.joinServer)
		{
			GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.clientMenu;
			Application.LoadLevel("ClientMenu");

		}
		else
		{
			if(GameSettingSingleton.Instance.CurrentMenuState == GameSettingSingleton.MenuState.startServer)
			{
				GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.serverMenu;
				Application.LoadLevel("ServerMenu");

			}
		}
	}
	
}