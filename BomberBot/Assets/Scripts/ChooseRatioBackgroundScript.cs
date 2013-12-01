using UnityEngine;
using System.Collections;

public class ChooseRatioBackgroundScript : MonoBehaviour {
	
	private float _ratioScreen;
	
	public GameObject _16_x_9_background;
	public GameObject _16_x_10_background;
	public GameObject _4_x_3_background;
	public GameObject _5_x_4_background;
	// Use this for initialization
	void Start ()	 {
		
		Screen.SetResolution(800,600,false);
		InstantiateBackgroungDependingOnScreenSize();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void InstantiateBackgroungDependingOnScreenSize()
	{
		_ratioScreen = (float)Screen.width/Screen.height;

		if(_ratioScreen == 4f/3f)
		{
			GameObject.Instantiate(_4_x_3_background,Vector3.zero,Quaternion.identity);
		}
		else
		{
			if(_ratioScreen == 16f/9f)
			{
				GameObject.Instantiate(_16_x_9_background,Vector3.zero,Quaternion.identity);
			}
			else
			{
				if(_ratioScreen == 5f/4f)
				{
					GameObject.Instantiate(_5_x_4_background,Vector3.zero,Quaternion.identity);

				}
				else
				{
					if(_ratioScreen == 16f/10f)
					{
						GameObject.Instantiate(_16_x_10_background,Vector3.zero,Quaternion.identity);
					}
				}
			}
		}
	}
}
