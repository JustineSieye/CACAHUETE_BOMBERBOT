using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class WinnerTeamDisplayScript : MonoBehaviour
{
	public Texture _greenWinnerTexture;
	public Texture _redWinnerTexture;
	public Texture _blueWinnerTexture;
	public Texture _yellowWinnerTexture;


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
			if((int)GameSettingSingleton.Instance.WinnerTeam != 0)
			{
				Debug.Log("winner");
				_myNetworkView.RPC("RPC_DisplayWinner",RPCMode.All,(int)GameSettingSingleton.Instance.WinnerTeam);
			}
		}

	}

	[RPC]
	void RPC_DisplayWinner(int win)
	{


		switch(win)
		{
		case 1: //green Team
			this.renderer.material.SetTexture("_MainTex",_greenWinnerTexture);
			break;

		case 2: //red team
			this.renderer.material.SetTexture("_MainTex",_redWinnerTexture);
			break;

		case 3://blue team
			this.renderer.material.SetTexture("_MainTex",_blueWinnerTexture);
			break;

		case 4://yellow team
			this.renderer.material.SetTexture("_MainTex",_yellowWinnerTexture);
			break;

		
		}
		Debug.Log("winner");
		this.renderer.enabled = true;
	}
}
