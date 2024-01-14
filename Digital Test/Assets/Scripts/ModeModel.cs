using UnityEngine;
using static Controller;
using static DataModel;

public class ModeModel : MonoBehaviour
{
    static public int GRADUADE_SCALE = 24; //Сколько делений в шкале мультиметра    
    public enum OperatingMode { Neutral, Resistance, Amperage, VoltageDC, VoltageAC } //Режимы работы мультиметра
    private OperatingMode _currMode; //Текущий режим работы
    private int _enumCount = 4; //Количество активных режимов
    private float _result = 0f; //Результат работы мультиметра

    [SerializeField] private GameObject _dataGo; //Получаем ссылку на объект с обработчиком значений
    private DataModel _dataModel;    

    public delegate void ModeChanged(OperatingMode mode, float result); //Делегат и эвент для рассылки изменения режима работы
    public static event ModeChanged OnModeChanged;

    void Awake()
    {
        _dataModel = _dataGo.GetComponent<DataModel>();
    }
    void Start()
    {
        SetOperatingMode(0);        
    }    

    void OnEnable() 
    {
        OnInputChanged += SetOperatingMode;
        OnValueChanged += SetResult;
    }
    void OnDisable()
    {
        OnInputChanged -= SetOperatingMode;
        OnValueChanged -= SetResult;
    }  
    private void SetOperatingMode(float state) //Устанавливаем режим работы мультиметра. В данном случае привязаны к шкале
    {        
        if (state == 0)
        {
            _currMode = OperatingMode.Neutral;
        } else if (state <= GRADUADE_SCALE * 1 / _enumCount)
        {
            _currMode = OperatingMode.VoltageDC;
        } else if (state <= GRADUADE_SCALE * 2 / _enumCount)
        {
            _currMode = OperatingMode.VoltageAC;
        } else if (state <= GRADUADE_SCALE * 3 / _enumCount)
        {
            _currMode = OperatingMode.Resistance;
        } else if (state < GRADUADE_SCALE * 4 / _enumCount)
        {
            _currMode = OperatingMode.Amperage;
        }
        SetResult(); //Устанавливаем результат        
    }
    private void SetResult () //Устанавливаем итоговый результат работы мультиметра в зависимости от режима работы
    {   if (_dataModel)
        switch (_currMode)
        {
            case OperatingMode.Neutral:
                _result = 0;
                break;
            case OperatingMode.VoltageDC:
                _result = _dataModel.Voltage;
                break;
            case OperatingMode.VoltageAC:
                _result = 0.01f;
                break;
            case OperatingMode.Resistance:
                _result = _dataModel.Resistance;
                break;
            case OperatingMode.Amperage:
                _result = _dataModel.Amperage;
                break;
            default:
                _result = 0;
                break;
        }
        OnModeChanged?.Invoke(_currMode, _result);
    }
}
