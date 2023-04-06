using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    [SerializeField] Image healthMask;

    private float originalSize;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        originalSize = healthMask.rectTransform.rect.width;
    }

    public void SetValue(float value)
    {
        healthMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
