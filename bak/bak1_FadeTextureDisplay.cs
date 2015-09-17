using UnityEngine;
using System.Collections;

////////////////////////////////////////////////////////////////////////////////////////
//�@�֐����F�t�F�[�h�N���X
//�@�@�\�F�V�[���؂�ւ����Ƀt�F�[�h�C�� / �t�F�[�h�A�E�g������
//�@�p���FMonoBehaviour
//�@��ʁF�ʏ�N���X
//�@�A�^�b�`��F���C���J�����I�u�W�F�N�g
//�@�ێ����\�b�h�F
//�@���_�C���N�g�F�Ȃ�
//
//�@�ڍׁF
//�@�@�@�@�V�[���؂�ւ����Ƀt�F�[�h�C�� / �t�F�[�h�A�E�g������
//
//  �Ăяo����F
//  �@�@�@�@�@�@Scene�J�ڎ��{
//  �@�@�@�@�@�@̪��ޱ�Ď��ԁA̪��ޒ��ҋ@���ԁA̪��޲ݎ��ԁA�װ�A�J�ڐ�Pos���(Vector3)�A�J�ڐ漰�
//            �@this.GetComponent<FadeToPos>().FadeOut(0.3f, 0.2f, 0.3f, Color.black, nextScene);
//
//�@�����F
//�@�@�@�@14.12.12 ����
//�@�@�@�@14.12.13 �Փ˂ɂ��Ĕ���o�O�̂��߉��C
//
////////////////////////////////////////////////////////////////////////////////////////
public class FadeTextureDisplay : MonoBehaviour
{
    /// <summary>�t�F�[�h�e�N�X�`���i�C���X�y�N�^����摜���w��j</summary>
    [SerializeField]
    private Texture2D texture;
    /// <summary>�J���[�i�����l�j</summary>
    [SerializeField]
    private Color initialVal;
    /// <summary>�J���[�i���B�l�j</summary>
    [SerializeField]
    private Color ReachingVal;
    /// <summary>���݂̃J���[</summary>
    private Color nowColorVal = Color.white;
    /// <summary>�t�F�[�h�A�E�g�������s���b</summary>
    [SerializeField]
    private float fadeoutSec;
    /// <summary>�t�F�[�h�A�E�g������ɂ��̏�Ԃ��ێ�����b</summary>
    [SerializeField]
    private float fadeoutKeepSec;
    /// <summary>�t�F�[�h�C���������s���b</summary>
    [SerializeField]
    private float fadeinSec;
    /// <summary>�t�F�[�h�C��������ɂ��̏�Ԃ��ێ�����b</summary>
    [SerializeField]
    private float fadeinKeepSec;
    /// <summary>�o�߂����b</summary>
    private float elapsedSec = 0;
    /// <summary>�t�F�[�h�L��</summary>
    private bool isFading = true;

    // =====================================================
    // GUI�`�掞�ɌĂ΂��
    // ���̒��̂��͍̂Ŏ�O�ɕ\�������B
    // =====================================================
    void OnGUI()
    {
        // �`��ʒu�A�`��T�C�Y�A�X�P�[�����O���[�h���w�肵�ăt�F�[�h�摜��`�悷��
        GUI.color = nowColorVal;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture, ScaleMode.ScaleToFit);
    }

    void Update()
    {
        if (isFading)
        {
            if (null == texture)
            {
                // Texture���A�^�b�`���Ă��Ȃ��ꍇ�̓t�F�[�h�s�̂��ߖ{�X�N���v�g���~����
                this.enabled = false;
            }
            if (0 >= fadeoutSec || 0 >= fadeinSec)
            {
                // �t�F�[�h�A�E�g�y�уt�F�[�h�C�����s���b�̐ݒ肪�s���ȏꍇ�̓t�F�[�h�s�̂��ߖ{�X�N���v�g���~����
                this.enabled = false;
            }

            // �o�ߎ��Ԃ𑪒�
            elapsedSec += Time.deltaTime;

            if (elapsedSec < fadeoutSec)
            {
                // �t�F�[�h�A�E�g���Ԓ���toColor�ւƏ��X�ɕω�������
                nowColorVal = Color.Lerp(initialVal, ReachingVal, elapsedSec / fadeoutSec);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec)
            {
                // �t�F�[�h�A�E�g�ێ����Ԓ���toColor�̐F���ێ�����
                nowColorVal = ReachingVal;
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec)
            {
                // �t�F�[�h�C�����Ԓ���fromColor�ւƏ��X�ɕω�������
                nowColorVal = Color.Lerp(ReachingVal, initialVal, (elapsedSec - fadeoutSec - fadeoutKeepSec) / fadeinSec);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec + fadeinKeepSec)
            {
                // �t�F�[�h�C���ێ����Ԓ���fromColor�̐F���ێ�����
                nowColorVal = initialVal;
            }
            else
            {
                // �S�Ă̏���������������o�ߎ��Ԃ�0�ɖ߂��ăt�F�[�h�A�E�g����ēx���s����
                elapsedSec = 0;
            }
        }
    }
}

