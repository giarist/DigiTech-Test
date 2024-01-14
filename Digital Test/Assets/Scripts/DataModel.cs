using UnityEngine;
public class DataModel : MonoBehaviour
{
    [SerializeField] private float resistance  = 1000f;//Значения показателей мультиметра
    [SerializeField] private float power = 400f;
    public float Amperage { private set; get; } = 0f;
    public float Voltage { private set; get; } = 0f;
    public float Resistance //Не даем сопротивлению быть неположотильным
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
    public float Power //Не даем мощности быть отрицательной
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

    public delegate void ValueChanged(); //Делегат и эвент для рассылки изменения значений.
    public static event ValueChanged OnValueChanged;

    void OnValidate() // При изменении значения через редактор, рассылаем уведомление.
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

    private void GetAmperage() //Вычисляем силу тока
    {
        Amperage = Mathf.Sqrt(power / resistance);
    }

    private void GetVoltage() //Вычисляем напряжение
    {
        Voltage = Mathf.Sqrt(power * resistance);
    }

    private void SetValues() //Вычисляем все значения
    {
        GetAmperage();
        GetVoltage();
        RoundData();
        OnValueChanged?.Invoke();
    }

    private float RoundTo2Decimals(float roundNumber) //Округляем значения до двух знаков
    {
        roundNumber = Mathf.Round(roundNumber * 100.0f) * 0.01f;
        return roundNumber;
    }

    private void RoundData() //Округляем все значения
    {
        power = RoundTo2Decimals(power);
        resistance = RoundTo2Decimals(resistance);
        Amperage = RoundTo2Decimals(Amperage);
        Voltage = RoundTo2Decimals(Voltage);
    }
}
