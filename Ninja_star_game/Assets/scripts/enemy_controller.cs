using UnityEngine;
using System.Collections;
public class enemy_controller: MonoBehaviour
{
    Animator animator;
    private int enemy_health;
    int damage_of_same_color_bullet;
    int damage_of_different_color_bullet;
    int IsDestroyedHash;
    private int isRightTrigerredHash;
    private int isLeftTrigerredHash;
    private int left_bool_flag;
    private int right_bool_flag;
    private int i=0;
    [SerializeField,HideInInspector] GameObject User;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject health_loot;
    [SerializeField] GameObject brown_bullet_loot;
    [SerializeField] GameObject gold_bullet_loot;
    [SerializeField] GameObject metallic_bullet_loot;
    [SerializeField] Vector3 offset;
    GameObject[] loot_list;
    private float global_shoot_interval;

    private void Awake()
    {
        User=GameObject.FindGameObjectWithTag("user");
    }
    void Start()
    {
        enemy_health = 100;
        damage_of_same_color_bullet = ((int)enemy_health/3)+1;
        damage_of_different_color_bullet=enemy_health/5;
        /********* ENEMY ANIMATION *********/
        animator = GetComponent<Animator>();
        IsDestroyedHash=Animator.StringToHash("IsDestroyed");
        isLeftTrigerredHash=Animator.StringToHash("IsLeft");
        isRightTrigerredHash=Animator.StringToHash("IsRight");
        /*********** LOOT LIST CREATION *****************/
        loot_list=new GameObject[] { health_loot, brown_bullet_loot, gold_bullet_loot, metallic_bullet_loot };
    }
    private void FixedUpdate()
    {
        track_user_x_position();
        raycast_controller();
    }
    private void LateUpdate()
    {
        back_to_default();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Color bullet_color = collision.gameObject.GetComponent<MeshRenderer>().material.color;
        Destroy(collision.gameObject);
        if (gameObject.GetComponent<MeshRenderer>().material.color ==bullet_color)
        {
            enemy_health-=damage_of_same_color_bullet;
            if(enemy_health <= 0)
            {
                animator.SetBool(IsDestroyedHash, true);
                StartCoroutine(destroy_GameObject());
            }

        }
        else if(gameObject.GetComponent<MeshRenderer>().material.color != bullet_color)
        {
            enemy_health-=damage_of_different_color_bullet;
            if(enemy_health <= 0)
            {
                animator.SetBool(IsDestroyedHash, true);
                StartCoroutine(destroy_GameObject());
            }
        }

    }
    IEnumerator destroy_GameObject()
    {
        yield return new WaitForSecondsRealtime(0.15f);
        animator.SetBool(IsDestroyedHash, false);
        int rand_num = Random.Range(0, 4);
        Instantiate(loot_list[rand_num], transform.position, loot_list[rand_num].transform.rotation);
        Destroy(gameObject);

    }
    void track_user_x_position()
    {
        Vector3 current_position_of_user = User.transform.position;
        if (current_position_of_user.x < -9)
        {
            if(!(gameObject.transform.position.x<-9))
            {
                to_left();
            }
        }
        else if(current_position_of_user.x>9)
        {
            if (!(gameObject.transform.position.x>9))
            {
                to_right();
            }
        }
        else if(-1<current_position_of_user.x&&current_position_of_user.x<1)
        {
            if (gameObject.transform.position.x>9)
            {
                to_left();
            }
            else if(gameObject.transform.position.x<-9)
            {
                to_right();
            }
        }
    }
    void to_left()
    {
        animator.SetBool(isLeftTrigerredHash, true);
        left_bool_flag=1;
    }
    void to_right()
    {
        animator.SetBool(isRightTrigerredHash, true);
        right_bool_flag=1;
    }
    void back_to_default()
    {
        if (left_bool_flag==1) { animator.SetBool(isLeftTrigerredHash, false); left_bool_flag=0; }
        if (right_bool_flag==1) { animator.SetBool(isRightTrigerredHash, false); right_bool_flag=0; }
    }
    void raycast_controller()
    {
        int layerMask = 1<<6;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 170, layerMask))
        {
            if(global_shoot_interval>=1){create_star();}
            else { global_shoot_interval+=Time.fixedDeltaTime; }
        }
    }
    void create_star()
    {
        GameObject produced_object = Instantiate(bullet,transform.position+offset, transform.rotation);
        global_shoot_interval=0;
    }
    
}
