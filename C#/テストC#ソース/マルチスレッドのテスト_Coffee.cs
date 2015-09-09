using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System;                       // �}���`�X���b�h�Ŏg��

public class Coffee : MonoBehaviour {

    public const int PAPER = 0;     // �y�[�p�[�h���b�v
    public const int SAIFON = 1;    // �T�C�t�H��

    public int num = 0;             // �l��
    public int hotwater = 0;        // ����
    public int meshbeen = 0;        // �҂�����
    public int meshval = 0;         // ���b�V���l
    public int coffee = 0;          // �����i
    public int cup = 0;             // �R�[�q�[�J�b�v
    public int driptype = 0;        // �h���b�v�^�C�v�i0:�y�[�p�[�h���b�v 1:�T�C�t�H���j
    public List<string> orderlist = new List<string>();

    delegate int Podmeth(int x);                    // �f���Q�[�g�^
    delegate int Dripmeth(int x, int y, int z);     // �f���Q�[�g�^
    Podmeth mydel;                                  // ���������\�b�h�p�t�B�[���h
    Dripmeth mydrip;                                // �h���b�v���\�b�h�p�t�B�[���h

	void Start ()
    {
        // �������ƃ��b�V���͕��ʓ����i�s������}���`�X���b�h�ŃR�[��
        mydel = new Podmeth(Podmethod);
        IAsyncResult podmeth = mydel.BeginInvoke(num, null, null);

        // ���b�V���i����҂��j
        meshbeen = Beenmesh(num, meshval);

        // ������������҂��A���Ɠ�����������h���b�v���{
        mydel.EndInvoke(podmeth);

        // �h���b�v��@��I��
        if (PAPER == driptype) // �y�[�p�[�h���b�v
        {
            mydrip = new Dripmeth(PaperDripper);
            //            coffee = PaperDripper(meshbeen, hotwater, num);
        }
        else                   // �T�C�t�H��
        {
            mydrip = new Dripmeth(SifonDripper);
            //            coffee = SifonDripper(meshbeen, hotwater, num);                
        }

        // �h���b�v���{
        coffee = mydrip(meshbeen, hotwater, num);

        // �T�[�u
        Server(coffee, num, orderlist);
    }

    // --------------------------
    // ��������
    // --------------------------
    int Podmethod(int num)
    {
        // 1�l��200ml x �l�����������i����"200"�͌Œ�j
        int hotwater = num * 200;

        // �����܂ł̑҂����Ԃ��R���[�`�����i�������e�����͌Œ�j
//        StartCoroutine(Wait(1.2f));�悭�l������R���[�`���̓��C���X���b�h�ł����g���Ȃ�������

        return hotwater;
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    // --------------------------
    // ���b�V��
    // --------------------------
    int Beenmesh(int num, int meshval)
    {
        // 1�l���̓�20g x �l�������w�胁�b�V���Ŕ҂��i����"20"�͌Œ�j
        int meshbeen = (num * 20) * meshval;

        return meshbeen;
    }

    // --------------------------
    // �h���b�p�[�i�y�[�p�[�h���b�v�j
    // --------------------------
    int PaperDripper(int been, int hotwater, int num)
    {
        // �R�[�q�[�🹂��
        int coffee = (hotwater / been) * (num * 20);

        return coffee;
    }

    // --------------------------
    // �h���b�p�[�i�T�C�t�H���h���b�v�j
    // --------------------------
    int SifonDripper(int been, int hotwater, int num)
    {
        // �R�[�q�[�🹂��
        int coffee = (hotwater / been) * (num * 20);

        return coffee;
    }

    // --------------------------
    // �T�[�o
    // --------------------------
    void Server(int coffee, int num, List<string> orderlist)
    {
        int cups = coffee / num;

        // �o�����R�[�q�[��l�����J�b�v�ɕ�������
        for (int x = 0; x < cups; x++)
        {
            orderlist.Add("�R�[�q�[");
        }

    }
}
