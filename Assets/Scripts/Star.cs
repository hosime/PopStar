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
    private int MoveDownCount = 0;
    private int MoveLeftCount = 0;
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
        Debug.Log(StarColor.ToString());
        GameManager.gameManager_Instance.FindTheSameStar(this);
        GameManager.gameManager_Instance.ClearClickedList();
    }

    public void DestroyStar()//销毁自己
    {
        Destroy(this.gameObject);
    }

    public void OpenMoveDown(int count)//向下移动的开关
    {
        MoveDownCount += count;//处理星星移动过程中下方星星又被消去的情况
        IsMoveDown = true;
    }
    
    public void OpenMoveLeft(int count)//向左移动的开关
    {
        MoveLeftCount += count;
        IsMoveLeft = true;
    }

    public void MoveDown()
    {
        int newRow = Row - MoveDownCount;
        if (this.transform.localPosition.y > 48 * newRow)
            this.transform.Translate(Vector3.down * speed);
        else
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 48 * newRow, this.transform.localPosition.z);
            MoveDownCount = 0;
            IsMoveDown = false;
            Row = newRow;
        }
    }
    public void MoveLeft()
    {
        int newCol = Column - MoveLeftCount;
        if (this.transform.localPosition.x > 48 * newCol)
            this.transform.Translate(Vector3.left * speed);
        else
        {
            this.transform.localPosition = new Vector3(48 * newCol,this.transform.localPosition.y, this.transform.localPosition.z);
            MoveLeftCount = 0;
            IsMoveLeft = false;
            Column = newCol;
        }
    }
}
