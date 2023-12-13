using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//主角状态枚举
public enum PlayerState
{
    idle, //人物初始站立状态
    run, //人物奔跑状态
    die, //死亡动画
    attack, //普通攻击
    attack2 //
}

public class ThirdControl : MonoBehaviour
{
    //主角状态
    private PlayerState state = PlayerState.idle;
    //动画器组件
    private Animator animator;
    //最大血量
    public int MaxHp = 100;
    //血量
    public int Hp = 100;
    //特效数组
    private List<Transform> fxList;
    float h; //水平轴系数
    float v; //垂直轴系数
    public float speed = 6;//人物移动速度
    public float turnSpeed = 15;//旋转速度
    private Transform camTransform; //相机
    Vector3 camForward; //临时三维坐标
    void Start()
    {
        //获取主相机组件
        camTransform = Camera.main.transform;
        //获取动画器组件
        animator = GetComponent<Animator>();
        //隐藏鼠标指针
        Cursor.lockState = CursorLockMode.Locked;
        //实例化数组
        fxList = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //按下Alt键
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            animator.SetBool("Run", false);
            //显示鼠标指针
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
        //如果鼠标指针为呼出状态，则不做任何事
        if(Cursor.lockState == CursorLockMode.None)
        {
            return;
        }
        //判断状态
        switch (state)
        {
            case PlayerState.idle:
                //允许移动
                Move();
                //攻击
                Attack();
                //播放站立动画
                animator.SetBool("Run", false);
                break;
            case PlayerState.run:
                //允许移动
                Move();
                //允许攻击
                Attack();
                //播放移动动画
                animator.SetBool("Run", true);
                break;
            case PlayerState.die:
                break;
            case PlayerState.attack:
                break;
            case PlayerState.attack2:
                break;
        }
        //按下B键
        if(Input.GetKeyDown(KeyCode.B))
        {

        }
        //要删除的特效
        Transform fx = null;
        //刷新特效的位置
        foreach(Transform trans in fxList)
        {
            //特效移动
            trans.Translate(Vector3.forward * 20 * Time.deltaTime);
            //判断周围有没有敌人
            Collider[] colliders = Physics.OverlapSphere(trans.position, 1f);
            //遍历特效
            foreach(Collider collider in colliders)
            {
                //如果附近有敌人
                if(collider.tag == "Enemy")
                {
                    //敌人血量减少
                    collider.GetComponent<EnemyControl>().GetDamage(20);
                    //待删除的火焰特效
                    fx = trans;
                    //爆炸特效
                    //加载特效预制件
                    GameObject fxPre = Resources.Load<GameObject>("Explosion");
                    //实例化特效
                    GameObject go = Instantiate(fxPre, collider.transform.position,
                        collider.transform.rotation);
                    //删除特效物体
                    Destroy(go, 2f);
                    break;
                }
            }
        }
        if(fx!=null)
        {
            //将特效从数组中删除
            fxList.Remove(fx);
        }
    }
    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        //-----
        //首先向量是矩阵
        //在位置变化过程中都是矩阵运算
        transform.Translate(camTransform.right * h * speed * Time.deltaTime //角色水平移动速度（水平移动矩阵）
            + camForward * v * speed * Time.deltaTime //角色垂直移动速度（垂直移动矩阵）
            , Space.World);
        //-----
        //水平垂直方向系数不为0表示需要进行旋转，角色跟随相机旋转
        if (h != 0 || v != 0)
        {
            Rotating(h, v);
        }
        if (dir != Vector3.zero)
        {
            //切换移动状态
            state = PlayerState.run;
        }
        else
        {
            //切换为站立状态
            state = PlayerState.idle;
        }
    }
    //旋转
    void Rotating(float horizontal, float vertical)
    {
        //对相机水平轴和世界Y轴做叉积
        camForward = Vector3.Cross(camTransform.right, Vector3.up);
        //计算目标位置
        Vector3 targetDir = camTransform.right * horizontal + camForward * vertical;//两个向量相加
        //计算目标旋转角度
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        //旋转
        //-----
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        //-----
    }

    //攻击
    void Attack()
    {
        //单击鼠标左键攻击
        if (Input.GetMouseButtonDown(0))
        {
            //播放攻击动画
            animator.SetTrigger("Attack");
            //攻击状态
            state = PlayerState.attack;
        }
        //单击鼠标右键攻击
        if (Input.GetMouseButtonDown(2))
        {
            //播放攻击动画
            animator.SetTrigger("Attack2");
            //攻击状态
            state = PlayerState.attack2;
        }
    }
    //结束攻击
    void AttackEnd()
    {
        //恢复为站立状态
        state = PlayerState.idle;
    }
    //收到攻击
    public void GetDamage(int Damage)
    {
        //血量减少
        Hp -= Damage;
        //如果血量为0
        if (Hp <= 0)
        {
            //变为死亡状态
            state = PlayerState.die;
            //播放一次死亡动画
            animator.SetTrigger("Die");
            //死亡3s后复活
            Invoke("Revive", 3f);
        }
    }
    //复活
    public void Revive()
    {
        //如果是死亡状态
        if (state == PlayerState.die)
        {
            //血量恢复
            Hp = MaxHp;
            //复活
            animator.SetTrigger("Revive");
            //复活为站立状态
            state = PlayerState.idle;
            //复活位置
            transform.position = transform.position;
        }
    }
    //对敌人造成伤害
    void Damage(int damage)
    {
        //获取3m内的物体
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);
        //遍历物体
        foreach(Collider collider in colliders)
        {
            //判断是敌人并在60°的攻击范围内
            if(collider.tag == "Enemy" && Vector3.Angle(collider.transform.
                position - transform.position, transform.forward) < 60)
            {
                //敌人受到伤害
                collider.GetComponent<EnemyControl>().GetDamage(damage);
            }
        }
    }
    //特效
    Transform FX(string name, float desTime)
    {
        //加载特效预制件
        GameObject fxPre = Resources.Load<GameObject>(name);
        //实例化特效
        GameObject go = Instantiate(fxPre, transform.position, transform.
            rotation);
        //删除特效物体
        Destroy(go, desTime);
        return go.transform;
    }
    //攻击1_1
    void Attack1_1() 
    {
        //攻击
        Damage(20);
        //特效
        FX("fx_hr_arthur_attack_01_1", 0.5f);
    }
    //攻击1_2
    void Attack1_2() 
    {
        //攻击
        Damage(20);
        //特效
        FX("fx_hr_arthur_attack_01_2", 0.5f);
        //添加能量火焰特效
        for(int i = 0; i < 5; i++)
        {
            //创建火焰特效
            Transform fire = FX("Magic fire pro red", 1f);
            //设置火焰特效的旋转
            fire.transform.rotation = transform.rotation;
            //设置不同的旋转角度
            fire.transform.Rotate(fire.transform.up, 15 * i - 30);
            //添加到特效数组
            fxList.Add(fire);
            //1s后清空特效数组
            Invoke("ClearFXList", 1f);
        }
    }
    //清空特效数组
    void ClearFXList()
    {
        fxList.Clear();
    }
    //攻击2_0
    void Attack2_0() 
    {
        //特效
        FX("fx_hr_arthur_pskill_03_1", 1f);
        //增加一个特效
        FX("RotatorPS2", 4f);
    }
    //攻击2_1
    void Attack2_1() 
    {
        //攻击
        Damage(80);
        //特效
        FX("fx_hr_arthur_pskill_01", 1.8f);
    }
    //攻击2_2
    void Attack2_2() 
    {
        //攻击
        Damage(20);
    }
}
