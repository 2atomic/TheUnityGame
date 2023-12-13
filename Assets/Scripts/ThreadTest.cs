using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ThreadTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //创建多线程
        ThreadStart ts = new ThreadStart(threadTest);
        Thread thread = new Thread(ts);
        //开始多线程
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void threadTest()
    {
        //该方法中的代码均在子线程中调用
        Debug.Log("线程中");
        //线程休眠
        Thread.Sleep(5000);
        Debug.Log("5s过去了");
    }
}
