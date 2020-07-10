using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerMananger : MonoBehaviour
{
    public int lifeValue = 3;
    public Text playerScoreText;
    public Text playerLifeValueText;
    public int playerScore = 0;
    public GameObject isDefeatUI;
    public bool isDefeat;
    public bool isDead;
    public static PlayerMananger instance;
    public GameObject born;
    
    public static PlayerMananger Instance
    {
        get => instance;
        set => instance = value;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Recover()
    {
        if (lifeValue<=0)
        {
            //游戏失败，返回主页面
            isDefeat = true;
            Invoke("ReturnToTheMainMenu",3);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu",3);
            return;
        }
        if (isDead)
        {
            Recover();
            
        }

        playerScoreText.text = playerScore.ToString();
        playerLifeValueText.text = lifeValue.ToString();
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
