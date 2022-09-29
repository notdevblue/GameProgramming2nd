using UnityEngine;

public class Car : MonoBehaviour
{
    public float originX;

    public float minspeed = 5.0f;
    public float maxspeed = 10.0f;

    private float _speed;
    private float _t = 0.0f;



    private void OnEnable()
    {
        _speed = Random.Range(minspeed, maxspeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER"))
        {
            GameManager.instance.Dead();
        }
        else if (other.CompareTag("DISABLE"))
        {
            ++GameManager.instance.Disabled;
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("RESET"))
        {
            Vector3 pos = transform.position;
            pos.x = originX;
            this.transform.position = pos;
        }
    }


    private void Update()
    {
        float x = transform.position.x + Time.deltaTime * _speed;

        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;

    }
}
