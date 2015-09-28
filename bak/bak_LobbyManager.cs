using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;    //CP��pHashtable

// =======================================================================================
// ���r�[�V�[���}�l�[�W���[
//
// ���r�[�V�[���ŋN������A���r�[�ɓ�������
// ���[���ւ̓����̓o�g���t�B�[���h�V�[���ōs���B
//
// �����{�^���͎擾��A����A�N�e�B�u�������r�[�����m����
// �A�N�e�B�u�����s���i���r�[�����O�Ƀ{�^����������鎖��}�~����j�B
//
// ���[��CP�ݒ�_�@�F�o�g���t�B�[���h�V�[���ɂ�CreateRoom�̒��O
// �v���C���[CP�ݒ�_�@�F���r�[�V�[���N�������Start���\�b�h��
//
// �y���r�[����у��[�������t���[ (����Ƃ�) �z
// Lobby�V�[���N������Ƀ��r�[����
// ��
// �����{�^��������LoadLevel�Ńo�g���t�B�[���h�V�[���֑J��
// ��
// �o�g���t�B�[���h�V�[���N�������CreateRoom+JoinRoom�œ���
//
// =======================================================================================
public class LobbyManager : MonoBehaviour
{
    private GameManager gameManager;         // �}�l�[�W���R���|
    private GameObject canVas;               // �Q�[���I�u�W�F�N�g"Canvas"
    private GameObject insideRoomButton;     // �����{�^���I�u�W�F�N�g
    private Text playerAllText;              // �S���[�U�[���\���p�e�L�X�g�R���|
    private Text roomAllText;                // �S���[�����\���p�e�L�X�g�R���|

    // ---- �v���C���[CP�p�t�B�[���h ----
    public string name = "Guest";            // ���[�U�[��
    public string rank = "";                 // �����N
    public int battlePoint = 0;              // �o�g���|�C���g
    public int battleCnt = 0;                // �퓬��

    void Awake()
    {
        // ���r�[�ɓ������邽�߃}�X�^�[�T�[�o�[�֐ڑ�
        PhotonNetwork.ConnectUsingSettings("v0.1");
    }

	void Start ()
    {
        // �}�l�[�W���R���|�擾
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // �Q�[���I�u�W�F�N�g"Canvas"�擾
        canVas = GameObject.FindWithTag("Canvas");

        // �����{�^���I�u�W�F�N�g�擾����A�N�e�B�u��
        insideRoomButton = GameObject.FindWithTag("InsideRoomButton");
        insideRoomButton.SetActive(false);

        // �S���[�U���\��Text�R���|���擾
        playerAllText = GameObject.FindWithTag("Roby_PlayersNum").GetComponent<Text>();

        // �S���[�����\��Text�R���|���擾
        roomAllText = GameObject.FindWithTag("Roby_RoomsNum").GetComponent<Text>();
        

        // �v���C���[CP�v�f���`
        gameManager.customPropeties = new Hashtable()
                                        {
                                            { "UserName", name },
                                            { "BP", battlePoint },
                                            { "BattleCnt", battleCnt },
                                            { "Rank", rank }
                                        };

        // �v���C���[CP��ݒ�
        PhotonNetwork.SetPlayerCustomProperties(gameManager.customPropeties);

        // �Q�[�����v���C���[���擾���\�b�h���R�[��
        StartCoroutine(GetPlayerAll());
	}

    // -------------------------------------------------------------
    // �}�X�^�[�T�[�o�[�̃��r�[�ɓ������ꍇ�ɃR�[�������
    // ���r�[�ɓ�������Ƃ肠���������𐶐�����
    // -------------------------------------------------------------
    void OnJoinedLobby()
    {
        Debug.Log("���r�[�ɓ���");

        // ���r�[�����m���A�����{�^�����A�N�e�B�u��
        insideRoomButton.SetActive(true);
    }

    // -------------------------------------------------------------------
    // �����{�^�����N���b�N�������ɓ����{�^����OnClick����R�[������A
    // �o�g���t�B�[���h�֑J�ڂ���B
    // -------------------------------------------------------------------
    public void RoomIn()
    {
        Application.LoadLevel("BattleStage");
    }

    // -------------------------------------------------------------------
    // ���j�b�g�Ґ��{�^�����N���b�N�������Ƀ��j�b�g�Ґ��{�^����OnClick����
    // �R�[������A���j�b�g�Ґ��V�[���֑J�ڂ���B
    // �������������������@�{�c���\�b�h�@��������������������
    // -------------------------------------------------------------------
    public void UnitForm()
    {
        // ���C���T�[�o�����x�ؒf
        PhotonNetwork.Disconnect();
        Application.LoadLevel("UnitForm");
    }

    // -------------------------------------------------------------
    // �S�v���C���[���擾���\�b�h
    // �S�Q�[�����̃v���C���[�����擾���AText�R���|�ɕ\������
    // Start()���\�b�h����R�[������A�R���[�`���Ƃ��Ē���I�ɍX�V����
    // -------------------------------------------------------------
    private IEnumerator GetPlayerAll()
    {
        while (true)
        {
            // �v���C���[�����擾
            playerAllText.text = PhotonNetwork.countOfPlayers.ToString();

            // ���[�������擾
            roomAllText.text = PhotonNetwork.countOfRooms.ToString();

            // �ꎞ��~
            yield return new WaitForSeconds(20.0f);
        }
    }
}
