using UnityEngine;

/*
 This class will information for price, names and base settings for all
 possible mines
     */

namespace IdleMiner
{
    [CreateAssetMenu(menuName = "IdleMiner/GameSettings")]
    public class GameSettingsScriptable : ScriptableObject
    {
        public MineParametersScriptalbe[] Mines;
    }
}
