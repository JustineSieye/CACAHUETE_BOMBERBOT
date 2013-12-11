/* Augustin Gardette */

using UnityEngine;
using System.Collections;



public class WhoIsTheWinnerScript : MonoBehaviour
{
		
	public GameSettingSingleton.Winner _whoIsTheWinner;


	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Flag"){
			GameSettingSingleton.Instance.WinnerTeam = _whoIsTheWinner;
		}

	}

}
