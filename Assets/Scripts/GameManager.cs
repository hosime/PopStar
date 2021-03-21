using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int MaxRow = 12;
    public int MaxColumn = 10;
    public GameObject[] StarObjs;
    public GameObject StarGroup;
    public List<Star> StarList;//所有星星
    public List<Star> ClickedList;//被点击的星星和它相邻的同色星星

    public static GameManager gameManager_Instance;

    // Use this for initialization
    void Start () {
        gameManager_Instance = this;
        CreateStarList();
    }
    
    // Update is called once per frame
    void Update () {
        
    }


    void CreateStarList()
    {
        for (int r = 0; r < MaxRow; r++)
        {
            for (int c = 0; c < MaxColumn; c++)
            {
                int index = Random.Range(0, 5);//获取随机颜色星星
                var obj = Instantiate(StarObjs[index], StarGroup.transform);//实例化星星
                Vector3 pos = new Vector3(48 * c, 48 * r, 0);//设置星星位置，行对应y，列对应x
                obj.transform.localPosition = pos;
                var star = obj.GetComponent<Star>();//获取星星的Star（脚本）组件
                star.Row = r;//记录星星行列
                star.Column = c;
                StarList.Add(star);
            }
        }
    }

    public void FindTheSameStar(Star currentStar)
    {
        int row = currentStar.Row;
        int column = currentStar.Column;
        if (row < MaxRow)
        {
            foreach (var item in StarList)
            {
                if (item.Row == row + 1 && item.Column == column)//判断上方
                {
                    if (item.StarColor == currentStar.StarColor)
                    {
                        if (!ClickedList.Contains(item))
                        {
                            ClickedList.Add(item);
                            FindTheSameStar(item);
                        }
                    }
                }
            }
        }
        if (row > 0)
        {
            foreach (var item in StarList)
            {
                if (item.Row == row - 1 && item.Column == column)//判断下方
                {
                    if (item.StarColor == currentStar.StarColor)
                    {
                        if (!ClickedList.Contains(item))
                        {
                            ClickedList.Add(item);
                            FindTheSameStar(item);
                        }
                    }
                }
            }
        }
        if (column > 0)
        {
            foreach (var item in StarList)
            {
                if (item.Row == row && item.Column == column - 1)//判断左边
                {
                    if (item.StarColor == currentStar.StarColor)
                    {
                        if (!ClickedList.Contains(item))
                        {
                            ClickedList.Add(item);
                            FindTheSameStar(item);
                        }
                    }
                }
            }
        }
        if (column < MaxColumn)
        {
            foreach (var item in StarList)
            {
                if (item.Row == row && item.Column == column + 1)//判断右边
                {
                    if (item.StarColor == currentStar.StarColor)
                    {
                        if (!ClickedList.Contains(item))
                        {
                            ClickedList.Add(item);
                            FindTheSameStar(item);
                        }
                    }
                }
            }
        }
    }

    public void ClearClickedList()
    {
        foreach (var item in ClickedList)
        {
            item.DestroyStar();
            StarList.Remove(item);
        }
        ClickedList.Clear();
    }
}
