using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
  //属性值
    public float moveSpeed = 3;
    private float timeVal;
    // private float defendTimeVal=3;
    private Vector3 bullectEulerAngles;
    private float v;
    private float h;
    private float timeValchangeDirection;
    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprites;//上 右 下 左
    public GameObject explosionPrefab;
    public GameObject bullectPrefad;
    public GameObject defendEffectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    private void FixedUpdate()
    {
        Move();
        time();
    }

    private void time()//坦克的攻击间隔
    {
        if (timeVal>=3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    private void Attack()//坦克的攻击 时间间隔
    {
  
            
                // 子弹产生的角度：当前坦克的角度+子弹应该旋转的角度；
                Instantiate(bullectPrefad, transform.position, Quaternion.Euler(transform.eulerAngles+bullectEulerAngles));
                timeVal = 0;
           
        
      
        
    }
    private void Move()//坦克的移动方法
    {
        if (timeValchangeDirection>=4)
        {
            int num = Random.Range(0, 8);
            if (num>5)
            {
                v = -1;
                h = 0;
            }
            else if (num==0)
            {
                v = 1;
                h = 0;
            }
            else if (num>0 && num<=2)
            {
                h = -1;
                v = 0;
            }
            else if (num>2 && num<=4)
            {
                h = 1;
                v = 0;
            }

            timeValchangeDirection = 0;
        }
        else
        {
            timeValchangeDirection += Time.fixedTime;
        }
         // h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right*h*moveSpeed*Time.deltaTime,Space.World);
        if (h<0)
        {
            sr.sprite = tankSprites[3];
            bullectEulerAngles=new Vector3(0,0,90);
        }
        else if (h>0)
        {
            sr.sprite = tankSprites[1];
            bullectEulerAngles=new Vector3(0,0,-90);
        }
        if (h!=0)
        {
            return;
        }
         // v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up*v*moveSpeed*Time.deltaTime,Space.World);
        if (v<0)
        {
            sr.sprite = tankSprites[2];
            bullectEulerAngles=new Vector3(0,0,-180);
        }
        else if (v>0)
        {
            sr.sprite = tankSprites[0];
            
            bullectEulerAngles=new Vector3(0,0,0);
        } 
    }

    private void Die()//坦克的死亡方法
    {
        PlayerMananger.Instance.playerScore++;
        
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            timeValchangeDirection = 4;
        }
    }
}
