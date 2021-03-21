using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//星星颜色
public enum Color { Blue, Green, Orange, Purple, Red }

public class Star : MonoBehaviour {

    public int Row = 0;
    public int Column = 0;
    public Color StarColor = Color.Blue;

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void OnClick_Star()
    {
        Debug.Log(StarColor.ToString());
        GameManager.gameManager_Instance.FindTheSameStar(this);
        GameManager.gameManager_Instance.ClearClickedList();
    }

    public void DestroyStar()
    {
        Destroy(this.gameObject);
    }
}
