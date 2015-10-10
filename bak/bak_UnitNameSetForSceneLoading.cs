using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // �R���N�V�����N���X�̒�`�ɕK�v
using System.Linq;

public class UnitNameSetForSceneLoading : MonoBehaviour
{
    /// <summary>�N���X���\���p�e�L�X�g�t�B�[���h���X�g</summary>
    public List<Text> ClassNameList = new List<Text>();
    /// <summary>���j�b�g���\���p�e�L�X�g�t�B�[���h���X�g</summary>
    public List<Text> UnitNameList = new List<Text>();
    /// <summary>�}�l�[�W���R���|</summary>
    private GameManager gameManager;
    /// <summary>Canvas</summary>
    private GameObject canVas;

    /// <summary>�R���X�g���N�^</summary>
    private UnitNameSetForSceneLoading() { }

    void Start()
    {
        // �}�l�[�W���R���|���擾
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // �Q�[���I�u�W�F�N�g"Canvas"�擾
        canVas = GameObject.FindWithTag("Canvas");

        // �S���j�b�g�����̃��j�b�g���\���p�e�L�X�g�R���|���擾���A���X�g�Ɋi�[
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName0").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName1").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName2").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName3").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName4").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName5").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName6").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName7").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName8").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName9").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName10").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName11").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName12").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName13").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName14").GetComponent<Text>());
        UnitNameList.Add(GameObject.FindWithTag("Nam_UnitName15").GetComponent<Text>());

        int unitID = 0;
        foreach (Text t in UnitNameList)
        {
            // ���j�b�g�������j�b�g�l�[���\���g�ɐݒ�
            t.text = gameManager.unitStateList[unitID].unitName;

            unitID++;
        }
    }
}
