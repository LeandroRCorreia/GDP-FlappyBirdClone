using UnityEngine;

[CreateAssetMenu(menuName = "Data/ObjetPool/ObjPoolParameters")]
public class ObjetPoolParameters : ScriptableObject
{
    [field:SerializeField] public int initialSizePool {get; private set;}
    [field:SerializeField] public int newResizePool {get; private set;}
    
}
