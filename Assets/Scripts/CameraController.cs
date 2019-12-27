using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float mvSpeed;
    [SerializeField]
    private Text height;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * mvSpeed;
    }

    private void Update()
    {
        height.text = ((int)transform.position.y).ToString();
    }
}
