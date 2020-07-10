using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject[] enemyPrefabList;
    public bool createPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BronTank",1f);
        Destroy(gameObject,1f);
        BornTank();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BornTank()
    {
        if (createPlayer)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
        
    }
}
