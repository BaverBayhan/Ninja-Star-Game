using UnityEngine;

public class fist_enemy_collision_controller : MonoBehaviour
{
    private int rigid_body_controller=0;
    void FixedUpdate()
    {
        RayController();
    }
    void RayController()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 300))
        {
            if(hit.distance <3)
            {
                if(gameObject.GetComponent<Rigidbody>() == null)
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.mass =100;
                rb.angularDrag = 0;
                rb.constraints=RigidbodyConstraints.FreezePosition;
            }
        }
    }
}
