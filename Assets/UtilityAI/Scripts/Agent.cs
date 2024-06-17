using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PLu.UtilityAI
{
    public class Agent : MonoBehaviour, IAgent
    {
        [SerializeField] private IAction[] _actions;
        public IAction DecideBestAction(IContext context)
        {
            if (_actions == null) { return null; }

            float highestScore = float.MinValue;
            IAction bestAction = null;

            foreach (IAction action in _actions) 
            { 
                float score = action.Consider(context);
                if (score > highestScore)
                {
                    highestScore = score;
                    bestAction = action;
                }
            }

            return bestAction;
        }
    }
}
