using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_hovering : MonoBehaviour
{
    // Start is called before the first frame update

    //public float horizontalSpeed;
    //public float verticalSpeed;
    //public float amplitude;

    
    private float amp = 0.15f;
    private float freq= 1.5f;
    private Vector3 init_Pos;

    private void Start()
    {
        init_Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(init_Pos.x,Mathf.Sin(Time.time * freq) * amp + init_Pos.y, 0);
        
    }
}
