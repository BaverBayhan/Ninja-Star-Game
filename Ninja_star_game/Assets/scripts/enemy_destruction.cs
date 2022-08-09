using UnityEngine;

public class enemy_destruction : MonoBehaviour
{
    GameObject user;
    void Start()
    {
        user=GameObject.FindGameObjectWithTag("user");
    }
    void Update()
    {
        if(user.transform.position.z>gameObject.transform.position.z+40)
        {
            Destroy(gameObject);
        }
    }
}
