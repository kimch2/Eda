
    private AddObjectColor palfxColor;		// ����t�^�N���X
    private GameObject palfxTarget;		    // ����t�^�ΏۃI�u�W�F�N�g
    float timer;							// ����t�^�p�^�C�}�[
    float waitingTime = 3.0f;				// ����t�^�p�^�C�}�[�}�[�W���l

    void Update()
    {
		// ����t�^����
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            // �I�u�W�F�N�g�Ɍ����t�^�i��AddObjectColor�N���X�͓��t�H���_��AddObjectColor.cs���Q�Ɓj
            palfxColor.PalfxStart(0.5f, 0.1f, 0.5f, Color.gray, palfxTarget);
            timer = 0; // �^�C�}�[���Z�b�g
        }
	}

