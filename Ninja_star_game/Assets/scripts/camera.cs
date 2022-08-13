using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [Range(0f, 50f)]
    [SerializeField] float smooth_factor;
    private float time_checker;

    private void Update()
    {
        smooth_factor_updater();
    }

    private void LateUpdate()
    {
        camera_follow();
    }
    private void camera_follow()
    { 
        Vector3 desired_position= target.position+offset;
        Vector3 smoothed_position=Vector3.Lerp(transform.position, desired_position, smooth_factor*Time.deltaTime);
        gameObject.transform.position = smoothed_position;
    }
    private void smooth_factor_updater()
    {
        time_checker += Time.deltaTime;
        if(time_checker >4) { time_checker=0; smooth_factor*=1.0473f; }
    }
}
