using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private readonly int MAX_ENERGY = 1000;
    private readonly int DEC_ENERGY = 1;

    [SerializeField]
    private float slideSpeed;
    [SerializeField]
    private Slider energy_bar;
        
    private Rigidbody2D rb;
    private int energy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        energy = MAX_ENERGY;
        GameManager.Instance.StartGame += () =>
        {
            rb.velocity = Vector2.up * GameManager.Instance.MvSpeed;
            GetComponentInChildren<Animator>().SetTrigger("Fly");
            //GetComponent<Animator>().SetTrigger("Fly");
        };
    }

    private void Update()
    {
        energy_bar.value = (float) energy / MAX_ENERGY;
        if (energy <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.StartFlag) return;
        var rate = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;

        energy -= DEC_ENERGY * rate;
        var x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * slideSpeed, GameManager.Instance.MvSpeed) * rate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            energy += collision.GetComponent<Food>().Value;
            energy = energy > MAX_ENERGY ? MAX_ENERGY : energy;
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Finish")
        {
            GameManager.Instance.GameOver();
        }
    }
}
