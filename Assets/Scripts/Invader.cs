using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    //InvaderSpawn InvaderSpawn = new InvaderSpawn();
    public SpriteRenderer SpriteRenderer;
    public System.Action killed;
    public Sprite[] Sprites = new Sprite[0];
    public int AnimationFrame;
    public float AnimationTime = 1.0f;

    public void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void Start()
    {
        InvokeRepeating(nameof(Animate), AnimationTime, AnimationTime);
    }

    private void Animate() {

        AnimationFrame++;

        if (this.AnimationFrame >= this.Sprites.Length)
        {
            AnimationFrame = 0;
        }
        SpriteRenderer.sprite = Sprites[AnimationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("laser"))
        {
            this.gameObject.SetActive(false);
            this.killed.Invoke();
            ScoreManager.current.ScoreUp();
        }
    }

}
