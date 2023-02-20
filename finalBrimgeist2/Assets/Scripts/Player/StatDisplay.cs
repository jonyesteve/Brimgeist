using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Stat _stat;
    public Stat stat
    {
        get => _stat;
        set
        {
            _stat = value;
            UpdateStatValue();
        }
    }

    
    
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI valueText;
    [SerializeField] StatTooltip tooltip;


    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(stat);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    public void UpdateStatValue()
    {
        valueText.text = _stat.FinalValue.ToString();
    }
}
