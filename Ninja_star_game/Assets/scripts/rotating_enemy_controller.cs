using UnityEngine;

public class rotating_enemy_controller : MonoBehaviour
{
    [SerializeField] int speed_parameter;
    void Update()
    {
        transform.Rotate(new Vector3(0,1,0) * speed_parameter * Time.deltaTime);
    }
}
