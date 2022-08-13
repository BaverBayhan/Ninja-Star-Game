using UnityEngine;
public class change_colors : MonoBehaviour
{
    [SerializeField] GameObject plane_blue;
    [SerializeField] GameObject plane_red;
    [SerializeField] GameObject plane_gray;
    [SerializeField] GameObject plane_green;
    [SerializeField] GameObject plane_purple;
    [SerializeField] GameObject user;
    GameObject[] plane_array;
    int[] random_index;

    Color[] color_Arr;
    float change_time;

    user_controller uc;

    void Start()
    {
        random_index = new int[3];
        plane_array=new GameObject[5] { plane_blue,  plane_red, plane_gray, plane_green, plane_purple };
        plane_color_changer();
        uc=GameObject.FindObjectOfType<user_controller>();

    }
    private void Update()
    {
        move_planes();
    }
    void FixedUpdate()
    {
        if (change_time>3)
        {
            clear_clones();
            plane_color_changer();
            user_color_changer();
            change_time=0;
        }
        else
        {
            change_time+=Time.fixedDeltaTime;
        }
    }
    void plane_color_changer()
    {
        GameObject instantiated_random_platform;
        GameObject main_random_platform;
        int rand_num1,rand_num2,rand_num3;
        color_Arr = new Color[3];
        rand_num1=Random.Range(0, 4);
        while(true)
        {
            rand_num2=Random.Range(0, 4);
            if(rand_num1 !=rand_num2)
            {
                break;
            }
        }
        while(true)
        {
            rand_num3 = Random.Range(0, 4);
            if(rand_num3 != rand_num2 && rand_num3 != rand_num1)
            {
                break;
            }
        }
        random_index=new int[3] { rand_num1, rand_num2, rand_num3 };
        for(int i=0;i<random_index.Length;i++)
        {
            main_random_platform=plane_array[random_index[i]];
            instantiated_random_platform=Instantiate(main_random_platform);
            color_Arr[i]=instantiated_random_platform.GetComponent<MeshRenderer>().material.color;
            instantiated_random_platform.transform.parent=gameObject.transform;
            switch(i)
            {
                case 0:
                    instantiated_random_platform.transform.position=new Vector3(0, 0, gameObject.transform.position.z);
                    break;
                case 1:
                    instantiated_random_platform.transform.position=new Vector3(11f, 0, gameObject.transform.position.z);
                    break;
                case 2:
                    instantiated_random_platform.transform.position=new Vector3(-11f, 0, gameObject.transform.position.z);
                    break;
            }
        }
    }
    void clear_clones()
    {
        int i = 0;
        GameObject[] allclones=new GameObject[3];
        foreach(Transform clones in transform)
        {
            allclones[i]=clones.gameObject;
            i++;
        }
        foreach(GameObject clone in allclones)
        {
            DestroyImmediate(clone.gameObject);
        }
    } 
    void user_color_changer()
    {
        int rand_num=Random.Range(0, 3);
        user.GetComponent<MeshRenderer>().material.color=color_Arr[rand_num];
    }
    void move_planes()
    {
        Vector3 initial_velocity = new Vector3(0, 0, uc.speed_parameter*Time.deltaTime);
        gameObject.transform.Translate(initial_velocity);
    }
    
}
