using UnityEngine;
using System.Collections;

public class DestroyBlockScript : MonoBehaviour {

	private string colliderTag;
	private NetworkView _myNetView;

	
	// Use this for initialization
	void Start () 
	{
		_myNetView = this.GetComponent<NetworkView>();
	}

	// Update is called once per frame
	void Update () 
	{ 

	}

	void OnCollisionEnter (Collision col)
	{
		if(Network.isServer)
		{
			colliderTag = col.gameObject.tag;
			_myNetView.RPC ("DestroyBlock", RPCMode.All,colliderTag);


		}
	}
	
	[RPC]
	void DestroyBlock(string colTag)
	{
		if (colTag == "RightRepulsiveBlast" || colTag == "LeftRepulsiveBlast" || colTag == "TopRepulsiveBlast" || colTag == "BottomRepulsiveBlast") 
		{

			Debug.Log("Destroy Block : "+this.networkView.viewID);
			if(Network.isServer)
			{
				int arenaWidth = GameSettingSingleton.Instance.CurrentLoadedArena[0];
				int arenaHeight = GameSettingSingleton.Instance.CurrentLoadedArena[1];
				Vector3 blockPos = this.transform.position;
				int currentIndex = arenaWidth*(arenaHeight-(int)blockPos.z+1)+(int)blockPos.x;
				Debug.Log("[OLD] block position ("+blockPos.x+","+blockPos.z+") --- bytes["+currentIndex+"] ="+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" ");
				GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("0");
				Debug.Log("[NEW] block position ("+blockPos.x+","+blockPos.z+") --- bytes["+currentIndex+"] ="+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" ");

			}
			Destroy (this.gameObject);

		}

		
	}
	
	
}
