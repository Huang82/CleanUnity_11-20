using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quaternion_Test : MonoBehaviour
{

    [SerializeField] private Transform go1;
    [SerializeField] private Transform go2;
    [SerializeField] private Transform go3;

    private Quaternion quaternion;
    private float m_Speed = 0.1f;
    private float NowTime = 0f;

    void Start()
    {
        quaternion = new Quaternion();
    }


    void Update()
    {
        SlerpUnclampedTest();
    }

    // a向量方向轉至b向量方向會將這值存進Quaternion變數變成旋轉值再將此旋轉值*現在角度
    private void SetFromToRotationTest()
    {
        quaternion.SetFromToRotation(go1.forward, (go1.right + go1.forward));

        go1.rotation *= quaternion;

        Debug.LogError("Qaternion： " + go1.rotation.eulerAngles);
    }

    // 賦予一個向量讓角度朝著那向量方向
    private void SetLookRotationTest()
    {
        Vector3 lookDir = go2.position - go1.position;
        quaternion.SetLookRotation(lookDir);

        go1.rotation = quaternion;

        Debug.LogError(go1.rotation.eulerAngles);
    }

    // a角度轉到b角度的最小轉到的角度
    private void AngleTest()
    {
        float angle = Quaternion.Angle(go1.rotation, go2.rotation);
        Debug.LogError("Angle: " + angle);
    }

    // 輸入角度和旋轉軸向就可以得出四位元角度
    private void AngleAxisTest()
    {
        float angle;
        Vector3 vector3;
        go1.rotation.ToAngleAxis(out angle, out vector3);

        go2.rotation = Quaternion.AngleAxis(angle, vector3);

        Debug.LogError("Angle: " + angle + " ,Vector3: " + vector3);
    }

    // 輸入兩個Quaternion可以得知-1~1的值，數字代表1表示轉向同個角度，-1代表轉了360度
    private void DotTest()
    {
        float dot = Quaternion.Dot(go1.rotation, go2.rotation);
        Debug.LogError("Dot: " + dot);
    }

    // a向量直線到達b向量的角度旋轉值
    private void FromToRotationTest()
    {
        Quaternion t = Quaternion.FromToRotation(go1.forward, (go1.right + go1.forward));

        go1.rotation = t;

        Debug.LogError("Qaternion： " + go1.rotation.eulerAngles);
    }

    // rotation(x,y,z,w)，那麼Quaternion.Inverse(rotation) = (-x,-y,-z,w)，但歐拉角不一定會是(-x, -y, -z)
    private void InverseTest()
    {
        Quaternion t = Quaternion.Inverse(go1.rotation);

        go1.rotation = t;

        Debug.LogError("Qaternion： " + go1.rotation.eulerAngles);
    }

    // a角度到達b角度可以賦予0~1的值表示到達進度
    private void LerpTest()
    {
        Quaternion t = Quaternion.Lerp(go1.rotation ,go2.rotation, m_Speed * NowTime);
        go3.rotation = t;
        NowTime += Time.deltaTime;
    }

    // 同Lerp但超過1繼續轉
    private void LerpUnclampedTest()
    {
        Quaternion t = Quaternion.LerpUnclamped(go1.rotation, go2.rotation, m_Speed * NowTime);
        go3.rotation = t;
        NowTime += Time.deltaTime;
    }

    // 賦予一個向量讓角度朝著那向量方向，另個值可加可不加，會影響那面朝哪邊
    private void LookRotationTest()
    {
        Vector3 lookDir = go2.position - go1.position;
        Quaternion t = Quaternion.LookRotation(lookDir, Vector3.up);
        go1.rotation = t;
    }

    private void NormalizeTest()
    {
        Quaternion normalize = Quaternion.Normalize(go1.rotation);
        Debug.LogError("Rotation: " + go1.rotation.x + ",Normalize: " + normalize.x);
    }

    // a角度轉至b角度，maxDegreesDelta決定每次轉幾度，from到達to會判斷角度是否相同而停止數值輸出
    private void RotateTowardsTest()
    {
        Quaternion t = Quaternion.RotateTowards(go1.rotation, go2.rotation, m_Speed * Time.deltaTime * 100f);
        go1.rotation = t;
    }

    // 轉動的角度比較圓滑(慢-快-慢)不會像Lerp速度都一致
    private void SlerpTest()
    {
        Vector3 vecDir = go2.position - go1.position;
        Quaternion rotDir = Quaternion.LookRotation(vecDir);

        Quaternion t = Quaternion.Slerp(go1.rotation, rotDir, Time.deltaTime * m_Speed * 10f);
        go1.rotation = t;
    }

    // 與Slerp一樣但是超過會繼續給值(不知為啥還是會停)
    private void SlerpUnclampedTest()
    {
        Vector3 vecDir = go2.position - go1.position;
        Quaternion rotDir = Quaternion.LookRotation(vecDir);

        Quaternion t = Quaternion.SlerpUnclamped(go1.rotation, rotDir, Time.deltaTime * m_Speed * 10f);
        go1.rotation = t;
    }
}
