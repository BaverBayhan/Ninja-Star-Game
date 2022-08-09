using UnityEngine;

public class loots : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    private void Update()
    {
        raycast_controller();
    }
    void raycast_controller()
    {
        int layerMask = 1<<6;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 5, layerMask))
        {
           rb.isKinematic=false;
        }
    }
}
