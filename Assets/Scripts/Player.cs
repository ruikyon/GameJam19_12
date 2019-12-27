using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private readonly int MAX_ENERGY = 1000;
    private readonly int DEC_ENERGY = 1;

    [SerializeField]
    private float mvSpeed, slideSpeed;
    [SerializeField]
    private Slider energy_bar;
        
    private Rigidbody2D rb;
    private int energy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * mvSpeed;
        energy = MAX_ENERGY;
    }

    private void Update()
    {
        energy_bar.value = (float) energy / MAX_ENERGY;
        if (energy <= 0)
        {
            //game over
        }
    }

    private void FixedUpdate()
    {
        energy -= DEC_ENERGY;
        var x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * slideSpeed, mvSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            energy += collision.GetComponent<Food>().Value;
            Destroy(collision.gameObject);
        }
    }
}
