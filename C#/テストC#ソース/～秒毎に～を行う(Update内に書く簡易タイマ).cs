
        float timer;			// �^�C�}�[
        int waitingTime = 10;		// �}�[�W���l

	void Updaste()
	{
	    // �^�C�}�[�N��
	    timer += Time.deltaTime;

	    // �}�[�W���l�ɂȂ����ꍇ
	    if (timer > waitingTime)
	    {
	        // --- ��肽������ --- //

	        timer = 0; // �^�C�}�[�����Z�b�g
	    }
	}


