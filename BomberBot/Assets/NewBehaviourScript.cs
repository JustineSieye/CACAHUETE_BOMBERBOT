using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	public Vector2[] uv;
	// Use this for initialization
	void Start () {
		uv = this.GetComponent<MeshFilter>().mesh.uv;
		Debug.Log(this.GetComponent<MeshFilter>().mesh.vertexCount);
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<MeshFilter>().mesh.uv = uv;
		Debug.Log(this.GetComponent<MeshFilter>().mesh.vertexCount);
	}
}
