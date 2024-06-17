using System;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.BehaviourTrees
{
    public class BehaviourTreeBuilder
    {
        BehaviourTree _behaviourTree;
        Node Pointer => _stack.Peek();
        Stack<Node> _stack = new();

        public BehaviourTreeBuilder(string name, IPolicy policy = null)
        {
            _behaviourTree = new BehaviourTree(name, policy);
            _stack.Push(_behaviourTree);            
        }
        public BehaviourTreeBuilder Sequence(string name, int priority = 0)
        {
            Node node = new Sequence(name, priority);
            Pointer.AddChild(node);
            _stack.Push(node);
            return this;
        }
        public BehaviourTreeBuilder Selector(string name, int priority = 0)
        {
            Node node = new Selector(name, priority);
            Pointer.AddChild(node);
            _stack.Push(node);
            return this;
        }
        public BehaviourTreeBuilder PrioritySelector(string name, int priority = 0)
        {
            Node node = new PrioritySelector(name, priority);
            Pointer.AddChild(node);
            _stack.Push(node);
            return this;
        }
        public BehaviourTreeBuilder RandomSelector(string name, int priority = 0)
        {
            Node node = new RandomSelector(name, priority);
            Pointer.AddChild(node);
            _stack.Push(node);
            return this;
        }
        public BehaviourTreeBuilder Delay(float delay)
        {
            Pointer.AddChild(new Delay(delay));
            return this;
        } 
        public BehaviourTreeBuilder Fail()
        {
            Pointer.AddChild(new Fail());
            return this;
        }  
        public BehaviourTreeBuilder Action(string name, Action action, int priority = 0)
        {
            IStrategy strategy = new ActionStrategy(action);
            Node node = new Leaf(name, strategy, priority);
            Pointer.AddChild(node);
            return this;
        }
        public BehaviourTreeBuilder Condition(string name, Func<bool> condition, int priority = 0)
        {
            IStrategy strategy = new Condition(condition);
            Node node = new Leaf(name, strategy, priority);
            Pointer.AddChild(node);
            return this;
        }
        public BehaviourTreeBuilder End()
        {
            _stack.Pop();
            if (_stack.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty, too many End() calls.");
            }
            return this;
        }
        public BehaviourTree Build()
        {   
            _stack.Pop();
            if (_stack.Count != 0)
            {
                throw new InvalidOperationException("Stack is not empty, End() calls are missing?");
            }
            return _behaviourTree;
        }
    }
}
