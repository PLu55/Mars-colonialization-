using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PLu.BehaviourTrees;

public class BehaviourTreeTestScript
{
    [UnityTest]
    public IEnumerator SequenceSuccessTest()
    {   
        Debug.Log($"Success Test");
        BehaviourTree behaviourTree = new BehaviourTree("Test", Policies.RunUntilSuccessOrFailure);
        Node root = new Sequence("Root");
        behaviourTree.AddChild(root);
        root.AddChild(new Leaf("Leaf 1", new ActionStrategy(() => Debug.Log("Action 1"))));
        root.AddChild(new Leaf("Leaf 2", new ActionStrategy(() => Debug.Log("Action 2"))));
        root.AddChild(new Leaf("Leaf 3", new ActionStrategy(() => Debug.Log("Action 3"))));
        behaviourTree.PrintTree();

        int cnt = 10;
        while (cnt-- > 0)
        {
            Node.Status status = behaviourTree.Process();
            Debug.Log($"Status: {status} cnt {cnt}");
            if (status != Node.Status.Running)
            {
                Assert.AreEqual(Node.Status.Success, status);
                Assert.AreEqual(7, cnt);
                yield break;
            }
            yield return null;
        }
    }
    [UnityTest]
    public IEnumerator SequenceFailureTest()
    {   
        Debug.Log($"Failure Test");
        BehaviourTree behaviourTree = new BehaviourTree("Test", Policies.RunUntilSuccessOrFailure);
        Node root = new Sequence("Root");
        behaviourTree.AddChild(root);
        root.AddChild(new Leaf("Leaf 1", new ActionStrategy(() => Debug.Log("Action 1"))));
        root.AddChild(new Leaf("Leaf 2", new ActionStrategy(() => Debug.Log("Action 2"))));
        root.AddChild(new Fail());
        root.AddChild(new Leaf("Leaf 3", new ActionStrategy(() => Debug.Log("Action 3"))));
        behaviourTree.PrintTree();

        int cnt = 10;
        while (cnt-- > 0)
        {
            Node.Status status = behaviourTree.Process();
            Debug.Log($"Status: {status} cnt {cnt}");
            if (status != Node.Status.Running)
            {
                Assert.AreEqual(Node.Status.Failure, status);
                Assert.AreEqual(7, cnt);
                yield break;
            }
            yield return null;
        }
    }
    [UnityTest]
    public IEnumerator SelectorSuccessTest()
    {   
        Debug.Log($"Selector Success Test");
        int result = 0;

        BehaviourTree behaviourTree = new BehaviourTree("Test", Policies.RunUntilSuccessOrFailure);
        Node root = new Selector("Root");
        behaviourTree.AddChild(root);
        root.AddChild(new Fail());
        root.AddChild(new Leaf("Leaf 1", new ActionStrategy(() => result = 1)));
        root.AddChild(new Leaf("Leaf 2", new ActionStrategy(() => result = 2)));
        behaviourTree.PrintTree();

        int cnt = 10;
        while (cnt-- > 0)
        {
            Node.Status status = behaviourTree.Process();
            Debug.Log($"Status: {status} cnt {cnt}");
            if (status != Node.Status.Running)
            {
                Assert.AreEqual(Node.Status.Success, status);
                Assert.AreEqual(1, result);
                Assert.AreEqual(8, cnt);
                yield break;
            }
            yield return null;
        }
    }
    [UnityTest]
    public IEnumerator PrioritySuccessTest()
    {   
        Debug.Log($"PrioritySelector Success Test");
        int result = 0;

        BehaviourTree behaviourTree = new BehaviourTree("Test", Policies.RunUntilSuccessOrFailure);
        Node root = new PrioritySelector("Root");
        behaviourTree.AddChild(root);
        root.AddChild(new Leaf("Leaf 1", new ActionStrategy(() => result = 1), 2));
        root.AddChild(new Leaf("Leaf 2", new ActionStrategy(() => result = 2), 3));
        root.AddChild(new Fail(4));
        behaviourTree.PrintTree();

        int cnt = 10;
        while (cnt-- > 0)
        {
            Node.Status status = behaviourTree.Process();
            Debug.Log($"Status: {status} cnt {cnt}");
            if (status != Node.Status.Running)
            {
                Assert.AreEqual(Node.Status.Success, status);
                Assert.AreEqual(2, result);
                Assert.AreEqual(9, cnt);
                yield break;
            }
            yield return null;
        }
    }    
    [UnityTest]
    public IEnumerator DelayTest()
    {   
        Debug.Log($"Delay Test");
        yield return new EnterPlayMode();
        float delay = 0.02f;
        BehaviourTree behaviourTree = new BehaviourTree("Delay Test", Policies.RunUntilSuccessOrFailure);
        Node root = new Delay(delay);
        behaviourTree.AddChild(root);

        float startTime = Time.time;
        int cnt = 2000;
        while (cnt-- > 0)
        {
            float time = Time.time - startTime;
            Node.Status status = behaviourTree.Process();
            if (status != Node.Status.Running || time * (1f - 1e-2) >= delay)
            {
                Assert.AreEqual(Node.Status.Success, status);
                Assert.That(time, Is.InRange(delay, delay + 1e-2f));

                yield return new ExitPlayMode();
                yield break;
            }

            yield return null;
        }

        Assert.Fail("Delay test failed");
        yield return new ExitPlayMode();
    }
}

