using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int MaxRow = 12;
    public int MaxColumn = 10;
    public int CurrentScore = 0;
    public int TargetScore = 1000;
    public int HurdleIndex = 1;
    public GameObject[] StarObjs;
    public GameObject StarGroup;
    public List<Star> StarList;//所有星星
    public List<Star> ClickedList;//被点击的星星和它相邻的同色星星

    public Text CurrentScoreText;
    public Text TargetScoreText;
    public Text HurdleIndexText;

    public Button RestartButton;

    public static GameManager gameManager_Instance;

    // Use this for initialization
    void Start () {
        gameManager_Instance = this;
        RestartButton.gameObject.SetActive(false);
        CreateStarList();
    }
    
    // Update is called once per frame
    void Update () {

    }

    void GoToNext()
    {
        CreateStarList();
        CurrentScoreText.text = "当前得分：" + CurrentScore.ToString();
        HurdleIndex++;
        HurdleIndexText.text = "当前关卡：" + HurdleIndex.ToString();
        SetTargetScore();
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
        CalculateScore();
        //销毁星星
        foreach (var item in ClickedList)
        {
            item.DestroyStar();
            StarList.Remove(item);
        }
        //向下移动剩余星星
        foreach (var rest in StarList)
        {
            int moveCount = 0;//需要移动的行数
            foreach (var clicked in ClickedList)
            {
                if (rest.Column == clicked.Column && rest.Row > clicked.Row)//统计同一列在下面消失的星星
                {
                    moveCount++;
                }
            }
            if (moveCount > 0)
                rest.OpenMoveDown(moveCount);
        }
        //向左移动列
        for (int col = MaxColumn - 2; col >= 0; col--)
        {
            bool isEmpty = true;
            foreach (var rest in StarList)
            {
                if (rest.Column == col)
                {
                    isEmpty = false;
                    break;
                }
            }
            if (isEmpty)
            {
                foreach (var rest in StarList)
                {
                    if (rest.Column > col)
                    {
                        rest.OpenMoveLeft(1);
                    }
                }
            }
        }
        ClickedList.Clear();
    }

    public void JudgeOver()
    {
        bool isOver = true;
        foreach (var star in StarList)
        {
            FindTheSameStar(star);
            if (ClickedList.Count > 0)
            {
                isOver = false;
                break;
            }
        }
        ClickedList.Clear();
        if (isOver)
        {
            foreach (var item in StarList)
            {
                item.DestroyStar();
                CurrentScore += 5; //每个剩余星星加5分
            }
            StarList.Clear();
            CurrentScoreText.text = "当前得分：" + CurrentScore.ToString();
            if (CurrentScore >= TargetScore)
            {
                GoToNext();
            }
            else
            {
                GameOver();
            }
        }
    }

    public void CalculateScore()
    {
        //增加得分
        CurrentScore += 5 * ClickedList.Count * ClickedList.Count;
        CurrentScoreText.text = "当前得分：" + CurrentScore.ToString();

    }

    public void SetTargetScore()
    {
        if (HurdleIndex == 1)
            TargetScore = 1000;
        else
            TargetScore = (100 * HurdleIndex + 1700) * HurdleIndex - 800;
        TargetScoreText.text = "目标得分：" + TargetScore.ToString();
    }

    public void GameOver()
    {
        RestartButton.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
