
using UnityEngine;
using System.Collections;

public class Test : Photon.MonoBehaviour {


    private float speed = 0.5f;             // �v���C���[���s���x
    private Vector2 vel = Vector2.zero;     // �L�����ړ��l�X�^�b�N
    // TransForm�R���|
    // ��Transform�͓����I��GetComponent���Ă��Ė���this�w�肷��ƃR�X�g��������(�ᑬ)
    // ���߃L���b�V�����Ă���
    public Transform _cachedTransform;
    public Transform cachedTransform
    {
        get
        {
            if (null == _cachedTransform)
            {
                _cachedTransform = this.transform;
            }
            return _cachedTransform;
        }
    }

	void Update ()
    {
        // �����̃I�u�W�F�N�g�̏ꍇ
        if (photonView.isMine)
        {
            // ���E�����L�[����i-1:���L�[�@1�F�E�L�[�j
            int input_facing = (int)Input.GetAxisRaw("Horizontal");

            // �㉺�����L�[����i-1:���L�[�@1�F��L�[�j
            int input_facing2 = (int)Input.GetAxisRaw("Vertical");

            // �ړ����{
            cachedTransform.position = vel;

            // �E�Ɉړ�����ꍇ
            // ���E�̃A�j���͕ύX���Ȃ�����localscale�Ō��������ς���
            if (input_facing == 1)
            {
                // �E�Ɉړ�
                vel.x += speed;

                //���Ɉړ�����ꍇ
            }
            else if (input_facing == -1)
            {
                // ���Ɉړ�
                vel.x -= speed;
            }

            //��Ɉړ�����ꍇ
            if (input_facing2 == 1)
            {
                // ��Ɉړ�
                vel.y += speed;
            }
            //���Ɉړ�����ꍇ
            else if (input_facing2 == -1)
            {
                // ���Ɉړ�
                vel.y -= speed;
            }
        }
    }
}

