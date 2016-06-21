using UnityEngine;
using System.Collections.Generic;

public class Obstacle : MonoBehaviour {
    
    public float speed;
    private uint difficulty;
    public float lifetime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float dt = Time.deltaTime;
        Vector3 shift = new Vector3( 0, 0, - speed * dt );
        
        lifetime -= dt;
        if( lifetime <= 0 )
        {
            Destroy( this.gameObject );
        }
        transform.Translate( shift );
        
	}
}
