using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class WinnerTeamDisplayScript : MonoBehaviour
{
	public Texture _greenWinnerTexture;
	public Texture _redWinnerTexture;
	public Texture _blueWinnerTexture;
	public Texture _yellowWinnerTexture;

	private int _winnerIndex = 0;
	private float _timeRemainingBeforeExit = 4f;
	private NetworkView _myNetworkView;

	// Use this for initialization
	void Start () 
	{
		_myNetworkView = this.networkView;
		this.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Network.isServer){
			if((int)GameSettingSingleton.Instance.WinnerTeam != 0 && _winnerIndex == 0)
			{

				_myNetworkView.RPC("RPC_DisplayWinner",RPCMode.All,(int)GameSettingSingleton.Instance.WinnerTeam);

			}
		}

		if(_winnerIndex != 0)
		{
			if(_timeRemainingBeforeExit <=0f)
			{

				if(Network.isServer)
				{
					Network.Disconnect(250);
					GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.serverMenu;
					Application.LoadLevel("ServerMenu");
				}
				else
				{
					if(Network.isClient)
					{
						Network.Disconnect(250);
						GameSettingSingleton.Instance.CurrentMenuState = GameSettingSingleton.MenuState.clientMenu;
						Application.LoadLevel("ClientMenu");
					}
				}
				GameSettingSingleton.Instance.WinnerTeam = GameSettingSingleton.Winner.none;
			}

			_timeRemainingBeforeExit-=Time.deltaTime;
		}

	}

	[RPC]
	void RPC_DisplayWinner(int win)
	{

		_winnerIndex = win;
		switch(win)
		{
		case 1: //green Team
			this.renderer.material.SetTexture("_MainTex",_greenWinnerTexture);
			break;

		case 2://blue team
			this.renderer.material.SetTexture("_MainTex",_blueWinnerTexture);
			break;

		case 3: //red team
			this.renderer.material.SetTexture("_MainTex",_redWinnerTexture);
			break;

		case 4://yellow team
			this.renderer.material.SetTexture("_MainTex",_yellowWinnerTexture);
			break;

		
		}
		this.renderer.enabled = true;



	}
}
