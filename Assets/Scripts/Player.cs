using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class Player : MonoBehaviour
{
    public Vector2 direction;
    private bool laseractive = false;
    public float speed;
    public System.Action killed;
    public Rigidbody2D rb;
    public Projectile PrefabProjectile;
    public TextMeshProUGUI TMPtext;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }
    private void Shoot()
    {
        if(!laseractive)
        { 
            Projectile projectile = Instantiate(PrefabProjectile, this.transform.position, Quaternion.identity);
            projectile.destroyed += laserDestroyed;
            laseractive = true;
        }
    }

    private void laserDestroyed() {
        laseractive = false;
    }

    private void FixedUpdate()
    {
        if (direction.magnitude != 0)
        {
            rb.AddForce(direction * this.speed );
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreSave score = new ScoreSave();
        if (collision.gameObject.layer == LayerMask.NameToLayer("missle")|| collision.gameObject.layer == LayerMask.NameToLayer("invader"))
        {
            //this.killed.Invoke();
            //score loader
            string json1 = File.ReadAllText(Application.dataPath + "/Save1.json");
            ScoreSave ScorePoint = JsonUtility.FromJson<ScoreSave>(json1);
            Debug.Log("readed data is " + ScorePoint.scoredata);
            //score saver
            score.scoredata = int.Parse(TMPtext.text);
            if(ScorePoint.scoredata < score.scoredata)
            {
                string json2 = JsonUtility.ToJson(score);
                Debug.Log(score.scoredata);
                File.WriteAllText(Application.dataPath + "/Save1.json", json2);
                Debug.Log(Application.dataPath);
            }
            SceneManager.LoadScene(1);
            this.gameObject.SetActive(false);
        }
    }
}
