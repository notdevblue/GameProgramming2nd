using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    static public PlayerInput Instance;

    public int Delta { get; set; }
    public bool Inputed { get; set; } = false;

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if (Inputed) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            Delta = -1;
            Inputed = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Delta = 1;
            Inputed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Delta = 0;
            Inputed = true;
        }
    }
}