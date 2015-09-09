using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System.Linq;

public class UnitSelectButtonSol : 
    MonoBehaviour,
    IPointerEnterHandler,					// �C�x���g�n���h��IF
    IPointerExitHandler						// �C�x���g�n���h��IF
{
    public int mouseOverJug = 0;            // �}�E�X�I�[�o�[����t���O

    // -----------------------------------
    // �J�[�\���G���g���[���\�b�h
    // -----------------------------------
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �}�E�X�I�[�o�[����t���O��ON
        mouseOverJug = 1;

        // �}�E�X�N���b�N�p�C�x���g�n���h�����R�[��
        StartCoroutine("MouseClickHandler");
    }

    // -----------------------------------
    // �J�[�\���G�X�P�[�v���\�b�h
    // -----------------------------------
    public void OnPointerExit(PointerEventData eventData)
    {
        // �}�E�X�I�[�o�[����t���O��OFF
        mouseOverJug = 0;

        // �}�E�X�N���b�N�p�C�x���g�n���h�����~
        StopCoroutine("MouseClickHandler");
    }

    // -----------------------------------
    // �}�E�X�N���b�N���胁�\�b�h
    // -----------------------------------
    public IEnumerator MouseClickHandler()
    {
        // �i�����[�v�i�������A�}�E�X�I�[�o�[�𔲂�����return����j
        while (1 == mouseOverJug)
        {
            // �}�E�X���N���b�N���ꂽ�ꍇ
            if (Input.GetMouseButtonDown(0))
            {
            }
            // �}�E�X�E�N���b�N���ꂽ�ꍇ
            else if (Input.GetMouseButtonDown(1))
            {
            }

            // �R���[�`���𔲂���
            yield return null;
        }
    }
}
