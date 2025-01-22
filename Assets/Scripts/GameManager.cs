using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public GameObject[] map;
    public GameObject player;
    public GameObject item;

    public GameObject start;
    public GameObject end;
    
    public GameObject instance;
    
    public TMP_Text gasText;
    
    private int gas = 100;

    private float moveSpeed = 10f;
    
    
    private void Awake()
    {
        Time.timeScale = 0;
        
        
    }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(CreateItem());
        StartCoroutine(MovaMap());
        StartCoroutine(MoveItem());
        StartCoroutine(Gas());
        gasText.text = gas.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (instance != null)
        {
            if (player.transform.position.x+0.5f>=instance.transform.position.x&&player.transform.position.x-0.5f<=instance.transform.position.x
                                                                               &&player.transform.position.y+0.5f>=instance.transform.position.y&&player.transform.position.y-0.5f<=instance.transform.position.y)
            {
                Debug.Log("가스 획득");
                gas += 30;
                gasText.text = gas.ToString();
                Destroy(instance.gameObject);
                instance = null;
                Debug.Log(gas.ToString());
                
            }
        }

        if (gas <= 0)
        {
            Time.timeScale = 0;
            end.SetActive(true);
            
            //액티브 패널, 시간 멈춤
            
        }

        if (instance!=null&&instance.transform.position.y <= -4)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }

    IEnumerator MovaMap()
    {
        while (true)
        {
            for (int i = 0; i < map.Length; i++)
            {
                float newY = Mathf.MoveTowards(map[i].transform.position.y, -30, moveSpeed * Time.deltaTime);
                map[i].transform.position = new Vector3(map[i].transform.position.x, newY, map[i].transform.position.z);
                
                
                if (map[i].transform.position.y < -15)
                {
                    map[i].transform.position = new Vector3(map[i].transform.position.x, 20, map[i].transform.position.z);
                    
                }
            }
            yield return null;
        }
           
    }

    IEnumerator MoveItem()
    {
        while (true)
        {
            if (instance != null)
            {
                float newY = Mathf.MoveTowards(instance.transform.position.y, -5, moveSpeed*0.5f* Time.deltaTime);
                instance.transform.position = new Vector3(instance.transform.position.x, newY, instance.transform.position.z);
                
            }
            
            yield return null;
        }
        
    }

    IEnumerator Gas()
    {
        while (true)
        {
            gas -= 10;
            gasText.text = gas.ToString();
            yield return new WaitForSeconds(1f);
            
        }
        
    }
    
    
    
    
    IEnumerator CreateItem()
    {
        while (true)
        {
            if (instance == null)
            {
                Debug.Log("아이템 생성");
                int[] options = { -2, 0, 2 };
                int randomValue = Random.Range(0, options.Length);
                GameObject newitem = Instantiate(item,new Vector3(options[randomValue],7,0),Quaternion.identity);
                instance = newitem;
                yield return new WaitForSeconds(3f);
            }

            yield return null;
        }
        
    }

    public void LeftClick()
    {
        if (player.transform.position.x > -2)
        {
            player.transform.position+= Vector3.left*2;
                
        }
        
    }

    public void RightClick()
    {
        if (player.transform.position.x < 2)
        {
            player.transform.position+= Vector3.right*2;
                
        }
        
    }

    public void GameStart()
    {
        start.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameEnd()
    {
        SceneManager.LoadScene("GameScene");
        
    }
    
}
