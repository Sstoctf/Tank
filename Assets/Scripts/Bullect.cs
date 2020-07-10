using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullect : MonoBehaviour
{
    public float moveSpeed = 8;

    public bool isPlayBullect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up*moveSpeed*Time.deltaTime,Space.World);
        
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        switch (collider2D.tag)
        {
            case "Tank" :
                if (!isPlayBullect)
                {
                    collider2D.SendMessage("Die");
                    Destroy(gameObject);
                }
                
                break;
            case "Heart" :
                collider2D.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy" :
                if (isPlayBullect)
                {
                    collider2D.SendMessage("Die");
                    Destroy(gameObject);
                }
                
                break;
            case "Wall" :
                Destroy(collider2D.gameObject);
                Destroy(gameObject);
                break;
            case "Barriar":
                collider2D.SendMessage("PlayAudio");
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
