using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.CharacterSystem
{


    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    public class Character : ScriptableObject, ICharacter
    {
        [SerializeField] private Stats _stats;
        [SerializeField] private Skill[] _skills;
        public string Name => name;
        public Stats Stats => _stats;
        public Skill[] Skills => _skills;
    }
}
