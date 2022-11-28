using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3_Test : MonoBehaviour
{
    [SerializeField] private Transform go;
    [SerializeField] private Transform go2;
    [SerializeField] private Transform go3;

    private Vector3 InitGoPos;

    private Vector3 Vec1;

    private float WaitSec = 5f;     // 等待秒數
    private float NowSec = 0f;      // 現在秒數

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Vec1 = Vector3.zero;
        InitGoPos = go.position;

    }


    void Update()
    {
        TestSmoothDamp();

    }

    // 計算出兩物件的角度(必須給個方向當作0度再由這個方向來計算角度)
    private void TestAngle()
    {
        Vector3 targetDir = go.position - this.transform.position;
        float angle = Vector3.Angle(targetDir, this.transform.forward);

        Debug.LogError("targetDir: " + targetDir + " ,transform.forward: " + this.transform.forward + " ,angle: " + angle);

        //float angle = Vector3.Angle(go.transform.position, this.transform.position);

        //Debug.LogError("angle: " + angle);
    }

    // 以Vector3(0,0,0)為中心點賦予的Vector3會回覆maxLength的Vector3(也就是說只會回傳maxLength座標內的值)
    private void TestClampMagnitude()
    {
        Vector3 targetPos = Vector3.ClampMagnitude(Vector3.zero + go.position, 2f);
        go.position = targetPos;
        Debug.LogError(targetPos);
    }

    // 不知道可以用在哪裡，輸出會依照左手法則而前進方向不同
    private void TestCross()
    {
        Vector3 targetPos = Vector3.Cross(this.transform.position, go.position);
        go2.position = targetPos;
        Debug.LogError(targetPos);
    }

    // 應用很多種，這邊舉個比較常用的，可以知道A物件是否有無在B物件眼前，為1代表在眼前
    private void TestDot()
    {
        // 算出離觀看物件朝哪個方向並歸一化
        Vector3 Normalize = Vector3.Normalize(this.transform.position - go.position);
        float dot = Vector3.Dot(Normalize, this.transform.forward);
        
        Debug.LogError(dot);
        if (0.9f <= dot && dot <= 1)
            Debug.LogError("物件看到所設置的物件了");
    }

    // 賦予0~1的值會輸出a到b的值(也就是說可以賦予時間幾秒到指定的地方)
    private void TestLerp()
    {
        Vector3 TargetPos = Vector3.Lerp(InitGoPos, (InitGoPos + Vector3.up * 5), NowSec / WaitSec);

        go.position = TargetPos;

        NowSec += Time.deltaTime;
    }

    // 功能同為Lerp但是賦予超過1時繼續輸出超過b的值
    private void TestLerpUnclamped()
    {
        Vector3 TargetPos = Vector3.LerpUnclamped(InitGoPos, (InitGoPos + go.transform.up * 5), NowSec / WaitSec);

        go.position = TargetPos;

        NowSec += Time.deltaTime;
    }

    // 取a與b的最大或最小的值在輸出
    private void TestMaxAndMin()
    {
        Vector3 MaxPos = Vector3.Max(this.transform.position, go.position);
        Vector3 MinPos = Vector3.Min(this.transform.position, go.position);

        go2.position = MaxPos;
        go3.position = MinPos;

        Debug.LogError("MaxPos: " + MaxPos + " ,MinPos: " + MinPos);
    }

    // 回傳a到b的Vector3再輸入maxDistanceDelta(移動多少距離)而得出所移動到點位置，輸入的值過大輸出的點不會超出b點
    private void TestMoveTowards()
    {
        float speed = 1f;
        Vector3 TargetPos = Vector3.MoveTowards(InitGoPos, (InitGoPos + Vector3.down * 5), speed * (NowSec / WaitSec));

        go.position = TargetPos;

        NowSec += Time.deltaTime;
    }

    // 完全不知道再做甚麼的，用於計算用途
    private void TestOrthoNormalize()
    {
        Vector3 o = this.transform.position;
        Vector3 t = go.position;
        Debug.LogError("in:   o: " + o + " ,t: " + t);
        Vector3.OrthoNormalize(ref o ,ref t);
        Debug.LogError("out:  o: " + o + " ,t: " + t);

        go2.position = o;
        go3.position = t;
    }

    // 用來計算Vector陰影(有點難解說)
    private void TestProject()
    {
        Vector3 TargetPos = Vector3.Project(this.transform.position, go.position);

        go2.position = TargetPos;
        Debug.LogError(TargetPos);
    }

    // 好像與Project輸出出來的座標相反?
    private void TestProjectOnPlane()
    {
        Vector3 TargetPos = Vector3.ProjectOnPlane(this.transform.position, go.position);
        go3.position = TargetPos;
        Debug.LogError(TargetPos);
    }

    // 類似鏡像然後給個鏡子方向輸出會是與輸入的pos相反
    private void TestReflect()
    {
        Vector3 TargetPos = Vector3.Reflect(this.transform.position, Vector3.right);
        go.position = TargetPos;
        Debug.LogError(TargetPos);
    }

    // 用於計算a物件朝向b物件的角度方向
    private void TestRotateTowards()
    {
        float speed = 10f;

        Vector3 targetDirection = go.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(this.gameObject.transform.forward, targetDirection, step, 0.0F);
        this.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
        Debug.LogError(newDirection);
    }

    // 就只是兩個Vector相乘而已
    private void TestScale()
    {
        Vector3 targetPos = Vector3.Scale(this.transform.position, go.position);
        go2.position = targetPos;
        Debug.LogError(targetPos);
    }

    // 與Angle差不多只是這可以指定方向
    private void TestSignedAngle()
    {
        Vector3 targetDir = go.position - this.transform.position;
        float angle = Vector3.SignedAngle(targetDir, this.transform.forward, Vector3.forward);

        Debug.LogError(angle);
    }

    // 與Lerp不同的是這是弧形的到達點(要注意0,0,0不知為啥會失效)
    private void TestSlerp()
    {
        Vector3 TargetPos = Vector3.Slerp(InitGoPos, (InitGoPos + Vector3.up * 5), NowSec / WaitSec);

        go2.position = TargetPos;

        NowSec += Time.deltaTime;
    }

    // 到達b點後一樣繼續跑
    private void TestSlerpUnclamped()
    {
        Vector3 TargetPos = Vector3.SlerpUnclamped(InitGoPos, (InitGoPos + Vector3.up * 5), NowSec / WaitSec);

        go2.position = TargetPos;

        NowSec += Time.deltaTime;
    }


    // a幾秒內移至b，與lerp不同的是這適合拿來做VR撿起東西的移動感(要記得Velocity要放全域變數)
    private void TestSmoothDamp()
    {
        float smoothTime = 0.3F;

        Vector3 targetPos = Vector3.SmoothDamp(this.transform.position, go.position, ref velocity, smoothTime);
        this.transform.position = targetPos;

        Debug.LogError("targetPos: " + targetPos + " ,velocity: " + velocity);
    }

    // ---------------------------------------------------------------------
    // 讓物件跟隨在前方
    private void TestGameObjectForward()
    {
        Vector3 ForwardPos = this.transform.position + (this.transform.forward * 3);
        go.position = ForwardPos;
    }

    
}
