using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image image;
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.eulerAngles = new Vector3(-90, 0, 0);
    }

    public void SetHealthBar(int maxHP, int currentHP)
    {
        if (image == null) return;
        float value = (float)currentHP / (float)maxHP;

        image.fillAmount = value;
    }
}
