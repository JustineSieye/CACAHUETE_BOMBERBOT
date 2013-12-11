/* Augustin Gardette */

using UnityEngine;
using System.Collections;

public class ChooseRatioBackgroundScript : MonoBehaviour {
	
	private float _ratioScreen;
	
	public GameObject _16_x_9_background;
	public GameObject _16_x_10_background;
	public GameObject _4_x_3_background;
	public GameObject _5_x_4_background;
	// Use this for initialization
	private GameObject _currentBackground;

	void Start ()
	{
		InstantiateBackgroungDependingOnScreenSize();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_ratioScreen != this.camera.aspect)
		{
			Destroy(_currentBackground);
			InstantiateBackgroungDependingOnScreenSize();
		}
	}
	
	void InstantiateBackgroungDependingOnScreenSize()
	{
		_ratioScreen = this.camera.aspect;

		if(_ratioScreen == 4f/3f)
		{
			_currentBackground = (GameObject) Instantiate(_4_x_3_background,Vector3.zero,Quaternion.identity);
		}
		else
		{
			if(_ratioScreen == 16f/9f)
			{
				_currentBackground = (GameObject) Instantiate(_16_x_9_background,Vector3.zero,Quaternion.identity);
			}
			else
			{
				if(_ratioScreen == 5f/4f)
				{
					_currentBackground = (GameObject) Instantiate(_5_x_4_background,Vector3.zero,Quaternion.identity);

				}
				else
				{
					if(_ratioScreen == 16f/10f)
					{
						_currentBackground = (GameObject) Instantiate(_16_x_10_background,Vector3.zero,Quaternion.identity);
					}
				}
			}
		}
	}
}
