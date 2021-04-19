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
    private bool IsMoveDown = false;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        if (IsMoveDown)
        {
            MoveDown();
        }
    }

    public void OnClick_Star()
    {
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
        MoveDownCount = count;
        IsMoveDown = true;
    }

    public void MoveDown()
    {
        int newRow = Row - MoveDownCount;
        if (this.transform.localPosition.y > 48 * newRow)
            this.transform.Translate(Vector3.down * speed);
        else
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, 48 * newRow, this.transform.localPosition.z);
            IsMoveDown = false;
            Row = newRow;
        }

    }
}
