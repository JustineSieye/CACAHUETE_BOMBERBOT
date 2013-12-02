using UnityEngine;
using System.Collections;



public class WhoIsTheWinnerScript : MonoBehaviour
{
		
	public GameSettingSingleton.Winner _whoIsTheWinner;


	void OnCollisionEnter(Collision col)
	{
		Debug.Log("something on me");
		if(col.gameObject.CompareTag("Flag")){
			Debug.Log("flag on me");
			GameSettingSingleton.Instance.WinnerTeam = _whoIsTheWinner;
		}

	}

}
