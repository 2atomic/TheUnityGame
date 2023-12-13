using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ThreadTest2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("正在忙工作");
        Debug.Log("正在忙工作");
        Debug.Log("正在忙工作");
        //开始执行多线程，创建一个多线程进行烧水操作，主线程不参与
        ThreadStart ts = new ThreadStart(Test);
        Thread thread = new Thread(ts);
        thread.Start();
        //创建完线程后，主线程立刻继续执行
        Debug.Log("继续工作"); 
        Debug.Log("继续工作"); 
        Debug.Log("继续工作");
    }

    void Test()
    {
        Debug.Log("开始烧水");
        //模拟烧水等待时间，这里等待5s
        Thread.Sleep(5000);
        Debug.Log("可以使用热水啦Q_Q！");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
