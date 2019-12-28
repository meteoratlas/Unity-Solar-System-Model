using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (transform.position.x - 0.01f, transform.position.y, transform.position.z);
	}
}
