using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System.Linq;
using System;

public class OnClickOkForLogin :
    MonoBehaviour,
    IMessageWriteToMW                                 // ���b�Z�[�W�E�B���h�E��������IF
{
    public AudioClip clickSE;                         // OK�{�^���N���b�NSE
    public InputField nameField;                      // ���O�̃C���v�b�g�t�B�[���h
    private GameManager gameManager;                  // �}�l�[�W���R���|
    private GameObject warningParentGO;               // ���b�Z�[�W�E�B���h�ECanvas
    private Text warningText;                         // ���b�Z�[�W�E�B���h�E��Text�R���|
    private bool IsWindow = false;                    // ���b�Z�[�W�E�B���h�E�\���L������t���O
    private string nextScene = "UnitSelect";          // �J�ڐ�V�[����
    private AudioSource audioCompo;                   // �I�[�f�B�I�R���|

    /// <summary>�R���X�g���N�^</summary>
    private OnClickOkForLogin() { }

    void Start()
    {
        // �}�l�[�W���R���|�擾
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // ���O���̓t�B�[���h�擾
        nameField = GameObject.FindWithTag("Login_InputField_Name").GetComponent<InputField>();

        // ���[�j���O�E�B���h�E�̐eGO�����[�j���O�E�B���h�E�Ǘ��N���X���擾
        warningParentGO = GameObject.Find("Canvas_WarningWindow").GetComponent<WarningWindowActiveManager>().warningWindowParentGO;

        // �I�[�f�B�I�R���|�擾��OK�{�^���N���b�N��SE�̐ݒ�
        audioCompo = GameObject.Find("PlayersParent").transform.FindChild("SEPlayer").gameObject.GetComponent<AudioSource>();
        // TODO �{���̓��N���C���[�h�R���|�������g���ׂ��B��肭�����Ă���Ȃ������̂łƂ肠����
        if (null == audioCompo) audioCompo = GameObject.Find("PlayersParent").transform.FindChild("SEPlayer").gameObject.GetComponent<AudioSource>();
        clickSE = (AudioClip)Resources.Load("Sounds/SE/Click7");
    }

    // =====================================
    // ���b�Z�[�W�E�B���h�E��������IF
    // ���b�Z�[�W�E�B���h�E��Text�R���|�ɕ�������������
    // =====================================
    public void MessageWriteToWindow(string a)
    {
        // ���b�Z�[�W�E�B���h�E���A�N�e�B�u��
        warningParentGO.SetActive(true);

        // �e�L�X�g�R���|���擾
        warningText = warningParentGO.transform.FindChild("WarningText").gameObject.GetComponent<Text>();

        // ���b�Z�[�W�E�B���h�E�\���L������t���O��ύX
        IsWindow = true;

        // ���b�Z�[�W�\��
        warningText.text = a;
    }

    // -------------------------------------------------------------------
    // OK�{�^�����N���b�N��������OK�{�^����OnClick����R�[������A
    // ���r�[�֑J�ڂ���B
    // -------------------------------------------------------------------
    public void OnClickOK()
    {
        // ���b�Z�[�W�E�B���h�E���\���̏ꍇ
        if (!IsWindow)
        {
            // ID�t�B�[���h�ɉ������͂���Ă��Ȃ��ꍇ
            if ("" == nameField.text)
            {
                MessageWriteToWindow("�����́B\n���O�C��ID����͂��ĉ������B");
                return;
            }
            // ���͂��ꂽID���uNameLess�v�̏ꍇ
            else if ("NameLess" == nameField.text)
            {
                gameManager.userName = "NameLess";
            }
            // ID������ɓ��͂��ꂽ�ꍇ
            else
            {
                // LinkToXML�N���X���쐬
                var t = new AppSettings();

                string xmlFile = "sample.xml";
                if (false == System.IO.File.Exists(xmlFile))
                {
                    // �t�@�C�����Ȃ���΍쐬����
                    t.CreateXmlFile();
                }

                // ID�������Ĉ�v�����烍�[�h����
                // �����͂܂������ĂȂ�
                // ��v����ID���Ȃ���΃G���[�������b�Z�[�W�E�B���h�E�ŕ\��
                // ���͂��ꂽID���疼�O���t��������GM�̃t�B�[���h�Ɋi�[
                gameManager.userName = nameField.text.ToString();
            }

            // ���񂩂�̓��͂����������邽�߁A���͂��ꂽ��������t�@�C���ɏ����o��
            var streamWriter = new StreamWriterSingleLine();
            string fileName = "iid.txt";
            string filetxt = nameField.text;
            bool result = streamWriter.WriteToStream(fileName, filetxt);

            // �V�[���J�ڃ��\�b�h�R�[��
            NextScene();
        }
        // ���b�Z�[�W�E�B���h�E���\������Ă���ꍇ
        else
        {
            // ���b�Z�[�W�E�B���h�E���A�N�e�B�u��
            warningParentGO.SetActive(false);

            // ���b�Z�[�W�E�B���h�E�\���L������t���O��ύX
            IsWindow = false;
        }
    }

    // -------------------------------
    // �V�[���J�ڃ��\�b�h
    // -------------------------------
    public void NextScene()
    {
        // �N���b�NSE��ݒ肨��эĐ�
        audioCompo.PlayOneShot(clickSE);

        // Scene�J��
        // ̪��ޱ�Ď��ԁA̪��ޒ��ҋ@���ԁA̪��޲ݎ��ԁA�װ�A�J�ڐ�Pos���(Vector3)�A�J�ڐ漰�
        gameManager.GetComponent<FadeToScene>().FadeOut(0.1f, 0.4f, 0.1f, Color.black, nextScene);
    }
}
