using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ArenaViewerScript : MonoBehaviour {

	public float _arenaViewerWidth = 3f;
	public GameObject _quadPrefab;

	private ArrayList _quadList = new ArrayList();

	private Vector2[] _uvs = new Vector2[4];

	private Rect _uvsGround;  
	private Rect _uvsUnbreakable; 
	private Rect _uvsBreakable;
	private Rect _uvsFlag;

	private Rect _uvsYellowHQ;
	private Rect _uvsRedHQ;
	private Rect _uvsBlueHQ;
	private Rect _uvsGreenHQ;

	private float _arenaViewerHeight;
	private float _elementSize;

	private int _arenaWidth;
	private int _arenaHeight;
	private int _arenaIndex;
	private float _breakBlockRate = 0.3f; //30% of the arena will be breakable elements

	private bool _generateBreakableBlock;
	// Use this for initialization
	void Start ()
	{
		GameSettingSingleton.Instance.CurrentLoadedArena = GameSettingSingleton.Instance.ArenaFileList[GameSettingSingleton.Instance.IndexArenaSelected].bytes;

		//arena settings
		_arenaWidth = GameSettingSingleton.Instance.CurrentLoadedArena[0];
		_arenaHeight = GameSettingSingleton.Instance.CurrentLoadedArena[1];
		_generateBreakableBlock = (GameSettingSingleton.Instance.CurrentLoadedArena[2]!=0)?true:false;

		_elementSize = _arenaViewerWidth / (_arenaWidth);
		_arenaViewerHeight = _elementSize * (_arenaHeight);
		_arenaIndex = GameSettingSingleton.Instance.IndexArenaSelected;



		_uvsGround  = new Rect( 0f, 0f, 0.25f, 0.5f);
		_uvsUnbreakable = new Rect( 0.25f, 0f, 0.25f, 0.5f);
		_uvsBreakable  = new Rect(  0.5f, 0f, 0.25f, 0.5f);
		_uvsFlag = new Rect(  0.75f, 0f, 0.25f, 0.5f);
		
		_uvsYellowHQ = new Rect( 0f, 0.5f, 0.25f, 0.5f);
		_uvsRedHQ = new Rect( 0.25f, 0.5f, 0.25f, 0.5f);
		_uvsBlueHQ = new Rect( 0.5f, 0.5f, 0.25f, 0.5f);
		_uvsGreenHQ = new Rect( 0.75f, 0.5f, 0.25f, 0.5f);

		GenerateArenaViewer();

		this.transform.localPosition = new Vector3(0f,-_arenaViewerHeight-1f ,0f);

	}
	
	// Update is called once per frame
	void Update ()
	{
		//this.GetComponent<MeshFilter>().mesh.uv = uv;
		if(_arenaIndex != GameSettingSingleton.Instance.IndexArenaSelected )
		{
			Debug.Log("Change arena on the viewer");
			GameSettingSingleton.Instance.CurrentLoadedArena = GameSettingSingleton.Instance.ArenaFileList[GameSettingSingleton.Instance.IndexArenaSelected].bytes;

			_arenaWidth = GameSettingSingleton.Instance.CurrentLoadedArena[0];
			_arenaHeight = GameSettingSingleton.Instance.CurrentLoadedArena[1];
			_generateBreakableBlock = (GameSettingSingleton.Instance.CurrentLoadedArena[2]!=0)?true:false;

			_elementSize = _arenaViewerWidth / (_arenaWidth);
			_arenaViewerHeight = _elementSize * (_arenaHeight);
			_arenaIndex = GameSettingSingleton.Instance.IndexArenaSelected;
			this.transform.localPosition = new Vector3(0f,-_arenaViewerHeight-1f,0f);
			GenerateArenaViewer();


		}
		else
		{
			if( GameSettingSingleton.Instance.UpdateArenaViewer == true)
			{
				UpdateViewerUV();
				GameSettingSingleton.Instance.UpdateArenaViewer = false;
			}
		}


	}


	//call when the player click on an arena element to change him
	void UpdateViewerUV()
	{

		foreach(GameObject obj in _quadList)
		{
			EditArenaElementScript quadScript = obj.GetComponent<EditArenaElementScript>();
			if(!quadScript.IsUpdated)
			{
				switch(quadScript.ValueElement)
				{
					
				case 0: //ground 
					_uvs[1] = new Vector2( _uvsGround.x, _uvsGround.y );
					_uvs[3] = new Vector2( _uvsGround.x + _uvsGround.width, _uvsGround.y );
					_uvs[2] = new Vector2( _uvsGround.x, _uvsGround.y - _uvsGround.height );
					_uvs[0] = new Vector2( _uvsGround.x + _uvsGround.width, _uvsGround.y - _uvsGround.height );
					
					break;

				case 1: //unbreakable Block 
					_uvs[1] = new Vector2( _uvsUnbreakable.x, _uvsUnbreakable.y );
					_uvs[3] = new Vector2( _uvsUnbreakable.x + _uvsUnbreakable.width, _uvsUnbreakable.y );
					_uvs[2] = new Vector2( _uvsUnbreakable.x, _uvsUnbreakable.y - _uvsUnbreakable.height );
					_uvs[0] = new Vector2( _uvsUnbreakable.x + _uvsUnbreakable.width, _uvsUnbreakable.y - _uvsUnbreakable.height );
					
					break;

				case 2: //breakable Block 
					_uvs[1] = new Vector2( _uvsBreakable.x, _uvsBreakable.y );
					_uvs[3] = new Vector2( _uvsBreakable.x + _uvsBreakable.width, _uvsBreakable.y );
					_uvs[2] = new Vector2( _uvsBreakable.x, _uvsBreakable.y - _uvsBreakable.height );
					_uvs[0] = new Vector2( _uvsBreakable.x + _uvsBreakable.width, _uvsBreakable.y - _uvsBreakable.height );

					break;
					
				case 3: // Flag
					_uvs[1] = new Vector2( _uvsFlag.x, _uvsFlag.y );
					_uvs[3] = new Vector2( _uvsFlag.x + _uvsFlag.width, _uvsFlag.y );
					_uvs[2] = new Vector2( _uvsFlag.x, _uvsFlag.y - _uvsFlag.height );
					_uvs[0] = new Vector2( _uvsFlag.x + _uvsFlag.width, _uvsFlag.y - _uvsFlag.height );
					
					break;
					
				case 4: // Yellow HQ
					_uvs[1] = new Vector2( _uvsYellowHQ.x, _uvsYellowHQ.y );
					_uvs[3] = new Vector2( _uvsYellowHQ.x + _uvsYellowHQ.width, _uvsYellowHQ.y );
					_uvs[2] = new Vector2( _uvsYellowHQ.x, _uvsYellowHQ.y - _uvsYellowHQ.height );
					_uvs[0] = new Vector2( _uvsYellowHQ.x + _uvsYellowHQ.width, _uvsYellowHQ.y - _uvsYellowHQ.height );
					
					break;
					
				case 5: //Red HQ
					_uvs[1] = new Vector2( _uvsRedHQ.x, _uvsRedHQ.y );
					_uvs[3] = new Vector2( _uvsRedHQ.x + _uvsRedHQ.width, _uvsRedHQ.y );
					_uvs[2] = new Vector2( _uvsRedHQ.x, _uvsRedHQ.y - _uvsRedHQ.height );
					_uvs[0] = new Vector2( _uvsRedHQ.x + _uvsRedHQ.width, _uvsRedHQ.y - _uvsRedHQ.height );
					
					break;
					
				case 6: //Blue HQ
					_uvs[1] = new Vector2( _uvsBlueHQ.x, _uvsBlueHQ.y );
					_uvs[3] = new Vector2( _uvsBlueHQ.x + _uvsBlueHQ.width, _uvsBlueHQ.y );
					_uvs[2] = new Vector2( _uvsBlueHQ.x, _uvsBlueHQ.y - _uvsBlueHQ.height );
					_uvs[0] = new Vector2( _uvsBlueHQ.x + _uvsBlueHQ.width, _uvsBlueHQ.y - _uvsBlueHQ.height );
					
					break;
					
				case 7: //Green HQ
					_uvs[1] = new Vector2( _uvsGreenHQ.x, _uvsGreenHQ.y );
					_uvs[3] = new Vector2( _uvsGreenHQ.x + _uvsGreenHQ.width, _uvsGreenHQ.y );
					_uvs[2] = new Vector2( _uvsGreenHQ.x, _uvsGreenHQ.y - _uvsGreenHQ.height );
					_uvs[0] = new Vector2( _uvsGreenHQ.x + _uvsGreenHQ.width, _uvsGreenHQ.y - _uvsGreenHQ.height );
					
					break;

				}
				obj.GetComponent<MeshFilter>().mesh.uv = _uvs;
				obj.GetComponent<EditArenaElementScript>().IsUpdated = true;
			}
		}
	}

	void ClearArenaViewer()
	{
		foreach(GameObject obj in _quadList)
		{
			Destroy(obj);
		}
		_quadList.Clear();
	}

		
	void GenerateArenaViewer()
	{
		ClearArenaViewer();

		for(int i = 0;i<_arenaHeight;i++)
		{ 
			for(int j = 0;j<_arenaWidth;j++)
			{

				switch(GameSettingSingleton.Instance.CurrentLoadedArena[_arenaWidth*(i+1)+j])
				{

				
				case 1: //unbreakable Block 
					_uvs[1] = new Vector2( _uvsUnbreakable.x, _uvsUnbreakable.y );
					_uvs[3] = new Vector2( _uvsUnbreakable.x + _uvsUnbreakable.width, _uvsUnbreakable.y );
					_uvs[2] = new Vector2( _uvsUnbreakable.x, _uvsUnbreakable.y - _uvsUnbreakable.height );
					_uvs[0] = new Vector2( _uvsUnbreakable.x + _uvsUnbreakable.width, _uvsUnbreakable.y - _uvsUnbreakable.height );

					break;
				
				case 3: // Flag
					_uvs[1] = new Vector2( _uvsFlag.x, _uvsFlag.y );
					_uvs[3] = new Vector2( _uvsFlag.x + _uvsFlag.width, _uvsFlag.y );
					_uvs[2] = new Vector2( _uvsFlag.x, _uvsFlag.y - _uvsFlag.height );
					_uvs[0] = new Vector2( _uvsFlag.x + _uvsFlag.width, _uvsFlag.y - _uvsFlag.height );

					break;
					
				case 4: // Yellow HQ
					_uvs[1] = new Vector2( _uvsYellowHQ.x, _uvsYellowHQ.y );
					_uvs[3] = new Vector2( _uvsYellowHQ.x + _uvsYellowHQ.width, _uvsYellowHQ.y );
					_uvs[2] = new Vector2( _uvsYellowHQ.x, _uvsYellowHQ.y - _uvsYellowHQ.height );
					_uvs[0] = new Vector2( _uvsYellowHQ.x + _uvsYellowHQ.width, _uvsYellowHQ.y - _uvsYellowHQ.height );

					break;
					
				case 5: //Red HQ
					_uvs[1] = new Vector2( _uvsRedHQ.x, _uvsRedHQ.y );
					_uvs[3] = new Vector2( _uvsRedHQ.x + _uvsRedHQ.width, _uvsRedHQ.y );
					_uvs[2] = new Vector2( _uvsRedHQ.x, _uvsRedHQ.y - _uvsRedHQ.height );
					_uvs[0] = new Vector2( _uvsRedHQ.x + _uvsRedHQ.width, _uvsRedHQ.y - _uvsRedHQ.height );
							
					break;
					
				case 6: //Blue HQ
					_uvs[1] = new Vector2( _uvsBlueHQ.x, _uvsBlueHQ.y );
					_uvs[3] = new Vector2( _uvsBlueHQ.x + _uvsBlueHQ.width, _uvsBlueHQ.y );
					_uvs[2] = new Vector2( _uvsBlueHQ.x, _uvsBlueHQ.y - _uvsBlueHQ.height );
					_uvs[0] = new Vector2( _uvsBlueHQ.x + _uvsBlueHQ.width, _uvsBlueHQ.y - _uvsBlueHQ.height );
				
					break;
					
				case 7: //Green HQ
					_uvs[1] = new Vector2( _uvsGreenHQ.x, _uvsGreenHQ.y );
					_uvs[3] = new Vector2( _uvsGreenHQ.x + _uvsGreenHQ.width, _uvsGreenHQ.y );
					_uvs[2] = new Vector2( _uvsGreenHQ.x, _uvsGreenHQ.y - _uvsGreenHQ.height );
					_uvs[0] = new Vector2( _uvsGreenHQ.x + _uvsGreenHQ.width, _uvsGreenHQ.y - _uvsGreenHQ.height );
				
					break;

				default:
					if(_generateBreakableBlock)
					{
						if(Random.Range(0f,1f)<=_breakBlockRate)
						{
							_uvs[1] = new Vector2( _uvsBreakable.x, _uvsBreakable.y );
							_uvs[3] = new Vector2( _uvsBreakable.x + _uvsBreakable.width, _uvsBreakable.y );
							_uvs[2] = new Vector2( _uvsBreakable.x, _uvsBreakable.y - _uvsBreakable.height );
							_uvs[0] = new Vector2( _uvsBreakable.x + _uvsBreakable.width, _uvsBreakable.y - _uvsBreakable.height );
							GameSettingSingleton.Instance.CurrentLoadedArena[_arenaWidth*(i+1)+j] = 2;
											
						}
						else
						{
							_uvs[1] = new Vector2( _uvsGround.x, _uvsGround.y );
							_uvs[3] = new Vector2( _uvsGround.x + _uvsGround.width, _uvsGround.y );
							_uvs[2] = new Vector2( _uvsGround.x, _uvsGround.y - _uvsGround.height );
							_uvs[0] = new Vector2( _uvsGround.x + _uvsGround.width, _uvsGround.y - _uvsGround.height );
							GameSettingSingleton.Instance.CurrentLoadedArena[_arenaWidth*(i+1)+j] = 0;
						}
						
					}
					else
					{
						if(GameSettingSingleton.Instance.CurrentLoadedArena[_arenaWidth*(i+1)+j] == 2){
							_uvs[1] = new Vector2( _uvsBreakable.x, _uvsBreakable.y );
							_uvs[3] = new Vector2( _uvsBreakable.x + _uvsBreakable.width, _uvsBreakable.y );
							_uvs[2] = new Vector2( _uvsBreakable.x, _uvsBreakable.y - _uvsBreakable.height );
							_uvs[0] = new Vector2( _uvsBreakable.x + _uvsBreakable.width, _uvsBreakable.y - _uvsBreakable.height );
						}
						else
						{
							_uvs[1] = new Vector2( _uvsGround.x, _uvsGround.y );
							_uvs[3] = new Vector2( _uvsGround.x + _uvsGround.width, _uvsGround.y );
							_uvs[2] = new Vector2( _uvsGround.x, _uvsGround.y - _uvsGround.height );
							_uvs[0] = new Vector2( _uvsGround.x + _uvsGround.width, _uvsGround.y - _uvsGround.height );
						}
						
					}
				
					
					break;

			
				}

				GameObject quadInstance = (GameObject)Instantiate(_quadPrefab);

				//update quad uv
				quadInstance.transform.localScale = new Vector3(_elementSize,_elementSize,_elementSize);
				quadInstance.transform.parent = this.transform;
				quadInstance.transform.localPosition = new Vector3(j*_elementSize-_arenaViewerWidth/2f,_arenaViewerHeight/2f-i*_elementSize,0f);
				quadInstance.transform.localRotation = Quaternion.identity;
				quadInstance.GetComponent<MeshFilter>().mesh.uv = _uvs;

				EditArenaElementScript quadScript = quadInstance.GetComponent<EditArenaElementScript>() ;

				//set value and index in the quad
				quadScript.IndexElement = _arenaWidth*(i+1)+j;
				quadScript.ValueElement = GameSettingSingleton.Instance.CurrentLoadedArena[_arenaWidth*(i+1)+j];

				_quadList.Add(quadInstance);
			}
		}
	}
}
