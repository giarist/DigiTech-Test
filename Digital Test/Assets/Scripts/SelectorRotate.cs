using UnityEngine;
using static Controller;
using static ModeModel;

public class SelectorRotate : MonoBehaviour
{
    private float _wheelSpeed; //Скорость вращения колеса - зависит от количества делений на шкале мультиметра

    void OnEnable()
    {
        OnInputChanged += UpdateSelector;
    }
    void OnDisable()
    {
        OnInputChanged -= UpdateSelector;
    }

    private void Start()
    {
        _wheelSpeed = 360 / GRADUADE_SCALE;
    }

    void UpdateSelector(float output)
    {
        transform.localRotation = Quaternion.Euler(0, output * _wheelSpeed, 0); 
    }
}
