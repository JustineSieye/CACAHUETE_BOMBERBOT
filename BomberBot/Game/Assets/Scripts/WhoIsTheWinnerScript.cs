using UnityEngine;
using System.Collections;



public class WhoIsTheWinnerScript : MonoBehaviour
{
		
	public GameSettingSingleton.Winner _whoIsTheWinner;


	void OnCollisonEnter(Collider col)
	{
		Debug.Log("something on me");
		if(col.CompareTag("Flag")){
			print("flag on me");
			GameSettingSingleton.Instance.WinnerTeam = _whoIsTheWinner;
		}

	}

}
