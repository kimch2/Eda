// ================================
// �L�����I�u�W�F�N�g�i��l���j�i�v���n�u�j
// �@�\�F��l���A�j����\������
// CharacterController�R���|�[�l���g������
// 
// �N���_�@�F�v���C���[�I�u�W�F�N�g�\�����i�펞�j
// 
// ���_�C���N�g�F�R���e�i�N���X�iLV��HP�ȂǊe�p�����[�^�l�̎Q�ƁA�㏑���j
// �@�@�@�@�@�@�@ ���l�A�C�x���g�I�u�W�F�N�g�Ȃ�
// ================================
using UnityEngine;
using System.Collections;

public class Player_2D : MonoBehaviour, IGetPutCont {

	// �A�j���R���|�[�l���g�擾�p�X�^�b�N
	Animator anim;

	// ==================
	// �������s�֐�
	// �@�\�F�L�����R���擾
	// �@�@�@�p�����[�^�ǂݍ���
	// ==================
	void  Start ()
	{
		// �A�j���R���|�[�l���g�擾
		anim = GetComponent<Animator>();
	}// void Start()

	void  Update (){
		
		// ����\����Inout_Box�Ƀq�b�g���Ă��Ȃ��ꍇ
		if(ctrl == 1 && obj_hitflag == 0)
		{
			// ���E�����L�[����i-1:���L�[�@1�F�E�L�[�j
			int input_facing = (int) Input.GetAxisRaw("Horizontal");
			
			// �㉺�����L�[����i-1:���L�[�@1�F��L�[�j
			int input_facing2 = (int) Input.GetAxisRaw("Vertical");
			
			// �E�Ɉړ�����ꍇ
			if(input_facing == 1)
			{
				// anim.SetBool�ŃA�j���X�e�[�^�X�ϐ��𐧌�
				transform.localScale = new Vector2(1, 1);
				anim.SetBool("Tina_Down",false);
				anim.SetBool("Tina_Up",false);
				anim.SetBool("Tina_FwdBack",true);
			//���Ɉړ�����ꍇ
			}else if(input_facing == -1)
			{
				// anim.SetBool�ŃA�j���X�e�[�^�X�ϐ��𐧌�
				transform.localScale = new Vector2(-1, 1);
				anim.SetBool("Tina_Down",false);
				anim.SetBool("Tina_Up",false);
				anim.SetBool("Tina_FwdBack",true);
			}
}


