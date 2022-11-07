using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScr : MonoBehaviour
{
    [SerializeField] Color[] colors;
    [Header("Enemy settings")]
    public float baseEnemySpeed=0.3f;
    Vector2 dir; // Position -1 so it goes to the center + offset(ADD THIS)
    public int enemyColor;
    void Start()
    {
        enemyColor=Random.Range(0,6);
        GetComponent<SpriteRenderer>().color=colors[enemyColor];
        this.name="Enemy"+enemyColor;
        dir=-transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(dir*Time.deltaTime*baseEnemySpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name=="worldEdge")
        {
            Destroy(this.gameObject);
        }
    }
}
