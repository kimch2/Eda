using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ComboBoxClass : MonoBehaviour 
{
    /// <summary>�}�l�[�W���[�R���|</summary>
    private GameManager gameManager;
    /// <summary>���j�b�gID��\�����Ă���Text�R���|�i�C���X�y�N�^����̂ݐݒ肷��j</summary>
    [SerializeField]
    private Text text_UnitID;
    /// <summary>�R���{�{�b�N�X���J�������̃R���{�{�b�N�X�̉摜</summary>
    public Sprite Sprite_UISprite;
    /// <summary>�R���{�{�b�N�X���J�������̃R���{�{�b�N�X�̔w�i�摜</summary>
    public Sprite Sprite_Background;
    /// <summary>�R���{�{�b�N�X�������̃{�^����I����������delegate</summary>
    public Action<int> OnSelectionChanged;

    [SerializeField]
	private bool _isComboBoxEnable = true;
    /// <summary>�v���_�E���{�^�������������Ƀv���_�E�����j���[��\�����邩�ۂ�</summary>
    public bool IsComboBoxEnable
	{
		get
		{
			return _isComboBoxEnable;
		}
		set
		{
            // �t�B�[���h�ɒl��ݒ肷��
			_isComboBoxEnable = value;

            // �q��Button�R���|���擾���A�L���t���O�ɓ��͂��ꂽ�l��ݒ肷��
			var button = comboButtonRectTransform.GetComponent<Button>();
			button.interactable = _isComboBoxEnable;

            // �q��Image�R���|���擾���A�J���[����уX�v���C�g��ݒ�
			var image = comboImageRectTransform.GetComponent<Image>();
			image.color = image.sprite == null ? new Color(1.0f, 1.0f, 1.0f, 0.0f) : _isComboBoxEnable ? button.colors.normalColor : button.colors.disabledColor;

            // �v���C���[���Đ��\�łȂ��ꍇ�̓Z�b�^�[�𔲂���
            if (!Application.isPlaying) return;
				
            // ���͂��ꂽ�l��false���I�[�o�[���CComboBox���A�N�e�B�u��Ԃ̏ꍇ�̓R���{�{�b�N�X��
			if (!_isComboBoxEnable && overlayGO.activeSelf)
				ToggleComboBox(false);
		}
	}

    [SerializeField]
	private int _itemsToDisplay = 4;
    /// <summary>�v���_�E���{�^�������������Ƀv���_�E�����j���[�Ɉ�x�ɕ\�������A�C�e����</summary>
    public int ItemsToDisplay
	{
		get
		{
			return _itemsToDisplay;
		}
		set
		{
			if (_itemsToDisplay == value)
				return;
			_itemsToDisplay = value;
			Refresh();
		}
	}

	[SerializeField]
	private bool _hideFirstItem;
    /// <summary>�v���_�E�����j���[�ň�ԏ�̃A�C�e����\�����邩�ۂ�</summary>
    public bool HideFirstItem
	{
		get
		{
			return _hideFirstItem;
		}
		set
		{
			if (value)
                // true�̏ꍇ�̓I�t�Z�b�g�ʒu���f�N�������g���Ĉ�ԏ�̃A�C�e�����\���ɂ���
				scrollOffset--;
			else
                // false�̏ꍇ�̓I�t�Z�b�g�ʒu���C���N�������g���Ĉ�ԏ�̃A�C�e����\������
                scrollOffset++;
			_hideFirstItem = value;
			Refresh();
		}
	}

	[SerializeField]
    private int _selectedClass = 0;
    /// <summary>�v���_�E�����j���[�őI�����ꂽ�N���X��ID</summary>
    public int SelectedClass
	{
		get 
		{
			return _selectedClass;
		}
		set
		{
			if (_selectedClass == value)
                // ��ID�ƑI�����ꂽID�������Ȃ珈������K�v�Ȃ�
				return;

			if (value > -1 && value < ClassList.Length)
			{
                // �I�����ꂽID������͈͓��ł���΃t�B�[���h�ɐݒ肷��
				_selectedClass = value;

                // �N���XID�ݒ胁�\�b�h���R�[�����A�I�����ꂽ�N���XID��ݒ肷��
                SetClassIDtoManager(value);

				RefreshSelected();
			}
		}
	}

	[SerializeField]
	private ComboBoxItem[] _classList;
    /// <summary>�v���_�E�����j���[���J�������ɕ\�������A�C�e���̑���</summary>
    public ComboBoxItem[] ClassList
	{
		get
		{
			if (_classList == null)
                // ���݂��Ȃ���΋�̔z��𐶐�����
				_classList = new ComboBoxItem[0];
			return _classList;
		}
		set
		{
			_classList = value;
			Refresh();
		}
	}

    /// <summary>�{�^�����}�X�N���邽�߂̃I�[�o�[���CComboBox�I�u�W�F�N�g</summary>
    private GameObject overlayGO;
	private int scrollOffset;
    /// <summary>ComboBox�̃X�N���[���o�[�����̃T�C�Y</summary>
    [SerializeField]
    private float _scrollbarWidth = 10.0f;

	private RectTransform _rectTransform;
    /// <summary>�v���_�E�����j���[�ň�ԏ�̃A�C�e����\�����邩�ۂ�</summary>
    private RectTransform rectTransform
	{
		get
		{
			if (_rectTransform == null)
                // �����Ă��Ȃ���Ύ擾����
                _rectTransform = GetComponent<RectTransform>();
			return _rectTransform;
		}
		set
		{
			_rectTransform = value;
		}
	}

	private RectTransform _buttonRectTransform;
    /// <summary>�v���_�E���{�^���i�ŏ�����\������Ă�{�^���j��RectTransform�R���|</summary>
    private RectTransform buttonRectTransform
	{
		get
		{
			if (_buttonRectTransform == null)
                // �����Ă��Ȃ���Ύ擾����
				_buttonRectTransform = rectTransform.Find("Button").GetComponent<RectTransform>();
			return _buttonRectTransform;
		}
		set
		{
			_buttonRectTransform = value;
		}
	}

	private RectTransform _comboButtonRectTransform;
    /// <summary>�R���{�{�^���i�v���_�E�����j���[���̃{�^���j��RectTransform�R���|</summary>
    private RectTransform comboButtonRectTransform
	{
		get
		{
			if (_comboButtonRectTransform == null)
                // �����Ă��Ȃ���Ύ擾����
                _comboButtonRectTransform = buttonRectTransform.Find("ComboButton").GetComponent<RectTransform>();
			return _comboButtonRectTransform;
		}
		set
		{
			_comboButtonRectTransform = value;
		}
	}

	private RectTransform _comboImageRectTransform;
    /// <summary>�R���{�{�^���i�v���_�E�����j���[���̃{�^���j��Image�I�u�W�F�N�g��RectTransform�R���|</summary>
    private RectTransform comboImageRectTransform
	{
		get
		{
			if (_comboImageRectTransform == null)
                // �����Ă��Ȃ���Ύ擾����
                _comboImageRectTransform = comboButtonRectTransform.Find("Image").GetComponent<RectTransform>();
			return _comboImageRectTransform;
		}
		set
		{
			_comboImageRectTransform = value;
		}
	}

	private RectTransform _comboTextRectTransform;
	private RectTransform comboTextRectTransform
	{
		get
		{
			if (_comboTextRectTransform == null)
				_comboTextRectTransform = comboButtonRectTransform.Find("Text").GetComponent<RectTransform>();
			return _comboTextRectTransform;
		}
		set
		{
			_comboTextRectTransform = value;
		}
	}

	private RectTransform _comboArrowRectTransform;
	private RectTransform comboArrowRectTransform
	{
		get
		{
			if (_comboArrowRectTransform == null)
				_comboArrowRectTransform = buttonRectTransform.Find("Arrow").GetComponent<RectTransform>();
			return _comboArrowRectTransform;
		}
		set
		{
			_comboArrowRectTransform = value;
		}
	}

	private RectTransform _scrollPanelRectTransfrom;
	private RectTransform scrollPanelRectTransfrom
	{
		get
		{
			if (_scrollPanelRectTransfrom == null)
				_scrollPanelRectTransfrom = rectTransform.Find("Overlay/ScrollPanel").GetComponent<RectTransform>();
			return _scrollPanelRectTransfrom;
		}
		set
		{
			_scrollPanelRectTransfrom = value;
		}
	}

	private RectTransform _itemsRectTransfrom;
	private RectTransform itemsRectTransfrom
	{
		get
		{
			if (_itemsRectTransfrom == null)
				_itemsRectTransfrom = scrollPanelRectTransfrom.Find("Items").GetComponent<RectTransform>();
			return _itemsRectTransfrom;
		}
		set
		{
			_itemsRectTransfrom = value;
		}
	}

	private RectTransform _scrollbarRectTransfrom;
	private RectTransform scrollbarRectTransfrom
	{
		get
		{
			if (_scrollbarRectTransfrom == null)
				_scrollbarRectTransfrom = scrollPanelRectTransfrom.Find("Scrollbar").GetComponent<RectTransform>();
			return _scrollbarRectTransfrom;
		}
		set
		{
			_scrollbarRectTransfrom = value;
		}
	}

	private RectTransform _slidingAreaRectTransform;
	private RectTransform slidingAreaRectTransform
	{
		get
		{
			if (_slidingAreaRectTransform == null)
				_slidingAreaRectTransform = scrollbarRectTransfrom.Find("SlidingArea").GetComponent<RectTransform>();
			return _slidingAreaRectTransform;
		}
		set
		{
			_slidingAreaRectTransform = value;
		}
	}

	private RectTransform _handleRectTransfrom;
	private RectTransform handleRectTransfrom
	{
		get
		{
			if (_handleRectTransfrom == null)
				_handleRectTransfrom = slidingAreaRectTransform.Find("Handle").GetComponent<RectTransform>();
			return _handleRectTransfrom;
		}
		set
		{
			_handleRectTransfrom = value;
		}
	}

	private void Awake()
	{
        // �}�l�[�W���R���|�擾
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // �I�u�W�F�N�g��������я��������\�b�h���R�[��
        InitControl();
    }

    /// <summary>
    /// �N���XID�ݒ胁�\�b�h
    /// <para>�@���̃v���_�E�����j���[���S�����郆�j�b�g��ID��</para>
    /// <para>�@�e�I�u�W�F�N�g��Text�R���|�̕�������擾���A���������</para>
    /// <para>�@���̃��j�b�gID�̃N���X�t�B�[���h�֑I�����ꂽ�N���XID��ݒ肷��B</para>
    /// </summary>
    private void SetClassIDtoManager(int setterValue)
    {
        // ���j�b�gID��Text���烆�j�b�gID�ł���Ō��1����(�܂���2����)�𔲂��o���Ē萔���e�����ɕϊ�����
        int unitID = 0;
        if (4 == text_UnitID.text.Length)
        {
            // ID��1���̏ꍇ�͖���1�����𒊏o
            unitID = int.Parse(text_UnitID.text.Substring(text_UnitID.text.Length - 1, 1));
        }
        else
        {
            // ID��2���̏ꍇ�͖���2�����𒊏o
            unitID = int.Parse(text_UnitID.text.Substring(text_UnitID.text.Length - 2, 2));
        }

        // �ݒ肳�ꂽ�N���XID���}�l�[�W���[�R���|�ɐݒ�
        // TODO �N���XID��G�������gID��0����n�߂�H+1�␳���鏈�����T������
        gameManager.unitStateList[unitID - 1].classType = setterValue + 1;

        // �\�����Ă��郆�j�b�g�摜���X�V
//        var unitSpriteSet = new NameSelect();
//        unitSpriteSet.UnitSpriteSet(gameManager);
    }

	public void OnItemClicked(int index)
	{
		var selectionChanged = index != SelectedClass;
		SelectedClass = index;
		ToggleComboBox(true);
		if (selectionChanged && OnSelectionChanged != null)
			OnSelectionChanged(index);
	}

    /// <summary>
    /// �v���_�E�����j���[�A�C�e���ǉ����\�b�h
    /// <para>�@�X�N���v�g����v���_�E�����j���[�A�C�e����ǉ�����ꍇ��</para>
    /// <para>�@�R�[�����A�A�C�e���𐶐�����B</para>
    /// </summary>
    public void AddItems(params object[] list)
	{
		var cbItems = new List<ComboBoxItem>();

		foreach (var obj in list)
		{
			if (obj is ComboBoxItem)
			{
				var item = (ComboBoxItem)obj;
				cbItems.Add(item);
				continue;
			}
			if (obj is string)
			{
				var item = new ComboBoxItem((string)obj, null, false, null);
				cbItems.Add(item);
				continue;
			}
			if (obj is Sprite)
			{
				var item = new ComboBoxItem(null, (Sprite)obj, false, null);
				cbItems.Add(item);
				continue;
			}
			throw new Exception("Only ComboBoxItem, string and Sprite types are allowed");
		}
		var newItems = new ComboBoxItem[ClassList.Length + cbItems.Count];
		ClassList.CopyTo(newItems, 0);
		cbItems.ToArray().CopyTo(newItems, ClassList.Length);
		Refresh();
		ClassList = newItems;
	}

    /// <summary>
    /// �S�v���_�E�����j���[�N���A���\�b�h
    /// </summary>
    public void ClearItems()
	{
		ClassList = new ComboBoxItem[0];
	}

    /// <summary>
    /// �v���_�E�����j���[���I�u�W�F�N�g�������\�b�h
    /// </summary>
    public void CreateControl()
	{
		rectTransform = GetComponent<RectTransform>();

        // �v���_�E�����j���[����Button�I�u�W�F�N�g����
		var buttonGO = new GameObject("Button");
		buttonGO.transform.SetParent(transform, false);
		buttonRectTransform = buttonGO.AddComponent<RectTransform>();
		buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.x);
		buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransform.sizeDelta.y);
		buttonRectTransform.anchoredPosition = Vector2.zero;

        // ComboButton�I�u�W�F�N�g����
        var comboButtonGO = new GameObject("ComboButton");
		comboButtonGO.transform.SetParent(buttonRectTransform, false);
		comboButtonRectTransform = comboButtonGO.AddComponent<RectTransform>();
		comboButtonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonRectTransform.sizeDelta.x);
		comboButtonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonRectTransform.sizeDelta.y);
		comboButtonRectTransform.anchoredPosition = Vector2.zero;

        // ComboButton�I�u�W�F�N�g��Image�R���|��ǉ����摜����щ摜�^�C�v�iSliced�j��ݒ�
        var comboButtonImage = comboButtonGO.AddComponent<Image>();
		comboButtonImage.sprite = Sprite_UISprite;
		comboButtonImage.type = Image.Type.Sliced;


        // ComboButton�I�u�W�F�N�g�̉摜��ݒ�
        var comboButtonButton = comboButtonGO.AddComponent<Button>();
		comboButtonButton.targetGraphic = comboButtonImage;

        // ComboButton�I�u�W�F�N�g�̃J���[�ݒ�
		var comboButtonColors = new ColorBlock();
		comboButtonColors.normalColor = new Color32(255, 255, 255, 255);
		comboButtonColors.highlightedColor = new Color32(245, 245, 245, 255);
		comboButtonColors.pressedColor = new Color32(200, 200, 200, 255);
		comboButtonColors.disabledColor = new Color32(200, 200, 200, 128);
		comboButtonColors.colorMultiplier = 1.0f;
		comboButtonColors.fadeDuration = 0.1f;
		comboButtonButton.colors = comboButtonColors;

        // ���I�u�W�F�N�g����
        var comboArrowGO = new GameObject("Arrow");
		comboArrowGO.transform.SetParent(buttonRectTransform, false);
		var comboArrowText = comboArrowGO.AddComponent<Text>();
		comboArrowText.color = new Color32(0, 0, 0, 255);
        comboArrowText.alignment = TextAnchor.MiddleRight;
		comboArrowText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		comboArrowText.text = "��";
		comboArrowRectTransform.localScale = new Vector3(1.0f, 0.5f, 1.0f);
		comboArrowRectTransform.pivot = new Vector2(1.0f, 0.5f);
		comboArrowRectTransform.anchorMin = Vector2.right;
		comboArrowRectTransform.anchorMax = Vector2.one;
		comboArrowRectTransform.anchoredPosition = Vector2.zero;
		comboArrowRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, comboButtonRectTransform.sizeDelta.y);
		comboArrowRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, comboButtonRectTransform.sizeDelta.y);
		var comboArrowCanvasGroup = comboArrowGO.AddComponent<CanvasGroup>();
		comboArrowCanvasGroup.interactable = false;
		comboArrowCanvasGroup.blocksRaycasts = false;

        // Image�I�u�W�F�N�g����
        var comboImageGO = new GameObject("Image");
		comboImageGO.transform.SetParent(comboButtonRectTransform, false);
		var comboImageImage = comboImageGO.AddComponent<Image>();
		comboImageImage.color = new Color32(255, 255, 255, 0);
		comboImageRectTransform.pivot = Vector2.up;
		comboImageRectTransform.anchorMin = Vector2.zero;
		comboImageRectTransform.anchorMax = Vector2.up;
		comboImageRectTransform.anchoredPosition = new Vector2(4.0f, -4.0f);
		comboImageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, comboButtonRectTransform.sizeDelta.y - 8.0f);
		comboImageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, comboButtonRectTransform.sizeDelta.y - 8.0f);

        // Text�I�u�W�F�N�g����
        var comboTextGO = new GameObject("Text");
		comboTextGO.transform.SetParent(comboButtonRectTransform, false);
		var comboTextText = comboTextGO.AddComponent<Text>();
		comboTextText.color = new Color32(0, 0, 0, 255);
		comboTextText.alignment = TextAnchor.MiddleLeft;
		comboTextText.lineSpacing = 1.2f;
		comboTextText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		comboTextRectTransform.pivot = Vector2.up;
		comboTextRectTransform.anchorMin = Vector2.zero;
		comboTextRectTransform.anchorMax = Vector2.one;
		comboTextRectTransform.anchoredPosition = new Vector2(10.0f, 0.0f);
		comboTextRectTransform.offsetMax = new Vector2(4.0f, 0.0f);
		comboTextRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, comboButtonRectTransform.sizeDelta.y);
	}

    /// <summary>
    /// �I�u�W�F�N�g��������я��������\�b�h
    /// </summary>
    public void InitControl()
	{
		var cbi = transform.Find("Button/ComboButton/Image");
		var cbt = transform.Find("Button/ComboButton/Text");
		var cba = transform.Find("Button/Arrow");
		if (cbi == null || cbt == null || cba == null)
		{
			foreach (Transform child in transform)
				Destroy(child);
			CreateControl();
		}

		comboButtonRectTransform.GetComponent<Button>().onClick.AddListener(() => { ToggleComboBox(false); });
		var dropdownHeight = comboButtonRectTransform.sizeDelta.y *  Mathf.Min(ItemsToDisplay, ClassList.Length - (HideFirstItem ? 1 : 0));

        // �{�^�����}�X�N���邽�߂̃I�[�o�[���CComboBox�I�u�W�F�N�g�𐶐�
        overlayGO = new GameObject("Overlay");
		overlayGO.SetActive(false);

        // �I�[�o�[���CComboBox��Image�R���|��ǉ�
        var overlayImage = overlayGO.AddComponent<Image>();

        // �J���[��ݒ�
		overlayImage.color = new Color32(0, 0, 0, 0);

        // Canvas�R���|���Q�g�R���ł���܂Őe�I�u�W�F�N�g�����[�v
		var canvasTransform = transform;
        while (canvasTransform.GetComponent<Canvas>() == null)
        {
            canvasTransform = canvasTransform.parent;
        }
        // �I�[�o�[���CComboBox�I�u�W�F�N�g��Canvas�R���|�����I�u�W�F�N�g�̎q�ɂ���
		overlayGO.transform.SetParent(canvasTransform, false);

        // �I�[�o�[���CComboBox��RectTransform�R���|��ǉ�
        var overlayRectTransform = overlayGO.GetComponent<RectTransform>();

        // �I�[�o�[���CComboBox��Rect�n�̐ݒ�
		overlayRectTransform.anchorMin = Vector2.zero;
		overlayRectTransform.anchorMax = Vector2.one;
		overlayRectTransform.offsetMin = Vector2.zero;
		overlayRectTransform.offsetMax = Vector2.zero;
		overlayGO.transform.SetParent(transform, false);

        // �I�[�o�[���CComboBox��Button�R���|��ǉ����ăA���t�@�l0�̓���Image��ݒ�
        var overlayButton = overlayGO.AddComponent<Button>();
		overlayButton.targetGraphic = overlayImage;

        // �Ȃ񂩂�ǉ����Ă���ۂ�
		overlayButton.onClick.AddListener(() => { ToggleComboBox(false); });

        // �I�[�o�[���CComboBox�I�u�W�F�N�g�Ɏ�������X�N���[���o�[�𐶐�
        var scrollPanelGO = new GameObject("ScrollPanel");

        // �X�N���[���o�[��Image�R���|��ǉ����摜����щ摜�^�C�v�iSliced�j��ݒ�
        var scrollPanelImage = scrollPanelGO.AddComponent<Image>();
		scrollPanelImage.sprite = Sprite_UISprite;
		scrollPanelImage.type = Image.Type.Sliced;

        // �X�N���[���o�[���I�[�o�[���CComboBox�I�u�W�F�N�g�̎q�ɐݒ�
		scrollPanelGO.transform.SetParent(overlayGO.transform, false);

        // �X�N���[���o�[��Rect�n�̐ݒ�
        scrollPanelRectTransfrom.pivot = new Vector2(0.5f, 1.0f);
		scrollPanelRectTransfrom.anchorMin = Vector2.zero;
		scrollPanelRectTransfrom.anchorMax = Vector2.one;

		scrollPanelGO.transform.SetParent(transform, false);
		scrollPanelRectTransfrom.anchoredPosition = new Vector2(0.0f, -comboButtonRectTransform.sizeDelta.y);
		scrollPanelGO.transform.SetParent(overlayGO.transform, false);

		scrollPanelRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, comboButtonRectTransform.sizeDelta.x);
		scrollPanelRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight);

		var scrollPanelScrollRect = scrollPanelGO.AddComponent<ScrollRect>();
		scrollPanelScrollRect.horizontal = false;
		scrollPanelScrollRect.elasticity = 0.0f;
		scrollPanelScrollRect.movementType = ScrollRect.MovementType.Clamped;
		scrollPanelScrollRect.inertia = false;
		scrollPanelScrollRect.scrollSensitivity = comboButtonRectTransform.sizeDelta.y;
		scrollPanelGO.AddComponent<Mask>();

        // �X�N���[���o�[�̉���������H
        var scrollbarWidth = ClassList.Length - (HideFirstItem ? 1 : 0) > _itemsToDisplay ? _scrollbarWidth : 0.0f;

        // Items�I�u�W�F�N�g�i�S�ẴR���{�{�^���̐e�I�u�W�F�N�g�j�𐶐�
		var itemsGO = new GameObject("Items");
		itemsGO.transform.SetParent(scrollPanelGO.transform, false);
		itemsRectTransfrom = itemsGO.AddComponent<RectTransform>();
		itemsRectTransfrom.pivot = Vector2.up;
		itemsRectTransfrom.anchorMin = Vector2.up;
		itemsRectTransfrom.anchorMax = Vector2.one;
		itemsRectTransfrom.anchoredPosition = Vector2.right;
		itemsRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollPanelRectTransfrom.sizeDelta.x - scrollbarWidth);
		var itemsContentSizeFitter = itemsGO.AddComponent<ContentSizeFitter>();
		itemsContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
		itemsContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
		var itemsGridLayoutGroup = itemsGO.AddComponent<GridLayoutGroup>();
		itemsGridLayoutGroup.cellSize = new Vector2(comboButtonRectTransform.sizeDelta.x - scrollbarWidth, comboButtonRectTransform.sizeDelta.y);
		itemsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
		itemsGridLayoutGroup.constraintCount = 1;
		scrollPanelScrollRect.content = itemsRectTransfrom;

        // Scrollbar�I�u�W�F�N�g�𐶐�
		var scrollbarGO = new GameObject("Scrollbar");
		scrollbarGO.transform.SetParent(scrollPanelGO.transform, false);
		var scrollbarImage = scrollbarGO.AddComponent<Image>();
		scrollbarImage.sprite = Sprite_Background;
		scrollbarImage.type = Image.Type.Sliced;
		var scrollbarScrollbar = scrollbarGO.AddComponent<Scrollbar>();
		var scrollbarColors = new ColorBlock();
		scrollbarColors.normalColor = new Color32(128, 128, 128, 128);
		scrollbarColors.highlightedColor = new Color32(128, 128, 128, 178);
		scrollbarColors.pressedColor = new Color32(88, 88, 88, 178);
		scrollbarColors.disabledColor = new Color32(64, 64, 64, 128);
		scrollbarColors.colorMultiplier = 2.0f;
		scrollbarColors.fadeDuration = 0.1f;
		scrollbarScrollbar.colors = scrollbarColors;
		scrollPanelScrollRect.verticalScrollbar = scrollbarScrollbar;
		scrollbarScrollbar.direction = Scrollbar.Direction.BottomToTop;
		scrollbarRectTransfrom.pivot = Vector2.one;
		scrollbarRectTransfrom.anchorMin = Vector2.one;
		scrollbarRectTransfrom.anchorMax = Vector2.one;
		scrollbarRectTransfrom.anchoredPosition = Vector2.zero;
		scrollbarRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollbarWidth);
		scrollbarRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight);

		var slidingAreaGO = new GameObject("SlidingArea");
		slidingAreaGO.transform.SetParent(scrollbarGO.transform, false);
		slidingAreaRectTransform = slidingAreaGO.AddComponent<RectTransform>();
		slidingAreaRectTransform.anchoredPosition = Vector2.zero;
		slidingAreaRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
		slidingAreaRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight - scrollbarRectTransfrom.sizeDelta.x);

		var handleGO = new GameObject("Handle");
		handleGO.transform.SetParent(slidingAreaGO.transform, false);
		var handleImage = handleGO.AddComponent<Image>();
		handleImage.sprite = Sprite_UISprite;
		handleImage.type = Image.Type.Sliced;
		handleImage.color = new Color32(255, 255, 255, 150);
		scrollbarScrollbar.targetGraphic = handleImage;
		scrollbarScrollbar.handleRect = handleRectTransfrom;
		handleRectTransfrom.pivot = new Vector2(0.5f, 0.5f);
		handleRectTransfrom.anchorMin = new Vector2(0.5f, 0.5f);
		handleRectTransfrom.anchorMax = new Vector2(0.5f, 0.5f);
		handleRectTransfrom.anchoredPosition = Vector2.zero;
		handleRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollbarWidth);
		handleRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, scrollbarWidth);

		IsComboBoxEnable = IsComboBoxEnable;

		if (ClassList.Length < 1)
			return;
		Refresh();
	}

	public void Refresh()
	{
		var itemsGridLayoutGroup = itemsRectTransfrom.GetComponent<GridLayoutGroup>();
		var itemsLength = ClassList.Length - (HideFirstItem ? 1 : 0);
		var dropdownHeight = comboButtonRectTransform.sizeDelta.y *  Mathf.Min(_itemsToDisplay, itemsLength);
		var scrollbarWidth = itemsLength > ItemsToDisplay ? _scrollbarWidth : 0.0f;
		scrollPanelRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight);
		scrollbarRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollbarWidth);
		scrollbarRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight);
		slidingAreaRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dropdownHeight - scrollbarRectTransfrom.sizeDelta.x);
		itemsRectTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, scrollPanelRectTransfrom.sizeDelta.x - scrollbarWidth);
		itemsGridLayoutGroup.cellSize = new Vector2(comboButtonRectTransform.sizeDelta.x - scrollbarWidth, comboButtonRectTransform.sizeDelta.y);
		for (var i = itemsRectTransfrom.childCount - 1; i > -1; i--)
			DestroyImmediate(itemsRectTransfrom.GetChild(0).gameObject);

        // �w�肳�ꂽ�����̃v���_�E�����j���[���{�^���ɂ��ĕ\�����ƕ\���摜��ݒ�
		for (var i = 0; i < ClassList.Length; i++)
		{
			if (HideFirstItem && i == 0)
				continue;

			var item = ClassList[i];    // �t�B�[���h�ڂ��ւ�
			item.OnUpdate = Refresh;
			var itemTransform = Instantiate(comboButtonRectTransform) as Transform;
			itemTransform.SetParent(itemsRectTransfrom, false);
			itemTransform.GetComponent<Image>().sprite = null;

            // �v���_�E�����j���[���̃{�^���\����Text��ݒ�
			var itemText = itemTransform.Find("Text").GetComponent<Text>();
			itemText.text = item.ClassName;

            // �v���_�E�����j���[���̃{�^���\����Text�̃J���[��ݒ�
            if (item.IsDisabled)
				itemText.color = new Color32(174, 174, 174, 255);

            // �v���_�E�����j���[���̃{�^���\���摜��ݒ�
			var itemImage = itemTransform.Find("Image").GetComponent<Image>();
			itemImage.sprite = item.Image;

            // �v���_�E�����j���[���̃{�^���\���摜�̃J���[��ݒ�
            itemImage.color = item.Image == null ? new Color32(255, 255, 255, 0) : item.IsDisabled ? new Color32(255, 255, 255, 147) : new Color32(255, 255, 255, 255);

            // �v���_�E�����j���[����Button�R���|���擾
			var itemButton = itemTransform.GetComponent<Button>();
			itemButton.interactable = !item.IsDisabled;

			var index = i;
			itemButton.onClick.AddListener(
				delegate()
				{
					OnItemClicked(index);
                    // �C���X�y�N�^�ɕ\������v���_�E�����j���[���{�^���̐ݒ荀�ڂ𐶐��H
					if (item.OnSelect != null)
						item.OnSelect();
				}
			);
		}
		RefreshSelected();
		UpdateComboBoxImages();
		UpdateGraphics();
		FixScrollOffset();
	}

	public void RefreshSelected()
	{
		var comboButtonImage = comboImageRectTransform.GetComponent<Image>();
		var item = SelectedClass > -1 && SelectedClass < ClassList.Length ? ClassList[SelectedClass] : null;
		var includeImage = item != null && item.Image != null;
		comboButtonImage.sprite = includeImage ? item.Image : null;
		var comboButtonButton = comboButtonRectTransform.GetComponent<Button>();
		comboButtonImage.color = includeImage ? (IsComboBoxEnable ? comboButtonButton.colors.normalColor : comboButtonButton.colors.disabledColor) : new Color(1.0f, 1.0f, 1.0f, 0);
		UpdateComboBoxImage(comboButtonRectTransform, includeImage);
		comboTextRectTransform.GetComponent<Text>().text = item != null ? item.ClassName : "";

		if (!Application.isPlaying)
            // Unity�v���C���[�ōĐ��s�\�ȏꍇ�͏I��
            return;

        // �S�Ă̎q�I�u�W�F�N�g��Image�R���|���擾���A�J���[��ݒ�
		var i = 0;
		foreach (Transform child in itemsRectTransfrom)
		{
			comboButtonImage = child.GetComponent<Image>();
			comboButtonImage.color = SelectedClass == i + (HideFirstItem ? 1 : 0) ? comboButtonButton.colors.highlightedColor : comboButtonButton.colors.normalColor;
			i++;
		}
	}

	private void UpdateComboBoxImages()
	{
		var includeImages = false;
		foreach (var item in ClassList)
		{
			if (item.Image != null)
			{
				includeImages = true;
				break;
			}
		}
		foreach (Transform child in itemsRectTransfrom)
			UpdateComboBoxImage(child, includeImages);
	}

	private void UpdateComboBoxImage(Transform comboButton, bool includeImage)
	{
		comboButton.Find("Text").GetComponent<RectTransform>().offsetMin = Vector2.right * (includeImage ? comboImageRectTransform.rect.width + 8.0f : 10.0f);
	}

	private void FixScrollOffset()
	{
		var selectedIndex = SelectedClass + (HideFirstItem ? 1 : 0);
		if (selectedIndex < scrollOffset)
			scrollOffset = selectedIndex;
		else
			if (selectedIndex > scrollOffset + ItemsToDisplay - 1)
				scrollOffset = selectedIndex - ItemsToDisplay + 1;
		var itemsCount = ClassList.Length - (HideFirstItem ? 1 : 0);
		if (scrollOffset > itemsCount - ItemsToDisplay)
			scrollOffset = itemsCount - ItemsToDisplay;
		if (scrollOffset < 0)
			scrollOffset = 0;
		itemsRectTransfrom.anchoredPosition = new Vector2(0.0f, scrollOffset * rectTransform.sizeDelta.y);
	}
	
	private void ToggleComboBox(bool directClick)
	{
		overlayGO.SetActive(!overlayGO.activeSelf);

        // �R���{�{�b�N�X���A�N�e�B�u�̏ꍇ
        if (overlayGO.activeSelf)
        {
            var curTransform = transform;
            do
            {
                curTransform.SetAsLastSibling();
            }
            while ((curTransform = curTransform.parent) != null);
            FixScrollOffset();
        }
        // �R���{�{�b�N�X����A�N�e�B�u�̏ꍇ
        else
        {
            if (directClick)
            {
                scrollOffset = (int)Mathf.Round(itemsRectTransfrom.anchoredPosition.y / rectTransform.sizeDelta.y);
            }
        }
    }

    /// <summary>
    ///  �v���_�E�����j���[�č\�z���\�b�h�H
    ///  <para>�@�v���_�E�����j���[�̃T�C�Y����I�u�W�F�N�g�̔z�u�����߂Ă���ۂ�</para>
    /// </summary>
	public void UpdateGraphics()
	{
		if (overlayGO != null)
		{
			var scrollbarWidth = ClassList.Length - (HideFirstItem ? 1 : 0) > ItemsToDisplay ? _scrollbarWidth : 0.0f;
			handleRectTransfrom.offsetMin = -scrollbarWidth / 2 * Vector2.one;
			handleRectTransfrom.offsetMax = scrollbarWidth / 2 * Vector2.one;
		}
		if (rectTransform.sizeDelta != buttonRectTransform.sizeDelta && buttonRectTransform.sizeDelta == comboButtonRectTransform.sizeDelta)
		{
			buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.x);
			buttonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransform.sizeDelta.y);
			comboButtonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.x);
			comboButtonRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransform.sizeDelta.y);
			comboArrowRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.y);
			comboImageRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, comboImageRectTransform.rect.height);
			comboTextRectTransform.offsetMax = new Vector2(4.0f, 0.0f);
			if (overlayGO == null)
				return;
			scrollPanelRectTransfrom.SetParent(transform, false);
			scrollPanelRectTransfrom.anchoredPosition = new Vector2(0.0f, -comboButtonRectTransform.sizeDelta.y);
			scrollPanelRectTransfrom.SetParent(overlayGO.transform, false);
			scrollPanelRectTransfrom.GetComponent<ScrollRect>().scrollSensitivity = comboButtonRectTransform.sizeDelta.y;
			UpdateComboBoxImage(comboButtonRectTransform, ClassList[SelectedClass].Image != null);
			Refresh();
		}
	}
}