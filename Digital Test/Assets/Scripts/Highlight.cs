using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Highlight : MonoBehaviour
{
    private Color _baseColor;            
    private MeshRenderer _meshRenderer;

    public delegate void ActiveChanged(bool isActive); //Делегат и эвент для рассылки изменения инпута.
    public static event ActiveChanged OnActiveChanged;

    [SerializeField] private bool isControl; //Блокируем управление до наведения на объект

    void Awake()    //Получаем изначальный цвет материала и ссылку на меш
    {   
        _meshRenderer = GetComponent<MeshRenderer>(); 
        _baseColor = _meshRenderer.material.color;
    }

    private void Start()
    {
        if (isControl)  //Если управляем контроллером, то блокируем его на старте
            OnActiveChanged?.Invoke(false);
    }
    void OnMouseEnter() //При наведении мыши меняем материал
    {
        ActivateHighlight();
        if (isControl)
            OnActiveChanged?.Invoke(true);
    }
    void OnMouseExit() //При выводе мыши ставим материал обратно
    {
        DectivateHighlight();
        if (isControl)
            OnActiveChanged?.Invoke(false);
    }

    void ActivateHighlight() //Включаем / выключаем подсветку
    {
        _meshRenderer.material.color = Color.yellow;
    }
    void DectivateHighlight()
    {
        _meshRenderer.material.color = _baseColor;
    }
}
