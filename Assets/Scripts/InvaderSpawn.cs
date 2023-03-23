using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvaderSpawn : MonoBehaviour
{
    public Invader[] Invader = new Invader[5];
    public GameObject[] walls = new GameObject[2];
    public float missleFreq;
    public int rows = 5, columns = 9, score = 0;
    public Projectile misslePrefab;
    public float numberofinvaders => (float) this.rows * this.columns;
    public float kill, perc;
    
    public AnimationCurve speed;
    
    public Vector3 direction = Vector2.right;

    public void Awake()
    {
        for (int i = 0; i < rows; i++)
        {
            Vector3 position = new Vector3(-5.5f, i * 1.0f, 0.0f);
            for (int j = 0; j < columns; j++)
            { 
                Invader invader = Instantiate(Invader[i], this.transform);
                invader.killed += killcount;
                position.x += columns * 0.1f;
                invader.transform.localPosition  = position;
            }
        }
    }

    private void killcount()
    {
        kill++;
        perc = kill / numberofinvaders;
    }

    private void Start()
    {
        InvokeRepeating(nameof(missleAttack), missleFreq, missleFreq);
    }


    private void FixedUpdate()
    {

        bool colEdge = false;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach(Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(direction == Vector3.right && invader.position.x >= walls[0].transform.position.x - 1.0f)
            {
                moveDown();
                colEdge = true;
            }
            else if (direction == Vector3.left && invader.position.x <= walls[1].transform.position.x + 1.0f)
            {
                moveDown();
                colEdge = true;
            }
        }
        if(colEdge==false)
        {
            this.transform.position += direction * this.speed.Evaluate(perc) * Time.deltaTime;

        }
    }

    private void missleAttack()
    {
        foreach(Transform invader in this.transform)
        {
        
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(Random.value < (1.0f/(float)(numberofinvaders-kill)))
            {
                Projectile projectile = Instantiate(this.misslePrefab, invader.position, Quaternion.identity);
                projectile.destroyed += missleTouch;
                break;
            }
            
        }
    }

    private void missleTouch()
    {
        
    }

    private void moveDown() {

        direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 0.5f;
        this.transform.position = position;
    }
}
