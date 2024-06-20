using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PLu.UtilityAI
{
    public interface IAgent
    {
        IAction DecideBestAction(IContext context);
    }
}