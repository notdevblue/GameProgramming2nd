using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabrik : MonoBehaviour
{
    public Transform[] joints = new Transform[0];
    public float dist = 2;


    private void Start()
    {
        
    }

    private void Update()
    {
        float stretched = Vector3.Distance(joints[0].position, joints[1].position);
        Vector3 dir = (joints[0].position - joints[1].position).normalized;
        float scalar = stretched - dist;

        joints[1].position = dir * scalar;


    }

    
}
