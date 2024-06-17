using System.Text;
using UnityEngine;

namespace PLu.BehaviourTrees
{
    public class BehaviourTree : Node {
        readonly IPolicy policy;
        
        public BehaviourTree(string name, IPolicy policy = null) : base(name) {
            this.policy = policy ?? Policies.RunForever;
        }

        public override Status Process() {
            Status status = children[currentChild].Process();
            if (policy.ShouldReturn(status)) {
                return status;
            }
            
            currentChild = (currentChild + 1) % children.Count;
            return Status.Running;
        }

        public override string ToString(){            
            StringBuilder sb = new StringBuilder();
            PrintNode(this, 0, sb);
            return sb.ToString();
        }
        public void PrintTree() {
            Debug.Log(ToString());
        }

        static void PrintNode(Node node, int indentLevel, StringBuilder sb) {
            sb.Append(' ', indentLevel * 2).AppendLine(node.name);
            foreach (Node child in node.children) {
                PrintNode(child, indentLevel + 1, sb);
            }
        }
    }
}