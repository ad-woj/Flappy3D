using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Point : MonoBehaviour {
    
    private bool up = true;
    private int counter;
    public float step;
    
    void Start()
    {
        counter = 150;
    }

    void Update()
    {
        if (counter > 400)
        {
            counter = 0;
            up = !up;
        }
        else
            counter++;
       
        if (up)
            transform.Translate(0, step * Time.deltaTime, 0);

        else
            transform.Translate(0, -step * Time.deltaTime, 0);
       
    }
}
