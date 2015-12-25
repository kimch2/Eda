using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// BattleStart�N���X
/// <para>�@���j�b�g�̏����z�u������ɃR�[������ABattleStart���������{����B</para>
/// </summary>
public class BattleStart : MonoBehaviour
{
    /// <summary>��ʃJ���[�̃t�F�[�h����</summary>
    public float fadeTime = 0;
    /// <summary>�o�߂����b</summary>
    private float elapsedSec = 0;
    /// <summary>�t�F�[�h���{������</summary>
    private bool isFading = false;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    private BattleStart() { }

    /// <summary>
    /// BattleStart�����N�����\�b�h
    /// </summary>
    public void StartingMeth()
    {
        // ��ʃJ���[�̃t�F�[�h���Ԃ��C���X�y�N�^����ݒ肳��ĂȂ���Ώ���������
        if (0 == fadeTime) fadeTime = 0.7f;

        // ��ʃJ���[�Q�[���I�u�W�F�N�g�ƃR���|���擾
        var imageGO = GameObject.Find("DisplayColorAtBattleStart");
        var imageCompo = imageGO.GetComponent<Image>();

        // �����J���[�Ɠ��B�J���[��ݒ�
        Color startColor = new Color(0.51f, 0.41f, 0.13f, 0);
        Color endColor = new Color(0.51f, 0.41f, 0.13f, 0.601f);

        // ��ʃJ���[���������F���\�b�h���R�[��
        StartCoroutine(ColorUp(startColor, endColor, imageCompo));

        // ��ʃJ���[���F���������\�b�h���R�[��
        StartCoroutine(ColorDown(startColor, endColor, imageCompo));

        // ��ʃJ���[�Q�[���I�u�W�F�N�g��j������
        var displayColorCanv = GameObject.Find("Canvas_DisplayColor");
        if (!isFading) Destroy(displayColorCanv);
    }

    /// <summary>
    /// ��ʃJ���[���������F�ύX���\�b�h
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="imageCompo"></param>
    /// <returns></returns>
    private IEnumerator ColorUp(Color a, Color b, Image imageCompo)
    {
        // �t�F�[�h�����J�n��錾
        isFading = true;

        // �o�ߎ��Ԃ�������
        elapsedSec = 0;
        while (true)
        {
            if (b == imageCompo.color)
            {
                // Lerp�������I�������烋�[�v�𔲂���
                break;
            }
            // �A���t�@�l��Lerp
            elapsedSec += Time.deltaTime;
            imageCompo.color = Color.Lerp(a, b, elapsedSec * fadeTime);
            yield return null;
        }
    }

    /// <summary>
    /// ��ʃJ���[���F�������ύX���\�b�h
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="imageCompo"></param>
    /// <returns></returns>
    private IEnumerator ColorDown(Color a, Color b, Image imageCompo)
    {
        // BattleStart�摜�\�����͏������~����
        yield return new WaitForSeconds(6.0f);

        // �o�ߎ��Ԃ�������
        elapsedSec = 0;
        while (true)
        {
            if (a == imageCompo.color)
            {
                // Lerp�������I�������烋�[�v�𔲂���
                yield return new WaitForSeconds(0.6f);
                // �t�F�[�h�I����錾
                isFading = false;
                break;
            }
            // �A���t�@�l��Lerp
            elapsedSec += Time.deltaTime;
            imageCompo.color = Color.Lerp(b, a, elapsedSec * fadeTime);
            yield return null;
        }
    }
}
