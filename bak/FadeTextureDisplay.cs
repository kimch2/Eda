using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System.Linq;

////////////////////////////////////////////////////////////////////////////////////////
//�@�֐����F�t�F�[�h�N���X
//�@�@�\�F�V�[���؂�ւ����Ƀt�F�[�h�C�� / �t�F�[�h�A�E�g������
//�@�p���FMonoBehaviour
//�@��ʁF�ʏ�N���X
//�@�A�^�b�`��F��ʃt�F�[�h��pCanvas�iCanvas_FadeDisplay�j
//�@�ێ����\�b�h�F
//�@���_�C���N�g�F�Ȃ�
//
//�@�ڍׁF
//�@�@�@�@�V�[���؂�ւ����Ƀt�F�[�h�C�� / �t�F�[�h�A�E�g������
//
//  �Ăяo����F
//
//�@�����F
//�@�@�@�@14.12.12 ����
//
////////////////////////////////////////////////////////////////////////////////////////
public class FadeTextureDisplay : MonoBehaviour
{
    /// <summary>�g�p����t�F�[�h�摜�̃I�u�W�F�N�g��</summary>
    public string chooseImageName;
    /// <summary>�t�F�[�h�A�E�g�������s���b</summary>
    public float fadeoutSec;
    /// <summary>�t�F�[�h�A�E�g������ɂ��̏�Ԃ��ێ�����b</summary>
    public float fadeoutKeepSec;
    /// <summary>�t�F�[�h�C���������s���b</summary>
    public float fadeinSec;
    /// <summary>�t�F�[�h�C��������ɂ��̏�Ԃ��ێ�����b</summary>
    public float fadeinKeepSec;
    /// <summary>�t�F�[�h�摜Image���A�^�b�`���Ă���I�u�W�F�N�g�z��</summary>
    private GameObject[] allFadeImages;
    /// <summary>�t�F�[�h�摜</summary>
    private Image fadeImage;
    /// <summary>�A���t�@�l�i�����l�F0�j</summary>
    [SerializeField]
    private Color initialAlphaVal;
    /// <summary>�A���t�@�l�i���B�l�F1�j</summary>
    [SerializeField]
    private Color reachingAlphaVal;
    /// <summary>���݂̃A���t�@�l</summary>
    private Color nowAlphaVal = Color.white;
    /// <summary>�o�߂����b</summary>
    private float elapsedSec = 0;
    /// <summary>�t�F�[�h�L��</summary>
    private bool isFading = true;

    void Awake()
    {
        // �S�V�[���Ŏg�p����@�\�̂��߉i���I�u�W�F�N�g�ɂ���
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        // �����ꂾ�ƕ����̃t�F�[�h�摜�������w��ł��Ȃ�����{�c
//      fadeImage = GetComponentInChildren<Image>();

        // �q�I�u�W�F�N�g�̒�����w�肵���I�u�W�F�N�g����Image�R���|���擾
        // �����ꂾ�Ǝw��摜�ȊO���A�N�e�B�u�����鏈�����ʓr�K�v������{�c
//      fadeImage = this.transform.Find(fadeImageName).GetComponentInChildren<Image>();

        // �S�Ẵt�F�[�h�摜�I�u�W�F�N�g���擾
        allFadeImages = GameObject.FindGameObjectsWithTag("FadeDisplayImages");
        // �Ƃ肠�����擾�����S�Ẵt�F�[�h�摜�I�u�W�F�N�g���A�N�e�B�u������
        AllFadeImagesStop();
        // ���̌�AfadeImageName�ɂ��w�肳�ꂽ�t�F�[�h�摜�I�u�W�F�N�g���A�N�e�B�u������
        FadeImageSet();
    }

    /// <summary>
    /// �t�F�[�h�摜�ݒ胁�\�b�h
    /// </summary>
    public void FadeImageSet()
    {
        if ("" == chooseImageName)
        {
            // �t�F�[�h�摜�����ݒ肳��Ă��Ȃ��ꍇ�̃G���[���b�Z�[�W���O���o���ď������~����
            Debug.Log("Unset ChooseImageName.");
            return;
        }

        foreach (GameObject fadeImageObject in allFadeImages)
        {
            if (chooseImageName == fadeImageObject.name)
            {
                // �w�肳�ꂽ�t�F�[�h�摜���̃I�u�W�F�N�g���A�N�e�B�u����Image�R���|���擾����
                fadeImageObject.SetActive(true);
                fadeImage = fadeImageObject.GetComponent<Image>();
                return;
            }
        }
    }

    /// <summary>
    /// �t�F�[�h�摜�����_���ݒ胁�\�b�h
    /// </summary>
    public void FadeImageRandomSet()
    {
        // �t�F�[�h�摜�z�񂩂�g�p����t�F�[�h�摜�������_���Ō��肷��
        int fadeImagesIndex = Random.Range(0, allFadeImages.Length +1);

        // ���肳�ꂽ�t�F�[�h�摜�I�u�W�F�N�g���A�N�e�B�u����Image�R���|���擾����
        allFadeImages[fadeImagesIndex].SetActive(true);
        fadeImage = allFadeImages[fadeImagesIndex].GetComponent<Image>();
    }

    /// <summary>
    /// �S�t�F�[�h�摜�I�u�W�F�N�g��~���\�b�h
    /// </summary>
    public void AllFadeImagesStop()
    {
        foreach (GameObject fadeImageObject in allFadeImages)
        {
            // �S�Ẵt�F�[�h�摜�I�u�W�F�N�g���A�N�e�B�u������
            fadeImageObject.SetActive(false);

            //��������Image�R���|�̂ݒ�~�B�R���|�����~�߂ăI�u�W�F�N�g�𐶂����Ă������R���Ȃ��̂Ń{�c�B
//          fadeImageObject.GetComponent<Image>().enabled = false;
        }
    }

    void Update()
    {
        if (isFading)
        {
            if (null == fadeImage)
            {
                // Image���A�^�b�`���Ă��Ȃ��ꍇ�̓t�F�[�h�s�̂��ߖ{�X�N���v�g���~����
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
                fadeImage.color = Color.Lerp(initialAlphaVal, reachingAlphaVal, elapsedSec / fadeoutSec);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec)
            {
                if (fadeImage.color != reachingAlphaVal)
                {
                    // �t�F�[�h�A�E�g�ێ����Ԓ��Ɍ��A���t�@�l�����B�l�ɂȂ��Ă��Ȃ��ꍇ�̓A���t�@�l�𓞒B�l�ɂ���
                    fadeImage.color = reachingAlphaVal;
                }
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec)
            {
                // �t�F�[�h�C�����Ԓ���fromColor�ւƏ��X�ɕω�������
                fadeImage.color = Color.Lerp(reachingAlphaVal, initialAlphaVal, (elapsedSec - fadeoutSec - fadeoutKeepSec) / fadeinSec);
            }
            else if (elapsedSec < fadeoutSec + fadeoutKeepSec + fadeinSec + fadeinKeepSec)
            {
                if (fadeImage.color != initialAlphaVal)
                {
                    // �t�F�[�h�C���ێ����Ԓ��Ɍ��A���t�@�l�������l�ɂȂ��Ă��Ȃ��ꍇ�̓A���t�@�l�������l�ɂ���
                    fadeImage.color = initialAlphaVal;
                }
            }
            else
            {
                // �S�Ă̏���������������o�ߎ��Ԃ�0�ɖ߂��ăt�F�[�h�A�E�g����ēx���s����
                elapsedSec = 0;
            }
        }
    }
}
