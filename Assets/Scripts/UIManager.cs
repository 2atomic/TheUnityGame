using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //单例
    public static UIManager Instance;
    //对话框
    private Image dialog;
    //血条
    private Image hpBar;
    //主角
    private ThirdControl player;
    //与该对话相关的任务id
    private int questid;

    void Awake()
    {
        //获取单例
        Instance = this;
        //获取血条
        hpBar = transform.Find("Head").Find("HpBar").GetComponent<Image>();
        //获取主角
        player = GameObject.FindWithTag("Player").GetComponent<ThirdControl>();
        //获取对话框
        dialog = transform.Find("Dialog").GetComponent<Image>();
        //默认隐藏对话框
        dialog.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //更新血条
        hpBar.fillAmount = (float)player.Hp / player.MaxHp;
    }

    //显示对话框，参数为对话标题、内容、相关的任务
    public void Show(string name, string content, int id = -1)
    {
        //呼出鼠标指针
        Cursor.lockState = CursorLockMode.None;
        //显示对话框
        dialog.gameObject.SetActive(true);
        //设置标题
        dialog.transform.Find("NameText").GetComponent<Text>().text =
            name;
        //记录任务id
        questid = id;
        //判断该任务是否被接受
        if(QuestManager.Instance.HasQuest(id))
        {
            //已接受任务
            dialog.transform.Find("ContentText").GetComponent<Text>().text =
                "你已经接受了该任务了";
        }
        else
        {
            //若未接受任务，则直接显示任务名称
            dialog.transform.Find("ContentText").GetComponent<Text>().text =
                content;
        }
    }

    //将AcceptButton的鼠标单击事件设置为该方法
    public void AcceptButtonClick()
    {
        //隐藏对话框
        dialog.gameObject.SetActive(false);
        //接受任务
        QuestManager.Instance.AddQuest(questid);
        //隐藏鼠标指针
        Cursor.lockState = CursorLockMode.Locked;
    }
    //将CancelButton的鼠标单击事件设置为该方法
    public void CancelButtonClick()
    {
        //隐藏对话框
        dialog.gameObject.SetActive(false);
        //隐藏鼠标指针
        Cursor.lockState = CursorLockMode.Locked;
    }
}
