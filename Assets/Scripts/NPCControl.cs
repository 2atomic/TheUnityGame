using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControl : MonoBehaviour
{
    //NPC姓名
    public string Name = "村民";
    //NPC对话
    public string Content = "最近村外石头人比较多，快去击杀两个吧！";
    //任务id
    public int QuestID = 1001;
    //主角
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //通过标签获取主角
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //获得NPC和主角的距离
        float dis = Vector3.Distance(player.position, transform.position);
        //距离小于4m的时候，按下F键
        if(dis<4&&Input.GetKeyDown(KeyCode.F))
        {
            //显示对话框
            UIManager.Instance.Show(Name, Content, QuestID);
        }
    }
}
