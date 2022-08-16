using UnityEngine;

public class instantiate_enemy : MonoBehaviour
{
    [SerializeField] Transform user_transform;
    [SerializeField] GameObject enemy1;
    private float[] x_pos=new float[3] {-10.5f,0,10.5f};
    private int[] x_pos_for_rotating_enemy = new int[3] { -15, -4, 6 };
    [SerializeField] GameObject[] enemy_list;
    float checker;
    int i;
    int offset;
    void Start()
    {
        checker=100;
    }
    void Update()
    {
        if (user_transform.position.z>checker-80 || user_transform.position.z>checker-60 || user_transform.position.z >checker/1.25)
        {
            for (i=0;i<6;i++)
            {
                offset=65*i;
                GameObject new_enemy = Instantiate(enemy_list[(int)Random.Range(0, 5)]);
                if(new_enemy.tag=="enemy_cube")
                {
                    new_enemy.transform.position=new Vector3(x_pos[(int)Random.Range(0, 3)], 3, checker+offset);
                }
                else if(new_enemy.tag=="wall")
                {
                    new_enemy.transform.position=new Vector3(x_pos[(int)Random.Range(0, 3)], 1.75f, checker+offset+1);
                }
                else if(new_enemy.tag=="fist_enemy")
                {
                    if (new_enemy.name=="fist_enemy_left(Clone)")
                    {
                        new_enemy.transform.position=new Vector3(x_pos[(int)Random.Range(1, 3)], 2, checker+offset);
                    }
                    else if(new_enemy.name=="fist_enemy(Clone)")
                    {
                        new_enemy.transform.position=new Vector3(x_pos[(int)Random.Range(0, 2)], 2, checker+offset);
                    }
                }
                else if (new_enemy.tag=="rotating_enemy")
                {
                    new_enemy.transform.position=new Vector3(x_pos_for_rotating_enemy[(int)Random.Range(0, 3)], 2.5f, checker+offset);
                }

            }
             checker+=offset*1.25f;
        }
    }
}
