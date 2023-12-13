using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    //关联HpText预制件
    public GameObject HpTextPre;
    //伤害文字
    public void ShowText(string text)
    {
        //实例化文字预制件
        GameObject go = Instantiate(HpTextPre, transform);
        //设置文字内容
        go.GetComponent<HpControl>().SetText(text);
    }

    // Update is called once per frame
    void Update()
    {
        //面向摄像机
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.
            forward);
    }
}
