using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//星星颜色
public enum Color { Blue, Green, Orange, Purple, Red }

public class Star : MonoBehaviour {

    public int Row = 0;
    public int Column = 0;
    public Color StarColor = Color.Blue;
    private float speed = 5.0f;
    private bool IsMoveDown = false;
    private bool IsMoveLeft = false;


    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        if (IsMoveDown)
        {
            MoveDown();
        }
        if (IsMoveLeft)
        {
            MoveLeft();
        }
    }

    public void OnClick_Star()
    {
        //星星移动时不响应点击
        if (IsMoveDown || IsMoveLeft)
            return;
        GameManager.gameManager_Instance.FindTheSameStar(this);
        GameManager.gameManager_Instance.ClearClickedList();
        GameManager.gameManager_Instance.JudgeOver();
    }

    public void DestroyStar()//销毁自己
    {
        Destroy(this.gameObject);
    }

    public void OpenMoveDown(int count)//向下移动的开关
    {
        Row -= count;
        IsMoveDown = true;
    }
    
    public void OpenMoveLeft(int count)//向左移动的开关
    {
        Column -= count;
        IsMoveLeft = true;
    }

    public void MoveDown()
    {
        if (this.transform.localPosition.y > 48 * Row)
            this.transform.Translate(Vector3.down * speed);
        else
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 48 * Row, this.transform.localPosition.z);
            IsMoveDown = false;
        }
    }
    public void MoveLeft()
    {
        if (this.transform.localPosition.x > 48 * Column)
            this.transform.Translate(Vector3.left * speed);
        else
        {
            this.transform.localPosition = new Vector3(48 * Column,this.transform.localPosition.y, this.transform.localPosition.z);
            IsMoveLeft = false;
        }
    }
}
