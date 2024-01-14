using UnityEngine;
using static Highlight;
using static ModeModel;

public class Controller : MonoBehaviour
{
    private float _input; //Отвечает за прием с колеса мыши    
    private float _output; //Вывод с колеса мыши

    public delegate void InputChanged(float output); //Делегат и эвент для рассылки изменения ввода
    public static event InputChanged OnInputChanged;

    private bool _isActive; //Активно ли управление

    void OnEnable()
    {
        OnActiveChanged += SetActive;
    }
    void OnDisable()
    {
        OnActiveChanged -= SetActive;
    }

    void Awake()
    {   
        _isActive = true;
        _output = 0;
    }

    void SetActive(bool isActive) //Устанавливаем активность управления извне
    {
        _isActive = isActive;
    }

    void Update()
    {
        _input = Input.GetAxis("Mouse ScrollWheel"); //Следим за движением колеса. При шевелении делаем рассылку        

        if (_isActive) {
            if (_input != 0.0f)
                InputHandler();
                OnInputChanged?.Invoke(_output);           
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void InputHandler() {
        _output += _input * 10;

        //Позволяем двигаться только в пределах одного круга.
        if (_output < 0)
        {
            _output += GRADUADE_SCALE;
        }
        if (_output >= GRADUADE_SCALE)
        {
            _output -= GRADUADE_SCALE;
        }
    }
}
    
