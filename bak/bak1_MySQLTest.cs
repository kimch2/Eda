using UnityEngine;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Threading;

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
    private string SERVER = "localhost";
    private string DATABASE = "MySQL56";
    private string USERID = "root";
    private string PASSWORD = "iG54bv7yH*gP";
    private string PORT = "3306";
    private string TABLENAME = "hoge";

    void Start()
    {
        StartCoroutine(SelectData());
    }

    IEnumerator SelectData()
    {
        // SQL�R�}���h���쐬
        string conCmd =
                "server=" + SERVER + ";" +
                "database=" + DATABASE + ";" +
                "userid=" + USERID + ";" +
//                "port=" + PORT + ";" +
                "password=" + PASSWORD;

        // SQL�R�l�N�V�������쐬
        MySqlConnection con = null;
        try
        {
            // SQL�֐ڑ������{����
            con = new MySqlConnection(conCmd);
            con.Open();
        }
        finally { }

/* TODO�@�悭������񂯂Ǔ����Ȃ�����p�b�`���O��finally����
        catch (MySqlException ex)
        {
            Debug.Log(ex.ToString());
        }
*/
        // �f�[�^��ǉ�����INSERT���́A�ʏ��MySqlCommand�N���X���g���܂��B
        // �R�}���h���쐬
        string selCmd = "SELECT * FROM TABLENAME LIMIT 0, 1200;";
        MySqlCommand cmd = new MySqlCommand(selCmd, con);

        // �񓯊��������J�n
        IAsyncResult iAsync = cmd.BeginExecuteReader();

        // �񓯊��ɂ��S�f�[�^�擾�����܂ő҂����킹��
        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }

        // �ꉞ�F
        // ��L��while�Ŋ�����҂ȊO�ɁABegin�ɂ��񓯊��J�n���ɃI�[�o�[���[�h��
        // cmd.BeginExecuteReader(new AsyncCallback(AsyncCallbackMethod), cmd);
        // �Ƃ��Ĕ񓯊�������������R�[���o�b�N���\�b�h���ĂсA���̒��Ŋ���EndExcuteReader
        // ���ĂԎ����ł���B
        // �������́A�񓯊��������I������ۂɃR�[���o�b�N����郁�\�b�h�B
        // ��������IAsyncResult.AsyncState�I�u�W�F�N�g�Ƃ��Ď擾�ł���I�u�W�F�N�g�B
        // �ŁA�R�[���o�b�N���\�b�h��void AsyncCallbackMethod(IAsyncResult result)
        // result�ɂ͑������Ŏw�肵��cmd������B

        // �񓯊���������������
        MySqlDataReader rdr = cmd.EndExecuteReader(iAsync);

        // �擾�����f�[�^����ID�𓾂Ă݂�
        while (rdr.Read())
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("ID")))
            {
                Debug.Log ( "ID : " + rdr.GetString ("ID") );
            }
        }

        // �S���\�[�X���N���[�Y����щ������
        rdr.Close();
        rdr.Dispose();
        con.Close();
        con.Dispose();
    }
}
