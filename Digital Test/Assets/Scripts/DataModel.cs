using UnityEngine;
public class DataModel : MonoBehaviour
{
    [SerializeField] private float resistance  = 1000f;//�������� ����������� �����������
    [SerializeField] private float power = 400f;
    public float Amperage { private set; get; } = 0f;
    public float Voltage { private set; get; } = 0f;
    public float Resistance //�� ���� ������������� ���� ���������������
    {
        get { return resistance; }
        private set {            
            if (value <= 0)
            {
                resistance = 0.01f;
            }
            else resistance = value;
            SetValues();
        }
    }
    public float Power //�� ���� �������� ���� �������������
    {
        get { return power; }
        private set
        {
            if (value <= 0)
            {
                power = 0;
            } else power = value;
            SetValues();
        }
    }

    public delegate void ValueChanged(); //������� � ����� ��� �������� ��������� ��������.
    public static event ValueChanged OnValueChanged;

    void OnValidate() // ��� ��������� �������� ����� ��������, ��������� �����������.
    {
        if (resistance <= 0)
        {
            resistance = 0.01f;
        }
        if (power < 0)
        {
            power = 0;
        }
        SetValues();
    }

    void Start()
    {
        SetValues();
    }

    private void GetAmperage() //��������� ���� ����
    {
        Amperage = Mathf.Sqrt(power / resistance);
    }

    private void GetVoltage() //��������� ����������
    {
        Voltage = Mathf.Sqrt(power * resistance);
    }

    private void SetValues() //��������� ��� ��������
    {
        GetAmperage();
        GetVoltage();
        RoundData();
        OnValueChanged?.Invoke();
    }

    private float RoundTo2Decimals(float roundNumber) //��������� �������� �� ���� ������
    {
        roundNumber = Mathf.Round(roundNumber * 100.0f) * 0.01f;
        return roundNumber;
    }

    private void RoundData() //��������� ��� ��������
    {
        power = RoundTo2Decimals(power);
        resistance = RoundTo2Decimals(resistance);
        Amperage = RoundTo2Decimals(Amperage);
        Voltage = RoundTo2Decimals(Voltage);
    }
}
