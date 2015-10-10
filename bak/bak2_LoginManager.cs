using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System.Linq;
using System;

public class LoginManager :
    MonoBehaviour,
    IMessageWriteToMW                                 // ���b�Z�[�W�E�B���h�E��������IF
{
    public AudioSource audioCompo;                      // �I�[�f�B�I�R���|
    public AudioClip clickSE;                           // OK�{�^���N���b�NSE
    private GameManager gameManager;                  // �}�l�[�W���R���|
    private GameObject warningParentGO;                 // ���b�Z�[�W�E�B���h�ECanvas
    /// <summary>LinkToXML�N���X</summary>
    private AppSettings appSettings;
    private Text warningText;                         // ���b�Z�[�W�E�B���h�E��Text�R���|
    public InputField guidField;                      // GUID�̃C���v�b�g�t�B�[���h
    private string nextScene = "UnitSelect";          // �J�ڐ�V�[����
    private string regisgerName = "Register";         // �J�ڐ�V�[����
    private bool IsWindow = false;                    // ���b�Z�[�W�E�B���h�E�\���L������t���O

    void Start()
    {
        // �}�l�[�W���R���|�擾
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // GUID���̓t�B�[���h�擾
        guidField = GameObject.FindWithTag("Login_InputField_Name").GetComponent<InputField>();
        // GUID��XML����ǂݏo���A���̓t�B�[���h�ɐݒ肷��
        var appSettings = new AppSettings();
        string userGuid = appSettings.GuidSetForInputFieldInLogin();
        guidField.text = userGuid;

        // ���[�j���O�E�B���h�E�̐eGO�����[�j���O�E�B���h�E�Ǘ��N���X���擾
        warningParentGO = GameObject.Find("Canvas_WarningWindow").GetComponent<WarningWindowActiveManager>().warningWindowParentGO;

        // �I�[�f�B�I�R���|���擾
        audioCompo = GameObject.Find("PlayersParent").transform.FindChild("SEPlayer").gameObject.GetComponent<AudioSource>();
        // TODO �{���̓��N���C���[�h�R���|�������g���ׂ��B��肭�����Ă���Ȃ������̂łƂ肠����
        if (null == audioCompo) audioCompo = GameObject.Find("PlayersParent").transform.FindChild("SEPlayer").gameObject.GetComponent<AudioSource>();

/*
                // LinkToXML�N���X���쐬
                appSettings = new AppSettings();
                string xmlFile = "var.xml";
                if (false == System.IO.File.Exists(xmlFile))
                {
                    // XML�t�@�C�����Ȃ���΍쐬����
                    appSettings.CreateXmlFile();
                }
                // GUID��XML���擾���AGUID�t�B�[���h�֐ݒ肷��
                guidField.text = appSettings.GuidSetForInputFieldInLogin();
                // ���[�U�[ID��txt�t�@�C������ǂݏo��
                var streamReader = new StreamReaderSingleLine();
                string filename = "iid.txt";
                userIDtxt = streamReader.ReadFromStream(filename);
                // �ǂݏo���ɐ��������ꍇ�A�ǂݏo����GUID���������̓t�B�[���h�ɐݒ�
                if ("null" != userIDtxt) guidField.text = userIDtxt;
*/
    }

    void Update()
    {
        // �G���^�[�L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // ���b�Z�[�W�E�B���h�E���\���̏ꍇ
            if (!IsWindow)
            {
                // ID�t�B�[���h�ɉ������͂���Ă��Ȃ��ꍇ
                if ("" == guidField.text)
                {
                    MessageWriteToWindow("�����́B\n���O�C��ID����͂��ĉ������B");
                    return;
                }
                // ���͂��ꂽID���uNameLess�v�̏ꍇ
                else if ("NameLess" == guidField.text)
                {
                    gameManager.userName = "NameLess";
                }
                // GUID������ɓ��͂��ꂽ�ꍇ
                else
                {
                    clickSE = (AudioClip)Resources.Load("Sounds/SE/Click7");
                    // �N���b�NSE��ݒ肨��эĐ�
                    audioCompo.PlayOneShot(clickSE);

                    // XML��胆�[�U�[��/GUID���擾���A�Q�[���}�l�[�W���[�ɐݒ肷��
//                    appSettings.UserStatusLoadFromXml();
                    // XML��胆�j�b�g�����擾
//                    appSettings.UnitStateLoadFromXml();
                    // XML���擾�������j�b�g�������Ƀ��j�b�gGO�̍쐬����у��j�b�g���X�g�ւ̊i�[���s��
//                    appSettings.UnitStateSetFromXml();

/*
                // ID�������Ĉ�v�����烍�[�h����
                // �����͂܂������ĂȂ�
                // ��v����ID���Ȃ���΃G���[�������b�Z�[�W�E�B���h�E�ŕ\��
                // ���͂��ꂽID���疼�O���t��������GM�̃t�B�[���h�Ɋi�[
                gameManager.userName = guidField.text.ToString();
*/
                }
                // �V�[���J�ڃ��\�b�h�R�[��
                NextScene();
            }
            // ���b�Z�[�W�E�B���h�E�\�����ɃG���^�[�L�[�������ꂽ�ꍇ
            else
            {
                // ���b�Z�[�W�E�B���h�E���A�N�e�B�u��
                warningParentGO.SetActive(false);

                // ���b�Z�[�W�E�B���h�E�\���L������t���O��ύX
                IsWindow = false;
            }
        }

        // ���b�Z�[�W�E�B���h�E���A�N�e�B�u��Ԃ̎��ɍ��N���b�N���ꂽ�ꍇ
        if (true == warningParentGO.activeSelf && Input.GetMouseButtonDown(0))
        {
            // ���b�Z�[�W�E�B���h�E���A�N�e�B�u��
            warningParentGO.SetActive(false);

            // ���b�Z�[�W�E�B���h�E�\���L������t���O��ύX
            IsWindow = false;
        }
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

    // =====================================
    // Registration�{�^������R�[������
    // ���W�X�g�V�[���֑J�ڂ���B
    // =====================================
    public void OnClickRegister()
    {
        // �V�[���J�ڎ��{
        Application.LoadLevel(regisgerName);
    }

    // =====================================
    // �V�[���J�ڃ��\�b�h
    // =====================================
    public void NextScene()
    {
        // Scene�J��
        // ̪��ޱ�Ď��ԁA̪��ޒ��ҋ@���ԁA̪��޲ݎ��ԁA�װ�A�J�ڐ�Pos���(Vector3)�A�J�ڐ漰�
        gameManager.GetComponent<FadeToScene>().FadeOut(0.1f, 0.4f, 0.1f, Color.black, nextScene);
    }
}
