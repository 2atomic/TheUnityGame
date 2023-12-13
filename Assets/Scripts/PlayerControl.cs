//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

////主角状态枚举
//public enum PlayerState
//{
//    idle,
//    run,
//    die,
//    attack,
//    attack2
//}

//public class PlayerControl : MonoBehaviour
//{
//    //主角状态
//    private PlayerState state = PlayerState.idle;
//    //动画器组件
//    private Animator animator;
//    //最大血量
//    public int MaxHp = 100;
//    //血量
//    public int Hp = 100;
//    //刚体组件
//    private Rigidbody rBody;

//    // Start is called before the first frame update
//    void Start()
//    {
//        //获取刚体组件
//        rBody = GetComponent<Rigidbody>();
//        //获取动画器组件
//        animator = GetComponent<Animator>(); 
//        //隐藏鼠标指针
//        Cursor.lockState = CursorLockMode.Locked;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //按下Alt键
//        if (Input.GetKeyDown(KeyCode.LeftAlt))
//        {
//            //显示鼠标指针
//            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
//        }
//        //如果鼠标指针为呼出状态，则不做任何事
//        if (Cursor.lockState == CursorLockMode.None)
//        {
//            return;
//        }
//        //判断状态
//        switch (state)
//        {
//            case PlayerState.idle:
//                //允许旋转
//                Rotate();
//                //允许移动
//                Move();
//                //攻击
//                Attack();
//                //播放站立动画
//                animator.SetBool("Run", false);
//                break;
//            case PlayerState.run:
//                //允许旋转
//                Rotate();
//                //允许移动
//                Move();
//                //允许攻击
//                Attack();
//                //播放移动动画
//                animator.SetBool("Run", true);
//                break;
//            case PlayerState.die:
//                break;
//            case PlayerState.attack:
//                break;
//            case PlayerState.attack2:
//                break;
//        }
//    }
//    //移动
//    void Move()
//    {
//        //获取水平轴
//        float horizontal = Input.GetAxis("Horizontal");
//        //获取垂直轴
//        float vertical = Input.GetAxis("Vertical");
//        //获取移动向量
//        Vector3 dir = new Vector3(horizontal, 0, vertical);
//        //如果按下了移动键
//        if (dir != Vector3.zero)
//        {
//            //纵向移动
//            rBody.velocity = transform.forward * vertical * 10;
//            //横向移动
//            rBody.velocity += transform.right * horizontal * 10;
//            //切换移动状态
//            state = PlayerState.run;
//        }
//        else
//        {
//            //切换为站立状态
//            state = PlayerState.idle;
//        }
//    }
//    //旋转
//    void Rotate()
//    {
//        //主角与摄像机同步旋转
//        transform.rotation = Camera.main.transform.parent.rotation;
//    }
//    //攻击
//    void Attack()
//    {
//        //单击鼠标左键攻击
//        if (Input.GetMouseButtonDown(0))
//        {
//            //播放攻击动画
//            animator.SetTrigger("Attack");
//            //攻击状态
//            state = PlayerState.attack;
//        }
//        //单击鼠标右键攻击
//        if (Input.GetMouseButtonDown(2))
//        {
//            //播放攻击动画
//            animator.SetTrigger("Attack2");
//            //攻击状态
//            state = PlayerState.attack2;
//        }
//    }
//    //结束攻击
//    void AttackEnd()
//    {
//        //恢复为站立状态
//        state = PlayerState.idle;
//    }
//    //收到攻击
//    public void GetDamage(int Damage)
//    {
//        //血量减少
//        Hp -= Damage;
//        //如果血量为0
//        if (Hp <= 0)
//        {
//            //变为死亡状态
//            state = PlayerState.die;
//            //播放一次死亡动画
//            animator.SetTrigger("Did");
//        }
//    }
//    //复活
//    public void Revive(Vector3 position)
//    {
//        //如果是死亡状态
//        if (state == PlayerState.die)
//        {
//            //复活
//            animator.SetTrigger("Revive");
//            //复活为站立状态
//            state = PlayerState.idle;
//            //复活位置
//            transform.position = position;
//        }
//    }
//    //攻击1_1
//    void Attack1_1() { }
//    //攻击1_2
//    void Attack1_2() { }
//    //攻击2_0
//    void Attack2_0() { }
//    //攻击2_1
//    void Attack2_1() { }
//    //攻击2_2
//    void Attack2_2() { }
//}
