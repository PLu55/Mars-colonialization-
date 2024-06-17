using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.CharacterSystem
{
    public interface ICharacter
    {
        string Name { get; }
        Stats Stats { get; }
        Skill[] Skills { get; }

    }
}
