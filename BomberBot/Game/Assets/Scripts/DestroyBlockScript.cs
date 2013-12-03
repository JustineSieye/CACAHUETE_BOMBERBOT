using UnityEngine;
using System.Collections;

public class DestroyBlockScript : MonoBehaviour {

	private string _colliderTag;
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



	void OnTriggerEnter (Collider col)
	{
		if(Network.isServer)
		{

			_colliderTag = col.gameObject.tag;

			if (_colliderTag.Contains("RepulsiveBlast")) 
			{
				int arenaWidth = GameSettingSingleton.Instance.CurrentLoadedArena[0];
				int arenaHeight = GameSettingSingleton.Instance.CurrentLoadedArena[1];
				Vector3 blockPos = this.transform.position;
				int currentIndex = arenaWidth*(arenaHeight-(int)blockPos.z+1)+(int)blockPos.x;
				Debug.Log("[OLD] block position ("+blockPos.x+","+blockPos.z+") --- bytes["+currentIndex+"] ="+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" ");
				GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("0");
				Debug.Log("[NEW] block position ("+blockPos.x+","+blockPos.z+") --- bytes["+currentIndex+"] ="+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" ");

				_myNetView.RPC ("DestroyBlock", RPCMode.All);
			}
			
		}
	}


	void OnCollisionEnter (Collision col)
	{
		if(Network.isServer)
		{
			_colliderTag = col.gameObject.tag;

			if (_colliderTag.Contains("RepulsiveBlast")) 
			{
				int arenaWidth = GameSettingSingleton.Instance.CurrentLoadedArena[0];
				int arenaHeight = GameSettingSingleton.Instance.CurrentLoadedArena[1];
				Vector3 blockPos = this.transform.position;
				int currentIndex = arenaWidth*(arenaHeight-(int)blockPos.z+1)+(int)blockPos.x;
				Debug.Log("[OLD] block position ("+blockPos.x+","+blockPos.z+") --- bytes["+currentIndex+"] ="+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" ");
				GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("0");
				Debug.Log("[NEW] block position ("+blockPos.x+","+blockPos.z+") --- bytes["+currentIndex+"] ="+GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex]+" ");


				_myNetView.RPC ("DestroyBlock", RPCMode.All);
			}
			
		}
	}

	[RPC]
	void DestroyBlock()
	{

			Debug.Log("Destroy Block : "+this.networkView.viewID);
			Destroy (this.gameObject);

	}
	
	
}
