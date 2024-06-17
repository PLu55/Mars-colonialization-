using UnityEngine;

namespace PLu.UtilityAI
{
    public abstract class Consideration : ScriptableObject, IConsideration
    {
        [SerializeField] protected AnimationCurve _responseCurve;

        private string _name;
        public string Name => _name;

        private float _score;
        public float Score => _score;

         public virtual float Consider(IContext context)
        {
            return _score;
        }
    }
}
