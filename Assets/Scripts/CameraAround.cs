using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAround : MonoBehaviour
{
    private Transform target;   //相机追随目标
    public float xSpeed = 200;  //X轴方向拖动速度
    public float ySpeed = 200;  //Y轴方向拖动速度
    public float mSpeed = 10;   //放大缩小速度
    public float yMinLimit = -10; //在Y轴最小移动范围
    public float yMaxLimit = 10; //在Y轴最大移动范围
    public float distance = 6;  //相机视角距离
    public float minDistance = 20; //相机视角最小距离
    public float maxDistance = 30; //相机视角最大距离
    public float x = 0.0f;
    public float y = 0.0f;
    public float damping = 5.0f;  //阻尼值
    public bool isDamping = true;  //是否开启阻尼


    // Start is called before the first frame update
    void Start()
    {
        //获取目标，也就是要围绕的旋转的transform组件
        target = GameObject.FindWithTag("target").transform;
        //获取摄像机开始时的欧拉角
        Vector3 angle = transform.eulerAngles;
        Debug.Log(angle.ToString());
        //令x等于当前摄像机在世界坐标轴y上的欧拉值
        x = angle.y;
        Debug.Log(x);
        //令x等于当前摄像机在世界坐标轴x上的欧拉值
        y = angle.x;
        Debug.Log(y);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            //if (Input.GetMouseButton(1))
            //{
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            //进行角度限制
            y = ClamAngle(y, yMinLimit, yMaxLimit);
            //}
            //鼠标滚轮向上滑动为0.1，向下滑动为-0.1
            distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;
            //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
            //返回一个值，如果比minDistance小就返回minDistance，比maxDistance大就返回maxDistance
            //其余情况就返回该值distance
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            //临时变量存储欧拉角
            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            //Debug.Log(rotation.ToString());
            //临时变量存储距离变化
            Vector3 disVector = new Vector3(0.0f, 0.0f, -distance);
            //临时变量存储位置变化
            Vector3 position = rotation * disVector + target.position;

            if (isDamping)
            {
                //摄像机旋转角度计算公式当前摄像机的角度与目标角度进行插值，有种摄像机移动阻力感
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
                //摄像机旋转角度计算公式当前摄像机的位置与目标位置进行插值
                transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);
            }
            else
            {
                transform.rotation = rotation;
                transform.position = position;
            }
        }
    }
    /// <summary>
    /// 限制某一轴移动范围
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    static float ClamAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
