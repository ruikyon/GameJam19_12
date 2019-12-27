using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{ 
    [SerializeField]
    private Text height;

    private void Start()
    {
        GameManager.Instance.StartGame += () =>
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * GameManager.Instance.MvSpeed;
        };
    }

    private void Update()
    {
        height.text = ((int)transform.position.y).ToString();
        GameManager.Instance.height = (int)transform.position.y;
    }
}
