using UnityEngine;
using TMPro;
using static ModeModel;
using System.Text;

public class HUD_TextData : MonoBehaviour
{
    [SerializeField] private OperatingMode _mode; //Рабочий режим конкретного текста
        
    private TMP_Text _tmp;      //Получаем базовые данные из TMP
    private string _baseText;
    private Color _baseColor;

    [SerializeField] private bool _isLabel; //Флаг, показываем только заголовок или результат тоже
    

    void OnEnable()
    {
        OnModeChanged += CheckMode;
    }
    void OnDisable()
    {
        OnModeChanged -= CheckMode;
    }

    void Awake()
    {
        _tmp = GetComponent<TMP_Text>();         //Получаем изначальный цвет материала и ссылку на текст              
        _baseColor = _tmp.color;
        _baseText = _tmp.text;
    }

    private void CheckMode(OperatingMode mode, float result)  //Проверяем совпадает ли режим работы с нашим текстом.
    {
        if (_mode == mode)
        {
            CheckLabel(_tmp, result);
            ActivateHighlight();
        }
        else
        {
            CheckLabel(_tmp, 0);
            DectivateHighlight();
        }
    }

    private void ActivateHighlight() //Включаем / выключаем подсветку
    {
        _tmp.color = Color.yellow;
    }
    private void DectivateHighlight()
    {
        _tmp.color = _baseColor;
    }

    private string ConvertNumberToText(TMP_Text tempText, float number) //Переводим число в текст
    {
        tempText.text = number.ToString();
        return tempText.text;
    }

    private void CheckLabel(TMP_Text tempText, float number) //Если текст - заголовок, то оставляем его, если нет - выводим так же результат
    {
        if (_isLabel)
        {   
            tempText.text = _baseText;
        }
        else
        {
            string tempValue = ConvertNumberToText(_tmp, number);
            tempText.text = $"{_baseText} {tempValue}"; //Не удалось сделать сложение символа юникода с другими символами в одной строке =(
            
        }
    }
}
