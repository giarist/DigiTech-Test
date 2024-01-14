using UnityEngine;
using TMPro;
using static ModeModel;
using System.Text;

public class HUD_TextData : MonoBehaviour
{
    [SerializeField] private OperatingMode _mode; //������� ����� ����������� ������
        
    private TMP_Text _tmp;      //�������� ������� ������ �� TMP
    private string _baseText;
    private Color _baseColor;

    [SerializeField] private bool _isLabel; //����, ���������� ������ ��������� ��� ��������� ����
    

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
        _tmp = GetComponent<TMP_Text>();         //�������� ����������� ���� ��������� � ������ �� �����              
        _baseColor = _tmp.color;
        _baseText = _tmp.text;
    }

    private void CheckMode(OperatingMode mode, float result)  //��������� ��������� �� ����� ������ � ����� �������.
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

    private void ActivateHighlight() //�������� / ��������� ���������
    {
        _tmp.color = Color.yellow;
    }
    private void DectivateHighlight()
    {
        _tmp.color = _baseColor;
    }

    private string ConvertNumberToText(TMP_Text tempText, float number) //��������� ����� � �����
    {
        tempText.text = number.ToString();
        return tempText.text;
    }

    private void CheckLabel(TMP_Text tempText, float number) //���� ����� - ���������, �� ��������� ���, ���� ��� - ������� ��� �� ���������
    {
        if (_isLabel)
        {   
            tempText.text = _baseText;
        }
        else
        {
            string tempValue = ConvertNumberToText(_tmp, number);
            tempText.text = $"{_baseText} {tempValue}"; //�� ������� ������� �������� ������� ������� � ������� ��������� � ����� ������ =(
            
        }
    }
}
