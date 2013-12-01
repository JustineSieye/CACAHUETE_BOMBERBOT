using UnityEngine;
using System.Collections;

public class TeamTextureUVMapScript : MonoBehaviour {

	// using an image that is an 3x2 grid
	// each image is 0.333 in width and 0.5 in height of the full image
	
	private Rect _uvsFront  = new Rect( 0f, 0f, 1f/3f, 0.5f);
	private Rect _uvsBack = new Rect( 1f/3f, 0f, 1f/3f, 0.5f);
	private Rect _uvsLeft  = new Rect( 2f/3f, 0f, 1f/3f, 0.5f);
	private Rect _uvsRight = new Rect( 0f, 0.5f, 1f/3f, 0.5f);
	private Rect _uvsTop = new Rect( 1f/3f, 0.5f, 1f/3f, 0.5f);
	private Rect _uvsBottom = new Rect( 2f/3f, 0.5f, 1f/3f, 0.5f);
	
	private Mesh _mesh ;
	private Vector2[] _uvs;

	// Use this for initialization
	void Start () 
	{
		_mesh = transform.GetComponent<MeshFilter>().mesh;
		_uvs = new Vector2[_mesh.uv.Length];


		SetUVs();
	}

	void SetUVs()
	{
		// - set UV coordinates -
		
		_uvs[2] = new Vector2( _uvsFront.x, _uvsFront.y );
		_uvs[3] = new Vector2( _uvsFront.x + _uvsFront.width, _uvsFront.y );
		_uvs[0] = new Vector2( _uvsFront.x, _uvsFront.y - _uvsFront.height );
		_uvs[1] = new Vector2( _uvsFront.x + _uvsFront.width, _uvsFront.y - _uvsFront.height );
		
		_uvs[11] = new Vector2( _uvsBack.x, _uvsBack.y );
		_uvs[10] = new Vector2( _uvsBack.x + _uvsBack.width, _uvsBack.y );
		_uvs[7] = new Vector2( _uvsBack.x, _uvsBack.y - _uvsBack.height );
		_uvs[6] = new Vector2( _uvsBack.x + _uvsBack.width, _uvsBack.y - _uvsBack.height );
		
		_uvs[19] = new Vector2( _uvsLeft.x, _uvsLeft.y );
		_uvs[17] = new Vector2( _uvsLeft.x + _uvsLeft.width, _uvsLeft.y );
		_uvs[16] = new Vector2( _uvsLeft.x, _uvsLeft.y - _uvsLeft.height );
		_uvs[18] = new Vector2( _uvsLeft.x + _uvsLeft.width, _uvsLeft.y - _uvsLeft.height );
		
		_uvs[23] = new Vector2( _uvsRight.x, _uvsRight.y );
		_uvs[21] = new Vector2( _uvsRight.x + _uvsRight.width, _uvsRight.y );
		_uvs[20] = new Vector2( _uvsRight.x, _uvsRight.y - _uvsRight.height );
		_uvs[22] = new Vector2( _uvsRight.x + _uvsRight.width, _uvsRight.y - _uvsRight.height );
		
		_uvs[4] = new Vector2( _uvsTop.x, _uvsTop.y );
		_uvs[5] = new Vector2( _uvsTop.x + _uvsTop.width, _uvsTop.y );
		_uvs[8] = new Vector2( _uvsTop.x, _uvsTop.y - _uvsTop.height );
		_uvs[9] = new Vector2( _uvsTop.x + _uvsTop.width, _uvsTop.y - _uvsTop.height );
		
		_uvs[15] = new Vector2( _uvsBottom.x, _uvsBottom.y );
		_uvs[13] = new Vector2( _uvsBottom.x + _uvsBottom.width, _uvsBottom.y );
		_uvs[12] = new Vector2( _uvsBottom.x, _uvsBottom.y - _uvsBottom.height );
		_uvs[14] = new Vector2( _uvsBottom.x + _uvsBottom.width, _uvsBottom.y - _uvsBottom.height );
		
		_mesh.uv = _uvs;
	}
}
