using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour {

    public Rigidbody2D player;
    GameObject[] allObjects;

    void Start () {
        allObjects = GameObject.FindGameObjectsWithTag("ground");  //returns GameObject[]
            
    }
	
	// Update is called once per frame
	void Update () {
        //foreach (GameObject go in allObjects)
        //{
        //    OnCollisionEnter2D(        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);

    }
}
