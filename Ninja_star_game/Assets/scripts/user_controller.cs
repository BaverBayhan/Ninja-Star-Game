using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class user_controller : MonoBehaviour
{
    [SerializeField] GameObject gold_bullet;
    [SerializeField] GameObject brown_bullet;
    [SerializeField] GameObject metalic_bullet;
    [SerializeField] GameObject plane_parent;
    public float speed_parameter;

    float decrease_health_parameter;
    Slider slider;

    Vector3 position_default;
    Vector3 position_left;
    Vector3 position_right;



    Animator animator;
    private int isRightTrigerredHash;
    private int isLeftTrigerredHash;
    private int isDodgedHash;
    private int isJumpedHash;
    [HideInInspector] public float time_checker = 1;

    [HideInInspector] public float health;
    Transform[] current_planes;
    bool health_flag;
    [SerializeField] Image fill;
    [SerializeField] Gradient gradient;
    [SerializeField] Text metallic_text;
    [SerializeField] Text brown_text;
    [SerializeField] Text gold_text;
    private UI_script ui_script;

    int move_user_activation_counter;

    private void Awake()
    {
        ui_script = GameObject.FindObjectOfType<UI_script>();
    }
    void Start()
    {

        move_user_activation_counter=0;
        /************************* UI ******************/
        decrease_health_parameter = 3.05f ;
        slider = GameObject.FindWithTag("GameController").GetComponent<Slider>();
        health=110;
        slider.value = health/100;
        fill.color=gradient.Evaluate(slider.value);

        /***************** POSITIONING****************/

        position_default = new Vector3(0, 3, -50);
        position_left=new Vector3(-10,3,-25);
        position_right=new Vector3(10,3,-25);
        gameObject.transform.position = position_default;

        /****************** ANIMATION ****************/
        
        animator = GetComponent<Animator>();
        isRightTrigerredHash=Animator.StringToHash("IsTrigerredRight");
        isLeftTrigerredHash=Animator.StringToHash("IsTrigerredLeft");
        isDodgedHash=Animator.StringToHash("IsDodge");
        isJumpedHash=Animator.StringToHash("IsJumped");

        /******************* MOVING USER *****************/
        move_user_activation_counter =0;
    }
    void Update()
    {

        StartCoroutine(move_user());
        increase_speed();

        /***************** MOVING ****************/
        bool pressed_RightArrow = Input.GetKeyDown(KeyCode.RightArrow);
        bool release_RightArrow = Input.GetKeyUp(KeyCode.RightArrow);
        bool pressed_LeftArrow = Input.GetKeyDown(KeyCode.LeftArrow);
        bool release_LeftArrow = Input.GetKeyUp(KeyCode.LeftArrow);
        bool pressed_downArrow=Input.GetKeyDown(KeyCode.DownArrow);
        bool release_downArrow=Input.GetKeyUp(KeyCode.DownArrow);
        bool pressed_space=Input.GetKeyDown(KeyCode.Space);
        bool release_space=Input.GetKeyUp(KeyCode.Space);
        if(pressed_space)
        {
            jump();
        }
        else if(release_space)
        {
            jump_cancel();
        }
        if(pressed_RightArrow)
        {
            move_right();
        }
        else if (release_RightArrow)
        {
            move_right_cancel();
        }
        if(pressed_LeftArrow)
        {
            move_left();
        }
        else if(release_LeftArrow)
        {
            move_left_cancel();
        }
        if(pressed_downArrow)
        {
            dodge();
        }
        else if(release_downArrow)
        {
            dodge_cancel();
        }
        /***************** SHOTING ****************/
        create_star();
    }
    private void FixedUpdate()
    {
        /*********** HEALTH CONTROLLER *******/

       user_plane_color_checker();
        if(health_flag)
        {
            health-=Time.fixedDeltaTime*decrease_health_parameter;
            update_slider();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            health-=8f;
            update_slider();
        }
        else if(collision.gameObject.tag=="heart_loot")
        {
            if (health<=80) { health+=20; }
            else { health=100; }
            Destroy(collision.gameObject);
            update_slider();
        }
        else if(collision.gameObject.tag=="brown_bullet_loot")
        {
            ui_script.brown_val+=8;
            brown_text.text=ui_script.brown_val.ToString();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag=="gold_bullet_loot")
        {
            ui_script.gold_val+=7;
            gold_text.text=ui_script.gold_val.ToString();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag=="metallic_bullet_loot")
        {
            ui_script.metallic_val+=8;
            metallic_text.text=ui_script.metallic_val.ToString();
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag=="fist")
        {
            health-=13;
            update_slider();
        }
        else if (collision.gameObject.tag=="stick")
        {
            health-=10;
            update_slider();
        }
        else if(collision.gameObject.tag=="fist_enemy" || collision.gameObject.tag=="wall" || collision.gameObject.tag=="enemy_cube")
        {
            print("carpisma oldu isim : "+collision.gameObject.tag);
            health-=6;
        }

    }
    void move_right()
    {
        if (gameObject.transform.position.x <position_right.x)
        {
            animator.SetBool(isRightTrigerredHash, true);
        }
    }
    void move_right_cancel()
    {
        animator.SetBool(isRightTrigerredHash, false);
    }
    void move_left()
    {
        if (gameObject.transform.position.x >position_left.x) 
        {
            animator.SetBool(isLeftTrigerredHash, true);
        }
    }
    void move_left_cancel()
    {
        animator.SetBool(isLeftTrigerredHash, false);
    }
    IEnumerator move_user()
    {
        if(move_user_activation_counter == 0)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            Vector3 initial_velocity = new Vector3(0, 0, 5f*Time.deltaTime);
            gameObject.transform.Translate(initial_velocity);
            move_user_activation_counter++;
        }
        else
        {
            Vector3 initial_velocity = new Vector3(0, 0, speed_parameter*Time.deltaTime);
            gameObject.transform.Translate(initial_velocity);
            move_user_activation_counter++;
            yield return null;
        }

    }
    void create_star()
    {
        GameObject instantaniated_star;
        bool Z_pressed = Input.GetKeyDown(KeyCode.Z);
        bool X_pressed= Input.GetKeyDown(KeyCode.X);
        bool C_pressed=Input.GetKeyDown(KeyCode.C);
        if (Z_pressed && ui_script.brown_val>0)
        {
            instantaniated_star = Instantiate(brown_bullet, transform.position, transform.rotation);
            instantaniated_star.transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z+2);
            ui_script.brown_val-=1;
            brown_text.text=ui_script.brown_val.ToString();
        }
        else if(X_pressed && ui_script.gold_val>0)
        {
            instantaniated_star = Instantiate(gold_bullet, transform.position, transform.rotation);
            instantaniated_star.transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z+2);
            ui_script.gold_val-=1;
            gold_text.text=ui_script.gold_val.ToString();   
        }
        else if(C_pressed && ui_script.metallic_val>0)
        {
            instantaniated_star = Instantiate(metalic_bullet,transform.position,transform.rotation);
            instantaniated_star.transform.position=new Vector3(transform.position.x, transform.position.y, transform.position.z+2);
            ui_script.metallic_val-=1;
            metallic_text.text=ui_script.metallic_val.ToString();   
        }
    }
    void user_plane_color_checker()
    {
        Vector3 gameobject_current_pos=new Vector3(transform.position.x,transform.position.y,transform.position.z);
        current_planes=plane_parent.GetComponentsInChildren<Transform>();
        Color middle_plane_color, right_plane_color, left_plane_color,current_color;
        current_color=gameObject.GetComponent<MeshRenderer>().material.color;
        middle_plane_color=current_planes[1].GetComponent<MeshRenderer>().material.color;
        right_plane_color = current_planes[2].GetComponent<MeshRenderer>().material.color;
        left_plane_color=current_planes[3].GetComponent<MeshRenderer>().material.color;

        if (gameobject_current_pos.x<-9)
        {
            if(current_color !=left_plane_color)
            {
                health_flag=true;
            }
            else if(current_color ==left_plane_color)
            {
                health_flag=false;
            }
        }
        else if(-1<gameobject_current_pos.x && gameobject_current_pos.x<1)
        {
            if (current_color !=middle_plane_color)
            {
                health_flag=true;
            }
            else if (current_color ==middle_plane_color)
            {
                health_flag=false;
            }
        }
        else if(gameobject_current_pos.x>9)
        {
            if (current_color !=right_plane_color)
            {
                health_flag=true;
            }
            else if (current_color ==right_plane_color)
            {
                health_flag=false;
            }
        }
    }
    void dodge()
    {
        animator.SetBool(isDodgedHash, true);
        gameObject.GetComponent<Collider>().isTrigger = true;
    }
    void dodge_cancel()
    {
        animator.SetBool(isDodgedHash, false);
        gameObject.GetComponent<Collider>().isTrigger = false;
    }
    void jump()
    {
        animator.SetBool(isJumpedHash, true);
    }
    void jump_cancel()
    {
        animator.SetBool(isJumpedHash, false);
    }
    void update_slider()
    {
        slider.value = health/100;
        fill.color=gradient.Evaluate(slider.value);
    }
    void increase_speed()
    {
        time_checker  +=Time.deltaTime;
        if (time_checker > 4)
        {
            time_checker=0;
            speed_parameter*=1.0473f;
            /** 1.059 :  this value doubles the speed in 60 seconds approximately **/
        }
    }
}
