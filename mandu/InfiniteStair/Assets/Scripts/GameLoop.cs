using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    float lastInputTime = Mathf.Infinity;
    float wait = 30.0f;
    float debug = 0.0f;

    public Slider slider;

    private void Update()
    {
        debug += Time.deltaTime;

        if (lastInputTime + wait < Time.time)
            Died();
        else
        {
            slider.maxValue = wait;
            slider.value = wait - debug;
        }

        if (PlayerInput.Instance.Inputed)
            NewLoop();
    }


    public void NewLoop()
    {
        // cur, next
        (Stair,Stair) stair = RandomStairPool.Instance.Get();
        int delta = PlayerInput.Instance.Delta;

        stair.Item1.gameObject.SetActive(false);

        Debug.Log(delta + " DELTA");
        Debug.Log(stair.Item1.pos + " ITEM1");
        Debug.Log(stair.Item2.pos + " ITEM2");

        if (stair.Item1.pos + delta != stair.Item2.pos)
        {
            Died();
            return;
        }

        if (RandomStairPool.Instance.PoolCount() < 25)
            RandomStairPool.Instance.Add();

        PlayerInput.Instance.transform.position = new Vector2(stair.Item2.pos, 0);

        lastInputTime = Time.time;
        wait *= 0.99f;
        debug = 0.0f;
        PlayerInput.Instance.Inputed = false;
    }


    public void Died()
    {
        Debug.LogWarning("Died!");
        PlayerInput.Instance.Inputed = false;
        gameObject.SetActive(false);
    }


}