using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    BoxPool _pool;

    void Start()
    {
        _pool = FindObjectOfType<BoxPool>();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            _pool.Get().SetActive(true);
        }
    }
}
