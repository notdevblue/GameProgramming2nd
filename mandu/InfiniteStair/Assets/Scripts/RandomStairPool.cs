using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStairPool : MonoBehaviour
{
    private Queue<Stair> _stairQueue
        = new Queue<Stair>();

    private int _initStairCount = 50;
    private Stair _lastStair = null;
    private Stair _nextStair = null;


    public GameObject stairPrefab;

    static public RandomStairPool Instance;

    private void Awake()
    {
        Instance = this;

        Add();
    }

    private void Start()
    {
        PlayerInput.Instance.transform.position = new Vector2(_stairQueue.Peek().pos, 0);
    }

    public void Add()
    {
        for (int i = 0; i < _initStairCount; ++i)
        {
            NewStair();
        }
    }

    private void Update()
    {
        int y = 0;

        foreach (Stair stair in _stairQueue)
        {
            Vector2 randPos = new Vector2(stair.pos, y++);
            stair.transform.position = randPos;
        }
    }

    private void NewStair()
    {
        int curPos = Random.Range(-1, 2);


        if (_stairQueue.Count > 0)
            _lastStair = _stairQueue.ToArray()[_stairQueue.Count - 1];

        if (_lastStair != null)
            curPos += _lastStair.pos;

        Stair stair = Instantiate(stairPrefab).GetComponent<Stair>();
        stair.pos = curPos;
        stair.gameObject.SetActive(true);

        _stairQueue.Enqueue(stair);
    }

    public int PoolCount()
        => _stairQueue.Count;

    /// <summary>
    /// 
    /// </summary>
    /// <returns>(CurrentStair, NextStair)</returns>
    public (Stair, Stair) Get()
    {
        Stair stair;

        stair      = _stairQueue.ToArray()[0];
        _nextStair = _stairQueue.ToArray()[1];
        _stairQueue.Dequeue();

        return (stair, _nextStair);
    }
}
