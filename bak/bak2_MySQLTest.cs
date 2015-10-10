using UnityEngine;
using System.Collections;
using MySql.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Threading;
using System.Data; //TODO namespace�ɖ����Ɠ{����B����nuget�֌W�̂��������Ǐڍוs���B���߂��B

public class MySQLTest : MonoBehaviour
{
    // SQL Connector�@.NET����MySQL�f�[�^�x�[�X�ɃA�N�Z�X�ł���ADO.NET�h���C�o
    // MySqlCommand�@�@�f�[�^�̒ǉ�
    // MySqlConnection �R�l�N�V�����̍쐬
    // DbConnection�@�f�[�^�x�[�X�ւ̐ڑ����쐬
    // ExecuteReader�@CommandText��Connection�ɑ��M���ASqlDataReader���\�z����
    // IAsyncResult�@�񓯊������̗l�X�ȏ�ԁi�X�e�[�^�X�j������
    // BeginExecuteReader�@�񓯊��Ńf�[�^��ǂݏo���ASqlDataReader���\�z����
    // EndExecuteReader�@BeginExecuteReader�ɂ��񓯊�����������

    /// <summary>SQL�R�}���h</summary>
    private string SERVER = "127.0.0.1";            // �T�[�o�A�h���X
    private string DATABASE = "ubtSchema";          // �f�[�^�x�[�X��
    private string USERID = "root";                 // ���[�U��
    private string PASSWORD = "iG54bv7yH*gP";       // �p�X���[�h
    private string PORT = "3306";                   // �ڑ��|�[�g
    private string TABLENAME = "guidlist";          // �e�[�u����

    void Start()
    {
        SelectData();
    }

    void SelectData()
    {
        // SQL�R�}���h���쐬
        string conCmd =
                "server=" + SERVER + ";" +
                "database=" + DATABASE + ";" +
                "userid=" + USERID + ";" +
                "port=" + PORT + ";" +
                "password=" + PASSWORD;

        // SQL�R�l�N�V�������쐬
        MySqlConnection con = null;
        try
        {
            // SQL�֐ڑ������{����
            con = new MySqlConnection(conCmd);
            con.Open();
        }
        finally
        { 
        }

        // SQL���Ɛڑ������w�肵�A�f�[�^�A�_�v�^���쐬
        MySqlDataAdapter da = new MySqlDataAdapter("select * from guidlist", con);
    }
}
