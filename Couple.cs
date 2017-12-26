﻿using UnityEngine;
using System.Collections;

public class Couple : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col){
		//quand Lucile entre en contact avec la voiture la voiture freine
		if (col.gameObject.tag == "Player") {
			player.GetComponent<Player> ().embrasser = true;
		}
	}
}
