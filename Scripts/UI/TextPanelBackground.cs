using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelBackground : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float maxAlpha;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        var alpha = 0f;

        if (!string.IsNullOrEmpty(_text.text))
        {
            alpha = maxAlpha * _text.color.a;
        }

        Color color = _image.color;
        color = new Color(color.r, color.g, color.b, alpha);
        _image.color = color;
    }
}