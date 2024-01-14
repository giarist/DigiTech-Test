using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Highlight : MonoBehaviour
{
    private Color _baseColor;            
    private MeshRenderer _meshRenderer;

    public delegate void ActiveChanged(bool isActive); //������� � ����� ��� �������� ��������� ������.
    public static event ActiveChanged OnActiveChanged;

    [SerializeField] private bool isControl; //��������� ���������� �� ��������� �� ������

    void Awake()    //�������� ����������� ���� ��������� � ������ �� ���
    {   
        _meshRenderer = GetComponent<MeshRenderer>(); 
        _baseColor = _meshRenderer.material.color;
    }

    private void Start()
    {
        if (isControl)  //���� ��������� ������������, �� ��������� ��� �� ������
            OnActiveChanged?.Invoke(false);
    }
    void OnMouseEnter() //��� ��������� ���� ������ ��������
    {
        ActivateHighlight();
        if (isControl)
            OnActiveChanged?.Invoke(true);
    }
    void OnMouseExit() //��� ������ ���� ������ �������� �������
    {
        DectivateHighlight();
        if (isControl)
            OnActiveChanged?.Invoke(false);
    }

    void ActivateHighlight() //�������� / ��������� ���������
    {
        _meshRenderer.material.color = Color.yellow;
    }
    void DectivateHighlight()
    {
        _meshRenderer.material.color = _baseColor;
    }
}
