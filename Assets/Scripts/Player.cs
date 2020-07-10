using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;
    private float timeVal;
    private float defendTimeVal=3;
    private bool isDefend=true;
    private Vector3 bullectEulerAngles;
    
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
        //是否处于无敌状态
        if (isDefend)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal<=0)
            {
                isDefend = false;
                defendEffectPrefab.SetActive(false);
            }
        }  
    }
    private void FixedUpdate()
    {
        if (PlayerMananger.Instance.isDefeat)
        {
            return;
            
        }
        Move();
        time();
    }

    private void time()//坦克的攻击间隔
    {
        if (timeVal>=0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }
    private void Attack()//坦克的攻击
    {
  
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 子弹产生的角度：当前坦克的角度+子弹应该旋转的角度；
                Instantiate(bullectPrefad, transform.position, Quaternion.Euler(transform.eulerAngles+bullectEulerAngles));
                timeVal = 0;
            } 
        
      
        
    }
    private void Move()//坦克的移动方法
    {
        float h = Input.GetAxisRaw("Horizontal");
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
        float v = Input.GetAxisRaw("Vertical");
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
        if (isDefend)
        {
            return;
        }

        PlayerMananger.instance.isDead = true;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
}
