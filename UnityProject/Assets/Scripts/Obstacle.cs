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
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        MeshRenderer ren;
        float dt = Time.deltaTime;
        Vector3 shift = new Vector3( 0, speed * dt, 0 );
        foreach( Obstacle obs in obstacles )
        {
            obs.lifetime -= dt;
            if( obs.lifetime <= 0 )
            {
                ren = obs.GetComponent<MeshRenderer>();
                if( ren )
                    ren.enabled = false;
            }
            transform.Translate( shift );
        }
	}
}
