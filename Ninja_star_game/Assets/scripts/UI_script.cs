using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_script : MonoBehaviour
{
    [SerializeField] GameObject gold_bullet;
    [SerializeField] GameObject brown_bullet;
    [SerializeField] GameObject metallic_bullet;
    [SerializeField] Transform user_object;
    [SerializeField] Text metallic_text;
    [SerializeField] Text brown_text;
    [SerializeField] Text gold_text;
    [HideInInspector] public int brown_val;
    [HideInInspector] public int metallic_val;
    [HideInInspector] public int gold_val;


    private void Start()
    {
        brown_val=Int32.Parse(brown_text.text);
        gold_val=Int32.Parse(gold_text.text);
        metallic_val=Int32.Parse(metallic_text.text);

    }


    public void create_metallic_star()
    {
        if (metallic_val != 0)
        {
            GameObject instantaniated_star;
            instantaniated_star = Instantiate(metallic_bullet, transform.position, transform.rotation);
            instantaniated_star.transform.position=new Vector3(user_object.position.x, user_object.position.y, user_object.position.z+2);
            metallic_val -=1;
            metallic_text.text =metallic_val.ToString();
        }
    }
    public void create_brown_star()
    {
        if(brown_val != 0)
        {
            GameObject instantaniated_star;
            instantaniated_star = Instantiate(brown_bullet, transform.position, transform.rotation);
            instantaniated_star.transform.position=new Vector3(user_object.position.x, user_object.position.y, user_object.position.z+2);
            brown_val -=1;
            brown_text.text =brown_val.ToString();
        }
    }
    public void create_gold_star()
    {
        if(gold_val != 0)
        {
            GameObject instantaniated_star;
            instantaniated_star = Instantiate(gold_bullet, transform.position, transform.rotation);
            instantaniated_star.transform.position=new Vector3(user_object.position.x, user_object.position.y, user_object.position.z+2);
            gold_val -=1;
            gold_text.text=gold_val.ToString();
        }
    }
}
