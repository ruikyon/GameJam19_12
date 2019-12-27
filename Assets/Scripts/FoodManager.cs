using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance { get; private set; }

    [SerializeField]
    private Food[] foods;
    private List<Food> activeFoods;
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
        activeFoods = new List<Food>();
        activeFoods.Add(foods[0]);

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

    public void LevelUp()
    {
        level++;
    }

    private void SetObject()
    {
        var parent = new GameObject("tmp");
        Scheduler.AddEvent(15, () => Destroy(parent));

        for(int i = 0; i < 20; i++)
        {
            if (i % 4 == 3)
            {
                //障害物配置
                var obstacles = Instantiate(obstaclePrefab, parent.transform);
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
                    var tmp = Instantiate(activeFoods[Random.Range(0, activeFoods.Count)], parent.transform);
                    tmp.transform.position = new Vector2(pos, nextLine + i);
                }
            }
        }
        nextLine += 20;
        //Debug.Log(nextLine%200);
        if((nextLine-10)%20 == 0)
        {
            level++;
            switch (level)
            {
                case 1:
                    obstacleCount++;
                    break;
                case 2:
                    foodCount--;
                    break;
                case 3:
                    obstacleCount++;
                    foodCount++;
                    activeFoods[0] = foods[1];
                    activeFoods.Add(foods[2]);
                    break;
                case 5:
                    activeFoods[1] = foods[0];
                    break;
                case 7:
                    activeFoods.RemoveAt(1);
                    break;
                case 10:
                    activeFoods[0] = foods[0];
                    break;
            }
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
