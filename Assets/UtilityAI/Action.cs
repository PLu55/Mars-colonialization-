using UnityEngine;

namespace PLu.UtilityAI
{
    public class Action : ScriptableObject, IAction
    {
        private string _name;
        public string Name => _name;

        private float _score;
        public float Score => _score;

        [SerializeField] private Consideration[] _considerations;
        public Consideration[] Considerations => _considerations;

        public float Consider(IContext context)
        {
            float totalScore = 1f;

            foreach (var consideration in _considerations)
            {
                float score = consideration.Consider(context);
                totalScore *= score;

                if (totalScore <= 0)
                {
                    return 0f;
                }
            }

            float originalScore = totalScore;
            float modFactor = 1 - (1 / _considerations.Length);
            float makeupValue = (1 - originalScore) * modFactor;
            _score = originalScore + (makeupValue * originalScore);

            return _score;
        }
        public void Execute(){}
    }
}
