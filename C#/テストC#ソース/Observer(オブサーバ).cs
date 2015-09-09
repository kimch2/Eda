
// ===========================================================================
// �uSubject�v���uObserver�v�ɒʒm����B
// Subject�͈�AObserver�͕����B
// 
// 
// �t���[�F
// �@�@�@�@Subject�̃t�B�[���h���ύX�����
// �@�@�@�@��
// �@�@�@�@Subject���ύX������������S�Ă�Observer�ɒʒm
// 
// �K�v�Ȃ��́F
// �@�@�@�@�@�@�@Subject�̒��ۃ��\�b�h�iabstruct�N���X��interface�j
// �@�@�@�@�@�@�@���Ǘ�Observer�̒ǉ����\�b�h�A�폜���\�b�h�A�ʒm���\�b�h��3�̃��\�b�h���`����B
// �@�@�@�@�@�@�AObserver�̒��ۃ��\�b�h�iabstruct�N���X��interface�j
// �@�@�@�@�@�@�@��Observer�N���X��Subject�N���X�̖�������}�~����ړI�BSubject����̒ʒm����M���郁�\�b�h�݂̂��`����B
// �@�@�@�@�@�@�BSubject�̋�ۃ��\�b�h�i���ʂ̃N���X���Ď��j
// �@�@�@�@�@�@�@���Ǘ�����Observer�̒ǉ��A�폜�ƁA���̃N���X���ɕύX���������ꍇ�����ʒm���郁�\�b�h�����B
// �@�@�@�@�@�@�CObserver�̋�ۃ��\�b�h�i���ʂ̃N���X���Ď��j
// �@�@�@�@�@�@�@��Subject����ʒm���󂯂郁�\�b�h���`����B�ʒm���󂯂���S�Ă�Observer��xxx���s��(�SObserver�̋��ʏ���)�A�݂����Ɏg���B
// 
// ===========================================================================

// =====================================
// �@�T�u�W�F�N�gIF
// =====================================
public interface ISubject
{
	// �Ǘ�����Observer�̓o�^���\�b�h
    void Attach(IObserver observer);
	// �Ǘ�����Observer�̍폜���\�b�h
    void Detach(IObserver observer);
	// �Ǘ�����Observer�ւ̒ʒm���\�b�h
    void Notify();
}

// =====================================
// �A�I�u�T�[�oIF
// =====================================
public interface IObserver
{
	// Subject�ʒm��M���\�b�h
    void Notify(bool num);
}


// =====================================
// �B�I�u�T�[�o��ۃN���X�iSubject����ω���ʒm�����N���X�j
// =====================================
public class ObserverClass :
    MonoBehaviour,
    IObserver				// �I�u�T�[�oIF
{
    void Start()
    {
        // �T�u�W�F�N�g�R���|���擾���A���g�����X�g�ɒǉ��i�ǉ����Ȃ���Βʒm�����Ȃ��j
        subjectComp =  canVas.GetComponent<UnitSubject>();
        subjectComp.Attach(this); // IObserver�C���^�[�t�F�C�X��w�����Ă���N���X�Ȃ�S��Attack(this)�œo�^�ł���
    }

    // ----------------------------------------
    // �ʒm���\�b�h
    // ----------------------------------------
    public void Notify(bool num)
    {
        // Subject���ύX���ꂽ�玩��(Observer)�𓧖���
        thisAlpha = new Color(255, 255, 255, -255);
        unitSpriteImage.color = thisAlpha;
    }
}

// =====================================
// �C�T�u�W�F�N�g��ۃN���X�iObserver�ɒʒm����N���X�j
// =====================================
public class SubjectClass :
    MonoBehaviour,
    ISubject				// �T�u�W�F�N�gIF
{
    private List<IObserver> obServers = new List<IObserver>();      // �Ǘ��I�u�T�[�o���X�g
    private bool _status = false;
    public bool status
    {
        get
        {
            return _status;
        }
        set
        {
            _status = value;
            Notify();		// �ʒm���\�b�h���R�[��
        }
    }
    
    // --------------------------------------------
    // �Ǘ��I�u�T�[�o�ǉ����\�b�h
    // --------------------------------------------
    public void Attach(IObserver observer)
    {
        obServers.Add(observer);
    }

    // --------------------------------------------
    // �Ǘ��I�u�T�[�o�폜���\�b�h
    // --------------------------------------------
    public void Detach(IObserver observer)
    {
        obServers.Remove(observer);
    }

    // ----------------------------------------
    // �ʒm���\�b�h
    // ----------------------------------------
    public void Notify(bool num)
    {
        // �S�ẴI�u�T�[�o��ۃN���X���̒ʒm���\�b�h�֒ʒm
        obServers.ForEach(observer => observer.Notify(this.status));
    }
}




