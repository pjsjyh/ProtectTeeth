using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SliderUI : MonoBehaviour
{
    public RectTransform panel1; // ù ��° ȭ��
    public RectTransform panel2; // �� ��° ȭ��
    public float slideDuration = 0.5f; // �����̵� �ð�

    public void SlideToPanel12()
    {
        panel1.DOAnchorPos(new Vector2(-Screen.width * 2, 0), slideDuration).SetEase(Ease.OutExpo);
        panel2.DOAnchorPos(new Vector2(0, 0), slideDuration).SetEase(Ease.OutExpo);
    }
    // 1�� ȭ������ �ٽ� �����̵��ϴ� �Լ�
    public void SlideToPanel21()
    {
        panel1.DOAnchorPos(new Vector2(0, 0), slideDuration).SetEase(Ease.OutExpo);
        panel2.DOAnchorPos(new Vector2(Screen.width * 2, 0), slideDuration).SetEase(Ease.OutExpo);
    }
}
