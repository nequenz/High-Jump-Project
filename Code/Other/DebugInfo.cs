using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DebugInfo : MonoBehaviour
{
    private Text _text;

    public string PublicText = "";

    private void Awake()
    {
        _text = GetComponent<Text>();
    }  

    private void Start()
    {
        
    }

    private void Update()
    {
        _text.text = PublicText;
    }
}