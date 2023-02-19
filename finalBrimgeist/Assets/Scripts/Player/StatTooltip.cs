using System.Text;
using UnityEngine;
using TMPro;

public class StatTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statModifierLabelText;
    [SerializeField] TextMeshProUGUI statModifiersText;

    private StringBuilder sb = new StringBuilder();
    public void ShowTooltip(Stat stat)
    {
        statNameText.text = GetStatTopText(stat);
        statModifiersText.text = GetStatModifiersText(stat);

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private string GetStatTopText(Stat stat)
    {
        sb.Length = 0;
        sb.Append(stat.StatName);
        sb.Append(" ");
        sb.Append(stat.FinalValue);

        if(stat.FinalValue != stat.baseValue)
        {
            sb.Append(" (");
            sb.Append(stat.baseValue);

            if (stat.FinalValue > stat.baseValue)
                sb.Append("+");
            sb.Append(System.Math.Round(stat.FinalValue - stat.baseValue, 1));
            sb.Append(")");
        }
        

        return sb.ToString();

    }

    string GetStatModifiersText(Stat stat)
    {
        sb.Length = 0;
        foreach(var mod in stat.valueModifiers)
        {
            if(sb.Length > 0)
            {
                sb.AppendLine();
            }
            
            if (mod.value > 0)
                sb.Append("+");

            if(mod.type == Types.StatModType.Flat)
                sb.Append(mod.value);
            else
            {
                sb.Append(mod.value);
                sb.Append("%");
            }

            var item = mod.source as EquippableItem;

            if (item != null)
            {
                sb.Append(" ");
                sb.Append(item.itemName);
            }
            else
                Debug.LogError("Modifier is not equippable item");
        }

        return sb.ToString();
    }
}