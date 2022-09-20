using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPool : MonoBehaviour
{
    public GameObject original;
    public int initialPoolObjCount = 20;
    public Vector3 dropPos;

    private List<GameObject> _pool;


    private void Awake()
    {
        _pool = new List<GameObject>();

        int i = initialPoolObjCount;
        while (--i >= 0)
            _pool.Add(Create());
    }

    private GameObject Create()
    {
        GameObject obj = Instantiate(original);
        obj.SetActive(false);
        obj.transform.position = dropPos;
        return obj;
    }

    public GameObject Get()
    {
        GameObject obj = _pool.Find(x => !x.activeSelf);

        if (obj == null)
        {
            obj = Create();
            _pool.Add(obj);
        }

        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.transform.position = dropPos;

        return obj;
    }
}
