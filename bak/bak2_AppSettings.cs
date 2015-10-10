using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// LINQ to XML�N���X
/// <para>�@LINQ to XML�ɂ��xml���̃f�[�^�𑀍삷��B</para>
/// </summary>
public class AppSettings : MonoBehaviour
{
    /// <summary>�}�l�[�W���[�R���|</summary>
    private GameManager gameManager;
    /// <summary>���[�U��</summary>
    private string userName;
    /// <summary>GUID</summary>
    private string guid;
    /// <summary>�Q�[������</summary>
    private int language;
    /// <summary>���j�b�gID</summary>
    private int[] unitidInXml = new int[16];
    /// <summary>�N���X</summary>
    private int[] classidInXml = new int[16];
    /// <summary>���j�b�g�ɂ������O</summary>
    private string[] unitNameInXml = new string[16];
    /// <summary>�A�r���e�B</summary>
    private int[] abilityInXml = new int[16];
    /// <summary>�G�������g</summary>
    private int[] elementInXml = new int[16];

    /// <summary>�R���X�^���g</summary>
    public AppSettings() { }

    void Start()
    {
        // �}�l�[�W���R���|���擾
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// GUID�t�B�[���h�ݒ胁�\�b�h
    /// <para>�@GUID��xml���擾���ALogin�V�[����GUID�t�B�[���h�ɐݒ肷�邽��</para>
    /// <para>�@�R�[�������\�b�h��GUID�l��Ԃ��B</para>
    /// </summary>
    public string GuidSetForInputFieldInLogin()
    {
        // xml�t�@�C�����擾
        XElement doc = XElement.Load("var.xml");

        // �v�f�ɑ΂���N�G�����쐬
        var query = from p in doc.Elements("UserParams")
                    select new
                    {
                        // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                        _guid = (string)p.Element("Guid")
                    };

        // xml���v�f���擾����
        foreach (var elem in query)
        {
            guid = elem._guid;
        }
        return guid;
    }

    /// <summary>
    /// ���[�U�֘A�p�����[�^�擾���\�b�h
    /// <para>�@���[�U����GUID�����[�U�֘A�̃p�����[�^��xml���擾����B</para>
    /// </summary>
    public void UserStatusLoadFromXml()
    {
        // �}�l�[�W���R���|���擾
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // xml�t�@�C�����擾
        XElement doc = XElement.Load("var.xml");

        // �v�f�ɑ΂���N�G�����쐬
        var query = from p in doc.Elements("UserParams")
        select new
        {
            // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
            _username = (string)p.Element("UserName"),
            _guid = (string)p.Element("Guid")
        };

        // xml���v�f���擾����
        foreach (var elem in query)
        {
            userName = elem._username;
            guid = elem._guid;
        }

        // xml���擾�������[�U�[����GUID���Q�[���}�l�[�W���[�R���|�ɐݒ肷��
        gameManager.userName = userName;
        gameManager.userGuid = guid;
    }

    /// <summary>
    /// ���j�b�g���X�g�擾���\�b�h
    /// <para>�@���j�b�g���X�g��xml���擾����B</para>
    /// <para>�@UnitStateSetFromXml�ƃZ�b�g�Ŏg�p����B</para>
    /// </summary>
    public void UnitStateLoadFromXml()
    {
        // xml�t�@�C�����擾
        XElement doc = XElement.Load("var.xml");

        // �v�f�ɑ΂���N�G�����쐬
        var query0 = from p
                        in doc.Elements("UnitStatus_0")
                        select new
                        {
                            // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                            _unitId = (int)p.Element("UnitID"),
                            _unitClass = (int)p.Element("UnitClass"),
                            _unitName = (string)p.Element("UnitName"),
                            _unitAbility = (int)p.Element("UnitAbility1"),
                            _unitElement = (int)p.Element("UnitElement")
                        };
        // xml���v�f���擾����
        foreach (var elem in query0)
        {
            unitidInXml[0] = elem._unitId;
            classidInXml[0] = elem._unitClass;
            unitNameInXml[0] = elem._unitName;
            abilityInXml[0] = elem._unitAbility;
            elementInXml[0] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query1 = from p
                        in doc.Elements("UnitStatus_1")
                    select new
                    {
                        // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                        _unitId = (int)p.Element("UnitID"),
                        _unitClass = (int)p.Element("UnitClass"),
                        _unitName = (string)p.Element("UnitName"),
                        _unitAbility = (int)p.Element("UnitAbility1"),
                        _unitElement = (int)p.Element("UnitElement")
                    };
        // xml���v�f���擾����
        foreach (var elem in query1)
        {
            unitidInXml[1] = elem._unitId;
            classidInXml[1] = elem._unitClass;
            unitNameInXml[1] = elem._unitName;
            abilityInXml[1] = elem._unitAbility;
            elementInXml[1] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query2 = from p
                        in doc.Elements("UnitStatus_2")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query2)
        {
            unitidInXml[2] = elem._unitId;
            classidInXml[2] = elem._unitClass;
            unitNameInXml[2] = elem._unitName;
            abilityInXml[2] = elem._unitAbility;
            elementInXml[2] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query3 = from p
                        in doc.Elements("UnitStatus_3")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query3)
        {
            unitidInXml[3] = elem._unitId;
            classidInXml[3] = elem._unitClass;
            unitNameInXml[3] = elem._unitName;
            abilityInXml[3] = elem._unitAbility;
            elementInXml[3] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query4 = from p
                        in doc.Elements("UnitStatus_4")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query4)
        {
            unitidInXml[4] = elem._unitId;
            classidInXml[4] = elem._unitClass;
            unitNameInXml[4] = elem._unitName;
            abilityInXml[4] = elem._unitAbility;
            elementInXml[4] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query5 = from p
                        in doc.Elements("UnitStatus_5")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query5)
        {
            unitidInXml[5] = elem._unitId;
            classidInXml[5] = elem._unitClass;
            unitNameInXml[5] = elem._unitName;
            abilityInXml[5] = elem._unitAbility;
            elementInXml[5] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query6 = from p
                        in doc.Elements("UnitStatus_6")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query6)
        {
            unitidInXml[6] = elem._unitId;
            classidInXml[6] = elem._unitClass;
            unitNameInXml[6] = elem._unitName;
            abilityInXml[6] = elem._unitAbility;
            elementInXml[6] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query7 = from p
                        in doc.Elements("UnitStatus_7")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query7)
        {
            unitidInXml[7] = elem._unitId;
            classidInXml[7] = elem._unitClass;
            unitNameInXml[7] = elem._unitName;
            abilityInXml[7] = elem._unitAbility;
            elementInXml[7] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query8 = from p
                        in doc.Elements("UnitStatus_8")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query8)
        {
            unitidInXml[8] = elem._unitId;
            classidInXml[8] = elem._unitClass;
            unitNameInXml[8] = elem._unitName;
            abilityInXml[8] = elem._unitAbility;
            elementInXml[8] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query9 = from p
                        in doc.Elements("UnitStatus_9")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query9)
        {
            unitidInXml[9] = elem._unitId;
            classidInXml[9] = elem._unitClass;
            unitNameInXml[9] = elem._unitName;
            abilityInXml[9] = elem._unitAbility;
            elementInXml[9] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query10 = from p
                        in doc.Elements("UnitStatus_10")
                     select new
                     {
                         // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xml���v�f���擾����
        foreach (var elem in query10)
        {
            unitidInXml[10] = elem._unitId;
            classidInXml[10] = elem._unitClass;
            unitNameInXml[10] = elem._unitName;
            abilityInXml[10] = elem._unitAbility;
            elementInXml[10] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query11 = from p
                        in doc.Elements("UnitStatus_11")
                      select new
                      {
                          // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xml���v�f���擾����
        foreach (var elem in query11)
        {
            unitidInXml[11] = elem._unitId;
            classidInXml[11] = elem._unitClass;
            unitNameInXml[11] = elem._unitName;
            abilityInXml[11] = elem._unitAbility;
            elementInXml[11] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query12 = from p
                        in doc.Elements("UnitStatus_12")
                      select new
                      {
                          // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xml���v�f���擾����
        foreach (var elem in query12)
        {
            unitidInXml[12] = elem._unitId;
            classidInXml[12] = elem._unitClass;
            unitNameInXml[12] = elem._unitName;
            abilityInXml[12] = elem._unitAbility;
            elementInXml[12] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query13 = from p
                        in doc.Elements("UnitStatus_13")
                      select new
                      {
                          // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xml���v�f���擾����
        foreach (var elem in query13)
        {
            unitidInXml[13] = elem._unitId;
            classidInXml[13] = elem._unitClass;
            unitNameInXml[13] = elem._unitName;
            abilityInXml[13] = elem._unitAbility;
            elementInXml[13] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query14 = from p
                        in doc.Elements("UnitStatus_14")
                      select new
                      {
                          // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xml���v�f���擾����
        foreach (var elem in query14)
        {
            unitidInXml[14] = elem._unitId;
            classidInXml[14] = elem._unitClass;
            unitNameInXml[14] = elem._unitName;
            abilityInXml[14] = elem._unitAbility;
            elementInXml[14] = elem._unitElement;
        }

        // �v�f�ɑ΂���N�G�����쐬
        var query15 = from p
                        in doc.Elements("UnitStatus_15")
                      select new
                      {
                          // �e�v�f�Ƃ���ɑΉ�����ϐ���ݒ�
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xml���v�f���擾����
        foreach (var elem in query15)
        {
            unitidInXml[15] = elem._unitId;
            classidInXml[15] = elem._unitClass;
            unitNameInXml[15] = elem._unitName;
            abilityInXml[15] = elem._unitAbility;
            elementInXml[15] = elem._unitElement;
        }
    }

    /// <summary>
    /// ���j�b�g���X�g�ݒ胁�\�b�h
    /// <para>�@���j�b�g���X�g�擾���\�b�h�iUnitStateLoadFromXml�j��xml���擾����</para>
    /// <para>�@���j�b�g����G�������g�����A�Q�[���}�l�[�W���[���̃��j�b�g���X�g�ɒǉ�����B</para>
    /// <para>�@�܂��A���j�b�gGO���쐬��UnitState�R���|���̃t�B�[���h�ւ̐ݒ���s���B</para>
    /// <para>�@UnitStateLoadFromXml�ƃZ�b�g�Ŏg�p����B</para>
    /// </summary>
    public void UnitStateSetFromXml()
    {
        for (int i = 0; 16 > i; i++)
        {
            // ���j�b�g�X�e�[�g�pGO�̃C���X�^���X���ƃR���|�擾
            GameObject unitGO = Instantiate(Resources.Load("UnitGO"), transform.position, Quaternion.identity) as GameObject;
            UnitState unitstate = unitGO.GetComponent<UnitState>();
            unitstate.unitID = unitidInXml[i];
            unitstate.classType = classidInXml[i];
            unitstate.unitName = unitNameInXml[i];
            unitstate.ability_A = abilityInXml[i];
            unitstate.element = elementInXml[i];
            gameManager.unitStateList.Add(unitstate);
        }
        /*
        // ID��UnitState�ƃ��j�b�g���X�g�ɐݒ�
        foreach (var id in unitidInXml)
        {
            // ���j�b�g�X�e�[�g�pGO�̃C���X�^���X���ƃR���|�擾
            unitGO = Instantiate(Resources.Load("UnitGO"), transform.position, Quaternion.identity) as GameObject;
            UnitState unitstate = unitGO.GetComponent<UnitState>();
            unitstate.unitID = id;
            gameManager.unitStateList.Add(unitstate);
        }
         */
    }

        /// <summary>
    /// xml�t�@�C���������\�b�h
    /// <para>�@�擾����xml�����݂��Ȃ��ꍇ�ɐ������s�����\�b�h�B</para>
    /// </summary>
    public void CreateXmlFile()
    {
        // xml�C���X�^���g���쐬
        XmlDocument document = new XmlDocument();
        XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
        XmlElement root = document.CreateElement("UBTProject");  // ���[�g�v�f
        document.AppendChild(declaration);                       // �w�肵���m�[�h���q�m�[�h�Ƃ��Ēǉ�
        document.AppendChild(root);

        // ���[�U�[���̗v�f���쐬
        XmlElement elementUserPrm = document.CreateElement("UserParams");
        root.AppendChild(elementUserPrm);
        XmlElement userName = document.CreateElement("UserName");
        userName.InnerText = "NONE";
        elementUserPrm.AppendChild(userName);
        XmlElement guID = document.CreateElement("Guid");
        guID.InnerText = "NONE";
        elementUserPrm.AppendChild(guID);

        // ���j�b�g���X�g�̗v�f���쐬
        for (int i = 0; 16 > i; i++)
        {
            XmlElement elementUnitSts0 = document.CreateElement("UnitStatus_" + i.ToString());
            root.AppendChild(elementUnitSts0);
            XmlElement UnitID_0 = document.CreateElement("UnitID");
            UnitID_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitID_0);
            XmlElement UnitClass_0 = document.CreateElement("UnitClass");
            UnitClass_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitClass_0);
            XmlElement UnitName_0 = document.CreateElement("UnitName");
            UnitName_0.InnerText = "NONE";
            elementUnitSts0.AppendChild(UnitName_0);
            XmlElement UnitAbility1_0 = document.CreateElement("UnitAbility1");
            UnitAbility1_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitAbility1_0);
            XmlElement UnitAbility2_0 = document.CreateElement("UnitAbility2");
            UnitAbility2_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitAbility2_0);
            XmlElement UnitElement_0 = document.CreateElement("UnitElement");
            UnitElement_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitElement_0);
        }
        // �t�@�C���֕ۑ�����
        document.Save("var.xml");
    }
}
