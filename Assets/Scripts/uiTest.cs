using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiTest : MonoBehaviour {

    public GameObject PlanetNameAnim;

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = PlanetNameAnim.GetComponent<Animator>();
        anim.speed = 0.6f;
        anim.Play("Expand");
	}
	
}
