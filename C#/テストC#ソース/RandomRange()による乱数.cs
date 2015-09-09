using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {

    public int idle = 1;   // �A�C�h����Ԕ���t���O�iON�̏ꍇ�̓����_���摜�\���j
    public int judge_Val;        // �o����
    public int push_Val;         // ��������
    public int result = 0;       // �W�����P���̔��茋�ʁi0:�����@1:�����@2:�������j
    private JankenArea jankenArea;  // �W�����P���G���A�p�t�B�[���h
    private int coinval = 5; // ���R�C�������i�v���p�e�B�j
    public int coinVal
    {
        get { return coinval; }
        set
        {
            // �R�C���������99�ȏ�̏ꍇ��99�ɌŒ肷��
            if (value >= 100) value = 99;
            // �R�C����������0�ȉ��̏ꍇ��0�ɌŒ肷��
            if (value <= 0) value = 0;
            coinval = value; 
        }
    }

    // -----------------------------------
    // ���������[���b�g�ɂ��N���W�b�g���Z���\�b�h
    // -----------------------------------
    public IEnumerator CreditsUp()
    {
        // �N���W�b�g�����Z
        int crd = Random.Range(1, 6); // 1�͊܂܂�邪�A6�͊܂܂�Ȃ�
        if (1 == crd) coinval += 1;
        if (2 == crd) coinval += 2;
        if (3 == crd) coinval += 4;
        if (4 == crd) coinval += 7;
        if (5 == crd) coinval += 20;

        // �҂����ԁi�\���Ԋu�̃}�[�W���l�j
        yield return new WaitForSeconds(0.4f);

        // �Q�[�����ĊJ���邽�߉������ڔ���t���O���N���A
        push_Val = 0;
    }

}
