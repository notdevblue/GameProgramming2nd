using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public KeyCode moveKey = KeyCode.Space;
    private float _t = 0.0f;
    public float speed = 3.0f;

    void Update()
    {
        if (Input.GetKeyDown(moveKey))
        {
            Vector3 pos = this.transform.position;
            pos.z += 1.0f;

            this.transform.position = pos;
        }   
    }
}
