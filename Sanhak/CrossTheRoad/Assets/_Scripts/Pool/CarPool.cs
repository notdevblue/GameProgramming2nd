using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPool : MonoBehaviour
{
    static public CarPool instance;

    public GameObject carPrefab;

    private Queue<Car> _pool = new Queue<Car>();

    private int _instCount = 2;


    private int initalPoolCount = 20;

    private void Awake()
    {
        instance = this;
        AddSet(initalPoolCount);
    }

    public void AddSet(int count)
    {
        for (int i = 0; i < count; ++i)
            Add();
    }


    private Car Add()
    {
        GameObject inst = Instantiate(carPrefab, this.transform);
        Car car = inst.GetComponent<Car>();

        _pool.Enqueue(car);

        inst.transform.position = new Vector3(0.0f, 0.0f, ++_instCount);

        float res = Random.Range(0.0f, 1.0f);

        if (res > 0.25f)
            ++_instCount;

        return car;
    }

    public Car Get()
    {
        Car car = _pool.Peek();

        if (car.gameObject.activeSelf)
            car = Add();
        else
        {
            car = _pool.Dequeue();
            car.gameObject.SetActive(true);
            _pool.Enqueue(car);
        }


        return car;
    }

    public Car[] GetArray()
    {
        return _pool.ToArray();
    }




    
}
