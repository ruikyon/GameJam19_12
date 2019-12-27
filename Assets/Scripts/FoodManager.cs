using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance { get; private set; }

    [SerializeField]
    private Food[] foods;
    [SerializeField]
    private GameObject obstaclePrefab;
    private int level, nextLine, foodCount, obstacleCount;
    private List<int> foodPos, obstaclePos;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        level = 0;
        nextLine = 10;
        foodCount = 2;
        obstacleCount = 1;

        foodPos = new List<int>();
        for (int i = 0; i < 15; i++)
            foodPos.Add(i-7);

        obstaclePos = new List<int>();
        for (int i = 0; i < 5; i++)
            obstaclePos.Add(i);

        GameManager.Instance.StartGame += () =>
        {
            SetObject();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        level++;
    }

    private void SetObject()
    {
        for(int i = 0; i < 20; i++)
        {
            if (i % 4 == 3)
            {
                //障害物配置
                var obstacles = Instantiate(obstaclePrefab);
                obstacles.transform.position = Vector2.up * (nextLine + i);
                
                var pos = Choice(obstaclePos, obstacleCount);
                foreach(int idx in pos)
                {
                    obstacles.transform.GetChild(idx).gameObject.SetActive(true);
                }
            }
            else
            {
                //食べ物配置
                var positions = Choice(foodPos, foodCount);
                foreach(int pos in positions)
                {
                    var tmp = Instantiate(foods[0]);
                    tmp.transform.position = new Vector2(pos, nextLine + i);
                }
            }
        }
        nextLine += 20;
        //Debug.Log(nextLine%200);
        if((nextLine-10)%40 == 0)
        {        
            obstacleCount++;
        }

        Scheduler.AddEvent(5, SetObject);
    }

    private List<int> Choice(List<int> original, int pick)
    {
        var res = new List<int>();
        original = new List<int>(original);

        for(int i = 0; i < pick; i++)
        {
            var idx = Random.Range(0, original.Count);
            res.Add(original[idx]);
            original.RemoveAt(idx);
        }
        return res;
    }
}
