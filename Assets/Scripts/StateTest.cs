using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//抽象状态类，这里作为每个状态的父类
public abstract class State
{
    //每个状态都要实现的抽象方法
    public abstract void Run();
}
//睡觉状态
public class SleepState:State
{
    public override void Run()
    {
        Debug.Log("睡觉状态的执行代码");
    }
}
//玩耍状态
public class PlayState : State
{
    public override void Run()
    {
        Debug.Log("玩耍状态的执行代码");
    }
}
//学习状态
public class StudyState : State
{
    public override void Run()
    {
        Debug.Log("学习状态的执行代码");
    }
}
//学生类
public class NewStudent
{
    //每名学生都包含一个当前的状态
    public State state;
    //接收时间，并切换学生该时间段的状态
    public void Run(int time)
    {
        if (time > 22 || time < 7)
        {
            state = new SleepState();
        }
        else if (time >= 7 && time <= 18)
        {
            state = new PlayState();
        }
        else
        {
            state = new StudyState();
        }
        //调用该状态
        state.Run();
    }
}
public class StateTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NewStudent student = new NewStudent();
        //做18点时的事情
        student.Run(18);
        //做22点时的事情
        student.Run(22);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
