using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("正在忙工作");
        Debug.Log("正在忙工作");
        Debug.Log("正在忙工作");
        //开始执行协程，去烧水，如果在协程执行中希望停止协程，
        //可以使用Stop Coroutine或Stop Coroutines
        StartCoroutine(Test());
        //烧水协程，注意协程返回值需要设置为IEnumerator迭代器

        //烧完水回来继续工作，这时候水还有烧好
        Debug.Log("继续工作"); 
        Debug.Log("继续工作"); 
        Debug.Log("继续工作");
    }
    IEnumerator Test()
    {
        Debug.Log("开始烧水");
        //烧水中，这里我们假设需要5s
        //yield return为核心内容，这里会开始计时，
        //并返回方法中继续执行核心代码，而该方法剩下内容被暂停执行了
        //这里不仅仅可以返回WaitForSeconds类型，
        //而且可以返回WWW、WaitUntil和null等类型
        yield return new WaitForSeconds(5);
        //5s后，到达计时时间，继续执行该方法的剩余部分
        Debug.Log("可以使用热水啦Q_Q！");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
