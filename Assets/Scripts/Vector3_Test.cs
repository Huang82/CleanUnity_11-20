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

    private float WaitSec = 5f;     // ���ݬ��
    private float NowSec = 0f;      // �{�b���

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

    // �p��X�⪫�󪺨���(�������Ӥ�V��@0�צA�ѳo�Ӥ�V�ӭp�⨤��)
    private void TestAngle()
    {
        Vector3 targetDir = go.position - this.transform.position;
        float angle = Vector3.Angle(targetDir, this.transform.forward);

        Debug.LogError("targetDir: " + targetDir + " ,transform.forward: " + this.transform.forward + " ,angle: " + angle);

        //float angle = Vector3.Angle(go.transform.position, this.transform.position);

        //Debug.LogError("angle: " + angle);
    }

    // �HVector3(0,0,0)�������I�ᤩ��Vector3�|�^��maxLength��Vector3(�]�N�O���u�|�^��maxLength�y�Ф�����)
    private void TestClampMagnitude()
    {
        Vector3 targetPos = Vector3.ClampMagnitude(Vector3.zero + go.position, 2f);
        go.position = targetPos;
        Debug.LogError(targetPos);
    }

    // �����D�i�H�Φb���̡A��X�|�̷ӥ���k�h�ӫe�i��V���P
    private void TestCross()
    {
        Vector3 targetPos = Vector3.Cross(this.transform.position, go.position);
        go2.position = targetPos;
        Debug.LogError(targetPos);
    }

    // ���Ϋܦh�ءA�o���|�Ӥ���`�Ϊ��A�i�H���DA����O�_���L�bB���󲴫e�A��1�N��b���e
    private void TestDot()
    {
        // ��X���[�ݪ���­��Ӥ�V���k�@��
        Vector3 Normalize = Vector3.Normalize(this.transform.position - go.position);
        float dot = Vector3.Dot(Normalize, this.transform.forward);
        
        Debug.LogError(dot);
        if (0.9f <= dot && dot <= 1)
            Debug.LogError("����ݨ�ҳ]�m������F");
    }

    // �ᤩ0~1���ȷ|��Xa��b����(�]�N�O���i�H�ᤩ�ɶ��X�����w���a��)
    private void TestLerp()
    {
        Vector3 TargetPos = Vector3.Lerp(InitGoPos, (InitGoPos + Vector3.up * 5), NowSec / WaitSec);

        go.position = TargetPos;

        NowSec += Time.deltaTime;
    }

    // �\��P��Lerp���O�ᤩ�W�L1���~���X�W�Lb����
    private void TestLerpUnclamped()
    {
        Vector3 TargetPos = Vector3.LerpUnclamped(InitGoPos, (InitGoPos + go.transform.up * 5), NowSec / WaitSec);

        go.position = TargetPos;

        NowSec += Time.deltaTime;
    }

    // ��a�Pb���̤j�γ̤p���Ȧb��X
    private void TestMaxAndMin()
    {
        Vector3 MaxPos = Vector3.Max(this.transform.position, go.position);
        Vector3 MinPos = Vector3.Min(this.transform.position, go.position);

        go2.position = MaxPos;
        go3.position = MinPos;

        Debug.LogError("MaxPos: " + MaxPos + " ,MinPos: " + MinPos);
    }

    // �^��a��b��Vector3�A��JmaxDistanceDelta(���ʦh�ֶZ��)�ӱo�X�Ҳ��ʨ��I��m�A��J���ȹL�j��X���I���|�W�Xb�I
    private void TestMoveTowards()
    {
        float speed = 1f;
        Vector3 TargetPos = Vector3.MoveTowards(InitGoPos, (InitGoPos + Vector3.down * 5), speed * (NowSec / WaitSec));

        go.position = TargetPos;

        NowSec += Time.deltaTime;
    }

    // ���������D�A���ƻ򪺡A�Ω�p��γ~
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

    // �Ψӭp��Vector���v(���I���ѻ�)
    private void TestProject()
    {
        Vector3 TargetPos = Vector3.Project(this.transform.position, go.position);

        go2.position = TargetPos;
        Debug.LogError(TargetPos);
    }

    // �n���PProject��X�X�Ӫ��y�Ьۤ�?
    private void TestProjectOnPlane()
    {
        Vector3 TargetPos = Vector3.ProjectOnPlane(this.transform.position, go.position);
        go3.position = TargetPos;
        Debug.LogError(TargetPos);
    }

    // �����蹳�M�ᵹ����l��V��X�|�O�P��J��pos�ۤ�
    private void TestReflect()
    {
        Vector3 TargetPos = Vector3.Reflect(this.transform.position, Vector3.right);
        go.position = TargetPos;
        Debug.LogError(TargetPos);
    }

    // �Ω�p��a����¦Vb���󪺨��פ�V
    private void TestRotateTowards()
    {
        float speed = 10f;

        Vector3 targetDirection = go.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(this.gameObject.transform.forward, targetDirection, step, 0.0F);
        this.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
        Debug.LogError(newDirection);
    }

    // �N�u�O���Vector�ۭ��Ӥw
    private void TestScale()
    {
        Vector3 targetPos = Vector3.Scale(this.transform.position, go.position);
        go2.position = targetPos;
        Debug.LogError(targetPos);
    }

    // �PAngle�t���h�u�O�o�i�H���w��V
    private void TestSignedAngle()
    {
        Vector3 targetDir = go.position - this.transform.position;
        float angle = Vector3.SignedAngle(targetDir, this.transform.forward, Vector3.forward);

        Debug.LogError(angle);
    }

    // �PLerp���P���O�o�O���Ϊ���F�I(�n�`�N0,0,0������ԣ�|����)
    private void TestSlerp()
    {
        Vector3 TargetPos = Vector3.Slerp(InitGoPos, (InitGoPos + Vector3.up * 5), NowSec / WaitSec);

        go2.position = TargetPos;

        NowSec += Time.deltaTime;
    }

    // ��Fb�I��@���~��]
    private void TestSlerpUnclamped()
    {
        Vector3 TargetPos = Vector3.SlerpUnclamped(InitGoPos, (InitGoPos + Vector3.up * 5), NowSec / WaitSec);

        go2.position = TargetPos;

        NowSec += Time.deltaTime;
    }


    // a�X������b�A�Plerp���P���O�o�A�X���Ӱ�VR�߰_�F�誺���ʷP(�n�O�oVelocity�n������ܼ�)
    private void TestSmoothDamp()
    {
        float smoothTime = 0.3F;

        Vector3 targetPos = Vector3.SmoothDamp(this.transform.position, go.position, ref velocity, smoothTime);
        this.transform.position = targetPos;

        Debug.LogError("targetPos: " + targetPos + " ,velocity: " + velocity);
    }

    // ---------------------------------------------------------------------
    // ��������H�b�e��
    private void TestGameObjectForward()
    {
        Vector3 ForwardPos = this.transform.position + (this.transform.forward * 3);
        go.position = ForwardPos;
    }

    
}
