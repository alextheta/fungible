using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelBackground : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private float _defaultAlpha;
    private Image _image;
    
    private void Start()
    {
        _image = GetComponent<Image>();
        _defaultAlpha = _image.color.a;
    }

    private void Update()
    {
        float alpha = _defaultAlpha * _text.color.a;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
    }
}
