using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class of card position data
public class PRS
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public int hand_pos;

    public PRS(Vector3 pos, Quaternion rot, Vector3 scale, int hand_pos)
    {
        this.pos = pos;
        this.rot = rot;
        this.scale = scale;
        this.hand_pos = hand_pos;
    }
}

public class Util : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
