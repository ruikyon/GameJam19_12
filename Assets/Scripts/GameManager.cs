using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private float mvSpeed;
    [SerializeField]
    private GameObject startUI, endUI;
    [SerializeField]
    private Text scoreText;
    public float MvSpeed { get => mvSpeed; }
    public Action StartGame;
    public bool StartFlag { get; private set; }
    public int height;

    private void Awake()
    {
        Time.timeScale = 1;
        Instance = this;
        StartFlag = false;
        StartGame += () =>
        {
            startUI.SetActive(false);
        };
    }

    private void Update()
    {
        if(!StartFlag && Input.GetKeyDown(KeyCode.Space))
        {
            StartFlag = true;
            StartGame();
        }
    }

    public void GameOver()
    {
        Debug.Log("game over!");
        endUI.SetActive(true);
        GetComponent<SceneChanger>().enabled = true;
        scoreText.text = "Score: " + height.ToString() + "m";
        Time.timeScale = 0;
    }
}
