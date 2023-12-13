using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    private static QuestManager instance;
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestManager();
            }
            return instance;
        }
    }
    //任务列表
    private List<QuestData> QuestList = new List<QuestData>();
    //是否接受任务
    public bool HasQuest(int id)
    {
        //遍历已有的任务
        foreach (QuestData qd in QuestList)
        {
            //如果已经有该任务
            if (qd.id == id)
            {
                return true;
            }
        }
        return false;
    }
    //添加任务
    public void AddQuest(int id)
    {
        //如果没有接受该任务
        if (!HasQuest(id))
        {
            //接受该任务
            QuestList.Add(QuestDataManager.Instance.QuestDic[id]);
        }
    }
    //击杀了敌人
    public void AddEnemy(int enemyid)
    {
        //遍历任务
        for(int i = 0; i < QuestList.Count; i++)
        {
            QuestData qd = QuestList[i];
            //遍历任务中是否有该击杀敌人的需求
            if(qd.enemyId == enemyid)
            {
                //有的话，增加击杀敌人任务完成数量
                qd.currentCount++;
                //如果敌人击杀完成数量大于需求数量，则完成任务
                if(qd.currentCount >= qd.count)
                {
                    //任务完成，这里可以制作任务奖励、光效等内容
                    Debug.Log("任务完成，这里可以制作任务奖励、光效等内容");
                    //删除任务
                    qd.currentCount = 0;
                    QuestList.Remove(qd);
                    //我们这里让主角显示一个光效
                    //读取光效预制体
                    GameObject go = Resources.Load<GameObject>("fx_hr_arthur_pskill_03_2");
                    //获取主角
                    Transform player = GameObject.FindWithTag("Player").transform;
                    //创建光效
                    GameObject.Instantiate(go, player.position, player.rotation);
                }
            }
        }
    }
}
