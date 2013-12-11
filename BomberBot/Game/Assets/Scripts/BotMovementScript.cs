/* Mickael Cocquempot*/

using UnityEngine;
using System.Collections;

public class BotMovementScript : MonoBehaviour {

	private NetworkView _myNetworkView;
	public Transform child;
	private int _rand;
	private float _changeBotIntents;
	private float _timeBeforeChangeMoveDirection = 2.5f;
	private Vector3 _moveDirection;
	[SerializeField]
	private float _botSpeed = 2f;
	public float botSpeed {
		get {
			return this._botSpeed;
		}
		set {
			_botSpeed = value;
		}
	}  

	public GameObject _repulsiveBombPrefab;
	public GameObject _attractiveBombPrefab;

	void Start()
	{
		_rand = Random.Range(1,5);
		_myNetworkView = this.GetComponent<NetworkView>();
	}
	
	void Update ()
	{


	}

	void FixedUpdate()
	{
		if(Network.isServer)
		{
			if(_timeBeforeChangeMoveDirection < 0f)
			{
				_changeBotIntents = Random.Range(0f,1f);
				if(_changeBotIntents > 0.7f)
				{
					_rand = Random.Range(0,5);
					_myNetworkView.RPC("RPC_UpdateBotPostion",RPCMode.All,_rand);
				}
				else
				{
					_rand = Random.Range(5,7);
					_myNetworkView.RPC("RPC_UpdateBotPostion",RPCMode.All,_rand);
					_rand = Random.Range(0,5);
				}
				_timeBeforeChangeMoveDirection = Random.Range(1f,3f);
			}
			else
			{
				_myNetworkView.RPC("RPC_UpdateBotPostion",RPCMode.All,_rand);

			}
			_timeBeforeChangeMoveDirection -= Time.deltaTime;
		}
		else
		{
			if(Network.peerType == NetworkPeerType.Disconnected)
			{
				if(_timeBeforeChangeMoveDirection < 0f)
				{
					_changeBotIntents = Random.Range(0f,1f);
					if(_changeBotIntents > 0.7f)
					{
						_rand = Random.Range(1,5);
						RandomActionBot(_rand);
					}
					else
					{
						_rand = Random.Range(5,7);
						RandomActionBot(_rand);
						_rand = Random.Range(1,5);
					}
					_timeBeforeChangeMoveDirection = Random.Range(1f,3f);
				}
				else
				{
					RandomActionBot(_rand);

				}
				_timeBeforeChangeMoveDirection -= Time.deltaTime;
			}
		}
	}

	void RandomActionBot(int rand)
	{
		//choisit aléatoirement la prochaine action du bot
		GameObject bomb;
		switch( rand )
		{
		case 0 :
			break;
		case 1 :
			child.rotation = Quaternion.Euler(new Vector3(0,0,0));
			transform.position += transform.forward * _botSpeed * Time.deltaTime;
			break;
		case 2 :
			child.rotation = Quaternion.Euler(new Vector3(0,180,0));
			transform.position -= transform.forward * _botSpeed * Time.deltaTime;
			break;
		case 3 :
			child.rotation = Quaternion.Euler(new Vector3(0,90,0));
			transform.position += transform.right * _botSpeed * Time.deltaTime;
			break;
		case 4 :
			child.rotation = Quaternion.Euler(new Vector3(0,270,0));
			transform.position -= transform.right * _botSpeed * Time.deltaTime;
			break;
		case 5 :
			bomb = (GameObject)Instantiate (_attractiveBombPrefab,RoundPostion(transform.position), Quaternion.identity);
			break;
		case 6 :
			bomb = (GameObject)Instantiate (_repulsiveBombPrefab,RoundPostion(transform.position), Quaternion.identity);
			break;
		}

	}

	void OnCollisionEnter(Collision col)
	{

		_timeBeforeChangeMoveDirection = 0f;
		transform.position = RoundPostion(transform.position);
	}

	Vector3 RoundPostion(Vector3 pos)
	{
		return new Vector3 (Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
	}

	[RPC]

	void RPC_UpdateBotPostion(int rand)
	{
		RandomActionBot(rand);
	}

}

