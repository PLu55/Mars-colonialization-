using UnityEngine;

namespace PLu.Mars.CharacterSystem
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Mars/Skill", order = 0)]
    public class Skill : ScriptableObject
    {
        [SerializeField] private string _description;
        [SerializeField] private float _level;
        [SerializeField] private Sprite _icon;

        public string Name => name;
        public string Description => _description;
        public float Level => _level;
        public Sprite Icon => _icon;
    }
}
