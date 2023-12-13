using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //敌人id
    public int ID = 101;
    //主角
    public ThirdControl player;
    //血量
    public int Hp = 100;
    //攻击力
    public int Attack = 20;
    //出生点位置
    private Vector3 position;
    //动画器组件
    private Animator animator;
    //攻击计时器
    private float timer = 1;
    //当前是否正在攻击
    private bool isAttack = false;
    //血瓶预制件，关联“项目”面板中RPG Pack/Prefabs/Bottle_Health
    public GameObject PotionPre;
    //敌人的碰撞器组件
    //private CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        //获取主角脚本
        player = GameObject.FindWithTag("Player").GetComponent<ThirdControl>();
        //获取出生点位置
        position = transform.position;
        //获取动画器组件
        animator = GetComponent<Animator>();
        //获取敌人的碰撞器组件
        //capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果主角死亡，则停止一切动作
        if(player.Hp <= 0 || Hp <= 0)
        {
            //停止播放攻击与移动动画
            animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            return;
        }
        //获取与主角的距离
        float distance = Vector3.Distance(player.transform.position, transform.position);
        //如果在周围18m内没发现主角
        if(distance > 18f)
        {
            //距离出生点超过15m
            if(Vector3.Distance(transform.position, position) > 15f)
            {
                //转向出生点
                transform.LookAt(new Vector3(position.x, transform.position.y,
                    position.z));
                //向前移动，也可以使用导航系统代替
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                //播放移动动画
                animator.SetBool("Run", true);
            }
            else
            {
                //停止播放移动动画
                animator.SetBool("Run", false);
            }
        }
        else if(distance > 3f)
        {
            //如果与主角的距离在3m到7m之间，则朝主角移动
            //转向玩家
            transform.LookAt(new Vector3(player.transform.position.x,
                transform.position.y, player.transform.position.z));
            //向前移动，这里我们直接移动，如果需要也可使用导航功能移动
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            //播放移动动画
            animator.SetBool("Run", true);
            //保证当前不在攻击状态
            isAttack = false;
            animator.SetBool("Attack", false);
        }
        else
        {
            //3m内停止移动，开始攻击
            //停止播放移动动画
            animator.SetBool("Run", false);
            //转向玩家
            transform.LookAt(new Vector3(player.transform.position.x,
                transform.position.y, player.transform.position.z));
            //攻击
            animator.SetBool("Attack", true);
            //如果不在攻击状态
            if(isAttack == false)
            {
                //设置为攻击状态
                isAttack = true;
                //将计时器重置为1
                timer = 1;
            }
            //计时器时间增加
            timer += Time.deltaTime;
            //这里我们用不同于主角的攻击方法，
            //使用计时器来计算什么时候打出伤害
            //攻击时间为2s，我们在1s的时候打出伤害
            if(timer >= 2)
            {
                timer = 0;
                //打出伤害
                player.GetDamage(Attack);
            }
        }
    }
    //受到伤害
    public void GetDamage(int damage)
    {
        if(Hp > 0)
        {
            //弹出伤害值
            GetComponentInChildren<HpManager>().ShowText("-" + damage);
            //减少血量
            Hp -= damage;
            //如果血量为0s
            if(Hp <= 0)
            {
                //敌人的碰撞器设置为无效
                //capsuleCollider.isTrigger = true;
                //掉落一个血瓶
                Instantiate(PotionPre, transform.position,
                    transform.rotation);
                //播放一次死亡动画
                animator.SetTrigger("Die");
                //给任务系统报告，击杀了一个ID为101的敌人
                QuestManager.Instance.AddEnemy(ID);
                //销毁自己
                Destroy(gameObject, 2f);
            }
        }
    }
}
