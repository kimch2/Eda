using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// �f�B�X�v���C�J���[�ύX�N���X
/// <para>�@BattleStart���ɉ�ʂ����F�ɂ���</para>
/// </summary>
public class DisplayColorGO : MonoBehaviour 
{
    /// <summary>��ʃJ���[�̃t�F�[�h����</summary>
    public float fadeTime = 0;
    /// <summary>�o�߂����b</summary>
    private float elapsedSec = 0;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    private DisplayColorGO() { }

    public void ColorChange()
    {
        // ��ʃJ���[�̃t�F�[�h���Ԃ�������
        fadeTime = 0.6f;

        // �����J���[�Ɠ��B�J���[��ݒ�(255�Ŋ���)
        var imageCompo = this.gameObject.GetComponent<Image>();
        Color startColor = new Color(0.51f, 0.41f, 0.13f, 0);
        Color endColor = new Color(0.51f, 0.41f, 0.13f, 0.601f);

        StartCoroutine(ColorCor(startColor, endColor, imageCompo));
    }

    private IEnumerator ColorCor(Color a, Color b, Image imageCompo)
    {
        while (true)
        {
            elapsedSec += Time.deltaTime;
            if (20.0f < elapsedSec)
            {
                // Lerp�������I�������烋�[�v�𔲂���
                break;
            }
            // �A���t�@�l��Lerp
            imageCompo.color = Color.Lerp(a, b, elapsedSec * fadeTime);
            yield return null;
        }
    }

    /// <summary>
    /// DisplayColorGO�������\�b�h
    /// <para>�@�{�I�u�W�F�N�g����������B</para>
    /// </summary>
    public void Destroy()
    {
        Destroy(this);
    }
}
