using UnityEngine;
using System.Collections;

public class move_star : MonoBehaviour
{
    float death_time=0;
    [Range(-300f, 400f)]
    [SerializeField] float velocity_parameter;
    void Update()
    { 
        gameObject.transform.Translate(new Vector3(0, 0, velocity_parameter*Time.deltaTime));
        if(death_time < 2)
        {
            death_time +=Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="user" || collision.gameObject.tag=="enemy")
        {
            Destroy(gameObject);
        }
    }
}
