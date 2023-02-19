using System.Text;
using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemSlotText;
    [SerializeField] TextMeshProUGUI itemStatsText;

    private StringBuilder sb = new StringBuilder();
    public void ShowTooltip(EquippableItem item)
    {
        itemNameText.text = item.itemName;
        itemSlotText.text = item.type.ToString();

        sb.Length = 0;
        AddStat(item.StrengthBonus, "Strength");
        AddStat(item.AgilityBonus, "Agility");
        AddStat(item.IntelligenceBonus, "Intelligence");
        AddStat(item.VitalityBonus, "Vitality");

        AddStat(item.StrengthPercentBonus, "Strength", true);
        AddStat(item.AgilityPercentBonus, "Agility", true);
        AddStat(item.IntelligencePercentBonus, "Intelligence", true);
        AddStat(item.VitalityPercentBonus, "Vitality", true);

        itemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }
            if (value > 0)
                sb.Append("+");
            if (isPercent)
            {
                sb.Append(value);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
