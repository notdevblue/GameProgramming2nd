using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public GameObject gameOverPanel;
    public Text text;
    public int score = 0;
    public int Disabled
    {
        get => _disabled;

        set
        {
            _disabled = value;
            text.text = $"{++score}";
            if (_disabled >= newSetCount)
            {
                CarPool.instance.AddSet(newSetCount);
                _disabled = 0;
            }
        }
    }

    private int _disabled;

    public int newSetCount = 10;

    private int _idx = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Dead()
    {
        Time.timeScale = 0.0f;
        gameOverPanel.SetActive(true);
    }
}