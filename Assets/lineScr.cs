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
    Vector2 dot1Dir;
    Vector2 dot2Dir;
    [Header("Dot speed")]
    public float speed=2f;
    [Header("Colors")]
    [SerializeField] Color[] colors;
    // Set as 5 so they start  red on startup
    int dotColor1=5;
    int dotColor2=5;
    int currentColor=0;

    //TEMP
    [Header("Touch settings")]
    float startHoldTime;
    [SerializeField]float holdTime=0.25f;
    Vector2 mousePosition;
    GameObject holding=null;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        dot1= GameObject.Find("dot1");
        dot2= GameObject.Find("dot2");
        changeColor(1);
        changeColor(2);

        //TEMP
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector2.Distance(mousePosition,dot1.transform.position)<Vector2.Distance(mousePosition,dot2.transform.position))
                holding=dot1;
            else
                holding=dot2;
            startHoldTime=Time.time;
        }
        if(Input.GetMouseButton(0) && Time.time-startHoldTime>holdTime)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            holding.transform.position=mousePosition;
            //Debug.Log(Time.time-startHoldTime);
        }
        else if(Input.GetMouseButtonUp(0) && Time.time-startHoldTime<holdTime)
        {
            changeColor(int.Parse(holding.name.Substring(3)));
        }
        //CHANGE COLOR, MOVE TO ANOTHER SCRIPT
        if(Input.GetKeyDown(KeyCode.Q))
        {
            changeColor();
        }
        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightShift))
        {
            changeColor(2);
        }

        // Update dot1 position
        dot1Dir=new Vector2(Input.GetAxis("dot1H"),Input.GetAxis("dot1V")).normalized; // Get direction
        dot1.transform.Translate(dot1Dir*Time.deltaTime*speed);
        // Update dot2 position
        dot2Dir=new Vector2(Input.GetAxis("dot2H"),Input.GetAxis("dot2V")).normalized; // Get direction
        dot2.transform.Translate(dot2Dir*Time.deltaTime*speed);


        // Connect the dots with the line and check the collision between them
        playerPos[0]= dot1.transform.position;
        playerPos[1]= dot2.transform.position;
        line.SetPositions(playerPos);
        hit = Physics2D.Raycast(dot1.transform.position,dot2.transform.position-dot1.transform.position);
        //Debug.DrawLine(dot1.transform.position,dot2.transform.position,Color.cyan);
        Debug.DrawRay(dot1.transform.position,dot2.transform.position-dot1.transform.position,Color.red);
        if(hit.collider==null)
            return;
        if(hit.collider.gameObject.tag=="Enemy")
        {
            Debug.Log("LINE HIT:"+hit.collider.gameObject.name+" CURRENT COLOR:"+hit.collider.GetComponent<enemyScr>().enemyColor);
            if(hit.collider.GetComponent<enemyScr>().enemyColor==currentColor)
            {
                Destroy(hit.collider.gameObject);
                return;
            }
        }
        //Debug.Log("LINE HIT:"+hit.collider.gameObject.name);
    }


    void changeColor(int dotNumber=1)
    {
        if(dotNumber==1)
        {
            dotColor1+=2;
            if(dotColor1>colors.Length-1)
                dotColor1=0;
            dot1.transform.GetChild(0).GetComponent<SpriteRenderer>().color=colors[dotColor1];
        }
        else if(dotNumber==2)
        {
            dotColor2+=2;
            if(dotColor2>colors.Length-1)
                dotColor2=0;
            dot2.transform.GetChild(0).GetComponent<SpriteRenderer>().color=colors[dotColor2];
        }
        currentColorCheck();
    }

    void currentColorCheck()
    {
        if(dotColor1==dotColor2)
            currentColor=dotColor1;
        else if((dotColor1==4 && dotColor2==0)|| (dotColor1==0 && dotColor2==4))
            currentColor=5; //If the combination is red and yellow set it to ORANGE
        else
        {
            if(dotColor1<dotColor2)
                currentColor=dotColor1+1;
            else 
            {
                currentColor=dotColor2+1;
            }
        }
        line.startColor=colors[currentColor];
        line.endColor=colors[currentColor];
        //DODAJ lepsu tranziciju za boje
    }
}
