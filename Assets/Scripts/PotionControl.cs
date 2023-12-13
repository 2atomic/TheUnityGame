using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionControl : MonoBehaviour
{
    //触发
    private void OnTriggerEnter(Collider other)
    {
        //如果是主角触发
        if (other.tag == "Player")
        {
            //获取主角脚本
            ThirdControl player = other.GetComponent<ThirdControl>();
            //增加血量
            player.Hp += 10;
            //判断是否超过上限
            if(player.Hp > player.MaxHp)
            {
                player.Hp = player.MaxHp;
            }
            //删除自己
            Destroy(gameObject);
        }
    }
}
