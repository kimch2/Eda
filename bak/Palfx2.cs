using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System.Linq;

////////////////////////////////////////////////////////////////////////////////////////
//�@�֐����F�I�u�W�F�N�g�J���[Palfx�N���X
//�@�@�\�F�I�u�W�F�N�g�̎��}�e���A���̌���(�P�x)��ύX����
//�@�p���FMonoBehaviour
//�@��ʁF�ʏ�N���X
//�@�A�^�b�`��FGameObject
//�@�ێ����\�b�h�F
//�@���_�C���N�g�F�Ȃ�
//
//�@�ڍׁF
//�@�@�@�@
//
//  �Ăяo����F
//
//�@�����F
//
////////////////////////////////////////////////////////////////////////////////////////
public class Palfx2 : MonoBehaviour
{
    /// <summary>�t�F�[�h�A�E�g�������s���b</summary>
    public float fadeoutSec;
    /// <summary>�t�F�[�h�A�E�g������ɂ��̏�Ԃ��ێ�����b</summary>
    public float fadeoutKeepSec;
    /// <summary>�t�F�[�h�C���������s���b</summary>
    public float fadeinSec;
    /// <summary>�t�F�[�h�C��������ɂ��̏�Ԃ��ێ�����b</summary>
    public float fadeinKeepSec;
    /// <summary>�P�x��ς���Material</summary>
    public Material flashingMaterial;
    /// <summary>�����̗L��</summary>
    public bool isFlashing = true;
    /// <summary>�P�x�i�����l�j</summary>
    private Color initialVal = new Color(0, 0, 0);
    /// <summary>�P�x�i���B�l�j</summary>
    private Color ReachingVal = new Color(0.7f, 0.7f, 0.7f);
    /// <summary>�o�߂����b</summary>
    private float elapsedSec = 0;

    void Start()
    {
        // �Q�l�Fhttp://dnasoftwares.hatenablog.com/entry/2015/03/19/100108
        flashingMaterial.EnableKeyword("_Emission");
    }

    public void Update()
    {
        if (isFlashing)
        {
            if ("Default UI Material" == flashingMaterial.name)
            {
                // Image�R���|���}�e���A���������Ă��Ȃ��ꍇ�͔����s�̂��ߖ{�X�N���v�g���~����
                this.enabled = false;
            }
            if (0 >= fadeoutSec || 0 >= fadeinSec)
            {
	            // �t�F�[�h�A�E�g�y�уt�F�[�h�C�����s���b�̐ݒ肪�s���ȏꍇ�͔����s�̂��ߖ{�X�N���v�g���~����
                this.enabled = false;
            }

            // �o�ߎ��Ԃ𑪒�
            elapsedSec += Time.deltaTime;

            if (elapsedSec < fadeoutSec)
            {
                // �t�F�[�h�A�E�g���Ԓ���toColor�ւƏ��X�ɕω�������
                flashingMaterial.SetColor("_EmissionColor", Color.Lerp(initialVal, ReachingVal, elapsedSec / fadeoutSec));
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec)
            {
                // �t�F�[�h�A�E�g�ێ����Ԓ���toColor�̐F���ێ�����
                flashingMaterial.SetColor("_EmissionColor", ReachingVal);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec)
            {
                // �t�F�[�h�C�����Ԓ���fromColor�ւƏ��X�ɕω�������
                flashingMaterial.SetColor("_EmissionColor", Color.Lerp(ReachingVal, initialVal, (elapsedSec - fadeoutSec - fadeoutKeepSec) / fadeinSec));
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec + fadeinKeepSec)
            {
                // �t�F�[�h�C���ێ����Ԓ���fromColor�̐F���ێ�����
                flashingMaterial.SetColor("_EmissionColor", initialVal);
            }
            else
            {
                // �S�Ă̏���������������o�ߎ��Ԃ�0�ɖ߂��ăt�F�[�h�A�E�g����ēx���s����
                elapsedSec = 0;
            }
        }
    }
}

