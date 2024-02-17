using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Data/RewardMedal")]
public class Medal : ScriptableObject
{
    [field: SerializeField] 
    public Sprite MedalSprite {get; private set; }

    [field: SerializeField] 
    public int MinScore {get; private set;} = 10;

}

[CreateAssetMenu(menuName = "Data/RewardCalculator")]
public class RewardSettings : ScriptableObject
{
    [SerializeField] 
    Medal[] medals;

    public Medal CalculateMedal(int score)
    {
        
        for (int i = medals.Length - 1; i >= 0; i--)
        {
            if(score >= medals[i].MinScore)
            {
                return medals[i];
            }

        }

        return null;
    }


}
