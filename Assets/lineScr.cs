using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineScr : MonoBehaviour
{
    LineRenderer line;
    Vector3[] playerPos=new Vector3[2];
    GameObject dot1;
    GameObject dot2;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        dot1= GameObject.Find("dot1");
        dot2= GameObject.Find("dot2");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos[0]= dot1.transform.position;
        playerPos[1]= dot2.transform.position;
        line.SetPositions(playerPos);
        hit = Physics2D.Raycast(dot1.transform.position,dot2.transform.position-dot1.transform.position);
        //Debug.DrawLine(dot1.transform.position,dot2.transform.position,Color.cyan);
        Debug.DrawRay(dot1.transform.position,dot2.transform.position-dot1.transform.position,Color.red);
        if(hit.collider==null)
            return;
        Debug.Log(hit.collider.gameObject);
    }
}
