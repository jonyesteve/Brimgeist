using UnityEngine;

public class StatPanel : MonoBehaviour
{

    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    [SerializeField] private Stat[] stats;

    private void OnValidate()
    {
        UpdateStatNames();
    }
    public void SetStats(params Stat[] charStats)
    {
        stats = charStats;
        if(stats.Length > statDisplays.Length)
        {
            Debug.LogError("ERROR DISPLAYS");
            return;
        }
        for(int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < stats.Length);
            if (i < stats.Length)
            {
                statDisplays[i].stat = stats[i];
            }
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].UpdateStatValue();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].name = statNames[i];
        }
    }
}