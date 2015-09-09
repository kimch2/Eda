using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System.Linq;

////////////////////////////////////////////////////////////////////////////////////////
//�@�֐����F�I�u�W�F�N�g�J���[Palfx�N���X
//�@�@�\�F�I�u�W�F�N�g�̃J���[��ύX����mugen�Ō���Palfx
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
public class AddObjectColor : MonoBehaviour
{
    private Texture2D texture;
    private string sequence = null;
    private Color from = Color.white;
    private Color to;
    private Color now;
    private Color setCloro;
    private float time;
    private float fadewait;
    private float fadeinTime;
    private GameObject targetObject;
    private Image targetImage;

    public void Update()
    {
        if (targetImage) targetImage.color = now;   // �^�[�Q�b�g�I�u�W�F�N�g�̃J���[��Palfx
    }

    // =====================================================
    // �@
    // Palfx�J�n���\�b�h
    // �t�F�[�h�A�E�g���ԁA�t�F�[�h���ҋ@���ԁA�t�F�[�h�C�����ԁA�t�F�[�h�J���[�A�Ώۂ�GameObject
    // =====================================================
    public void PalfxStart(float t_time, float f_wait, float a_time, Color t_color, GameObject go)
    {
        to = setCloro = t_color;
//        from.a = 0;
        time = t_time;
        fadewait = f_wait;
        targetObject = go;
        targetImage = targetObject.GetComponent<Image>();
        fadeinTime = a_time;
        StartCoroutine("PalfxBegin");
    }

    // =====================================================
    // �B
    // ���򑝉��R���[�`��
    // ���򑝉����������{����
    // =====================================================
    public IEnumerator PalfxBegin()
    {
        float now_time = 0;
        while (0 < time && now_time < time)
        {
            now_time += Time.deltaTime;
            now = Color.Lerp(from, to, now_time / time);
            yield return 0;
        }

        // �t�F�[�h�A�E�g�������̃J���[�����J���[�ɐݒ�
        now = to;

        // �t�F�[�h�C���O�̈ꎞ��~���\�b�h���R�[��
        StartCoroutine(BeforePalfx(fadewait, fadeinTime, targetObject));
    }

    // =================================================
    // �C
    // �t�F�[�h�C���O�ꎞ��~���\�b�h
    // �t�F�[�h����~���ԁA�t�F�[�h�C�����ԁA�J�ڐ�V�[��
    // =================================================
    public IEnumerator BeforePalfx(float waittime, float fadein, GameObject go)
    {
        // �Ó]������A�ꎞ��~
        yield return new WaitForSeconds(waittime);

        // �ꎞ��~
        yield return new WaitForSeconds(0.1f);

        // �t�F�[�h�A�E�g��̓t�F�[�h�C�����邽��
        // �t�F�[�h�C�����\�b�h���R�[��
        BeforePalfx(fadein, setCloro);
    }

    // =====================================================
    // �D
    // �t�F�[�h�C���J�n���\�b�h
    // �R�[�����Ɏw�肵���t�F�[�h�C������, �t�F�[�h�J���[
    // ��ݒ肵�AFadeUpdateFromFadeIn���\�b�h��
    // StartSequence���\�b�h����R�[������
    // =====================================================
    public void BeforePalfx(float t_time, Color t_color)
    {
        // �w�肵���t�F�[�h�J���[�ƃt�F�[�h���Ԃ�ݒ�
        to = t_color;
//        to.a = 0;
        time = t_time;
        // �t�F�[�h�C�����\�b�h���R�[��
        StartCoroutine("Palfx");
        return;
    }

    // =====================================================
    // �E
    // �t�F�[�h�C�����{�R���[�`��
    // �t�F�[�h�C�������{����
    // =====================================================
    public IEnumerator Palfx()
    {
        float now_time = 0;
        while (0 < time && now_time < time)
        {
            now_time += Time.deltaTime;
            now = Color.Lerp(to, from, now_time / time);
            yield return 0;
        }

        // �t�F�[�h�A�E�g�������̃J���[�����J���[�ɐݒ�
        now = from;
    }

    // =====================================================
    // �I�u�W�F�N�g�J���[�ύX���\�b�h
    // ���͂��ꂽ�I�u�W�F�N�g�̃J���[��ύX����
    // �Ƃ肠�����g��Ȃ�
    // =====================================================
    private void ChangeColorOfGameObject(GameObject targetObject, Color color)
    {

        //���͂��ꂽ�I�u�W�F�N�g��Renderer��S�Ď擾���A����ɂ���Renderer�ɐݒ肳��Ă���SMaterial�̐F��ς���
        foreach (Renderer targetRenderer in targetObject.GetComponents<Renderer>())
        {
            foreach (Material material in targetRenderer.materials)
            {
                material.color = color;
            }
        }

        //���͂��ꂽ�I�u�W�F�N�g�̎q�ɂ����l�̏������s��
        for (int i = 0; i < targetObject.transform.childCount; i++)
        {
            ChangeColorOfGameObject(targetObject.transform.GetChild(i).gameObject, color);
        }

    }
}

