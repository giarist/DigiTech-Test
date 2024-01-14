using UnityEngine;
using TMPro;
using static ModeModel;

public class HUD_Result : MonoBehaviour
{
    private TMP_Text _uit_result;

    void OnEnable()
    {
        OnModeChanged += UpdateUI;
    }
    void OnDisable()
    {
        OnModeChanged -= UpdateUI;
    }

    void Awake() //Получаем ссылки на компоненты
    {
        _uit_result = GetComponent<TMP_Text>();
    }

    private void UpdateUI(OperatingMode mode, float result) //Обновляем текст 
    {
        _uit_result.text = result.ToString();
    }

}
