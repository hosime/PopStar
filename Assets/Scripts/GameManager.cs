using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] StarObjs;
    public GameObject StarGroup;
    public List<Star> StarList;
    public int Row = 12;
    public int Column = 10;
 
    // Use this for initialization
    void Start () {
        CreateStarList();
    }
    
    // Update is called once per frame
    void Update () {
        
    }


    void CreateStarList()
    {
        for (int r = 0; r < Row; r++)
        {
            for (int c = 0; c < Column; c++)
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
}
