﻿/* Justine Sieye */

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
				GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("0");
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
				GameSettingSingleton.Instance.CurrentLoadedArena[currentIndex] = byte.Parse("0");
				GameSettingSingleton.Instance.BreakableBlockList.Remove(_myNetView.viewID);
				_myNetView.RPC ("DestroyBlock", RPCMode.All);
			}
			
		}
	}

	[RPC]
	void DestroyBlock()
	{
			Destroy (this.gameObject);
	}
	
	
}
