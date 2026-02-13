using UnityEngine;
using UnityEngine.UI;
public class ErrorPanel : MonoBehaviour
{
    [SerializeField] Text errorText;

    private void Awake()
    {
        errorText = GetComponentInChildren<Text>();
    }
    public void SetText(string message)
    {
        errorText.text = message;
    }
}
