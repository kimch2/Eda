

	// GameObject�Ɋi�[���Ȃ��ꍇ
	Instantiate(Resources.Load("Prefab/PushEffect_GU"), transform.position, Quaternion.identity);


	// GameObject�Ɋi�[����ꍇ
	void Start()
	{
	    GameObject obj = Instantiate(Resources.Load("Prefab/PushEffect_GU"), transform.position, Quaternion.identity) as GameObject;
	}


	// ---------------------------------------------------------------
	// Instantiate�����I�u�W�F�N�g�����I�u�W�F�N�g�̎q�ɂ���ꍇ�i���V�����BSetParent���g���j
	// ---------------------------------------------------------------
	// �X�v���C�gprefab�p�t�B�[���h
    GameObject prefab;
    // prefab��\��
    prefab = Instantiate(sprite, vec, Quaternion.identity) as GameObject;
    prefab.transform.SetParent(canVas.transform, false);



	// ---------------------------------------------------------------
	// Instantiate�����I�u�W�F�N�g�����I�u�W�F�N�g�̎q�ɂ���ꍇ�i���Â��Bparent���g���j
	// ---------------------------------------------------------------
	// �e�I�u�W�F�N�g���擾
	GameObject test1 = GameObject.Find("floor");
	// �q�I�u�W�F�N�g���擾
    GameObject test2 = Instantiate(Resources.Load("Prefab/PushEffect_GU"), transform.position, Quaternion.identity) as GameObject;
	// �q�I�u�W�F�N�g��parent�t�B�[���h�ɐe�I�u�W�F�N�g��ݒ�
	test2.transform.parent = test1.transform;

