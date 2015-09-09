using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System.Linq;

public class UnitSelectButtonSol : 
    MonoBehaviour,
    IUnitSelect,                            //  ���j�b�g�Z���N�gIF
    IPointerEnterHandler,
    IPointerExitHandler
{
    private GameManager gameManager;        // �}�l�[�W���R���|
    public int mouseOverJug = 0;            // �}�E�X�I�[�o�[����t���O

    // ----------------------------------------
    // Start���\�b�h
    // ----------------------------------------
    void Start()
    {
        // �}�l�[�W���R���|�擾
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // -----------------------------------
    // �J�[�\���G���g���[���\�b�h
    // �I�u�W�F�N�g��Ƀ}�E�X�J�[�\�����I�[�o�[�������ɃR�[������A
    // �}�E�X�{�^���̍��E�𔻒肷�郁�\�b�h���R���[�`���ŃR�[������B
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
    // �I�u�W�F�N�g�ォ��}�E�X�J�[�\���������[�X���ꂽ���ɃR�[������A
    // �N�����Ă����R���[�`�����~������B
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
    // �I�u�W�F�N�g��Ƀ}�E�X�I�[�o�[����Ă��鎞�ɃR�[������A
    // �}�E�X�̉E�N���b�N�ƍ��N���b�N�ŕʂ̏������s���B
    // -----------------------------------
    public IEnumerator MouseClickHandler()
    {
        // �i�����[�v�i�������A�}�E�X�I�[�o�[�𔲂�����return����j
        while (1 == mouseOverJug)
        {
            // �}�E�X���N���b�N���ꂽ�ꍇ
            if (Input.GetMouseButtonDown(0))
            {
				// ����
            }
            // �}�E�X�E�N���b�N���ꂽ�ꍇ
            else if (Input.GetMouseButtonDown(1))
            {
				// ����
            }

            // �R���[�`���𔲂���
            yield return null;
        }
    }
}
