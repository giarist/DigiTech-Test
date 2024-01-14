using UnityEngine;
using static Highlight;
using static ModeModel;

public class Controller : MonoBehaviour
{
    private float _input; //�������� �� ����� � ������ ����    
    private float _output; //����� � ������ ����

    public delegate void InputChanged(float output); //������� � ����� ��� �������� ��������� �����
    public static event InputChanged OnInputChanged;

    private bool _isActive; //������� �� ����������

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

    void SetActive(bool isActive) //������������� ���������� ���������� �����
    {
        _isActive = isActive;
    }

    void Update()
    {
        _input = Input.GetAxis("Mouse ScrollWheel"); //������ �� ��������� ������. ��� ��������� ������ ��������        

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

        //��������� ��������� ������ � �������� ������ �����.
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
    
