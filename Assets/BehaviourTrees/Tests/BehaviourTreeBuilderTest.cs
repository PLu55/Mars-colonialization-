using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PLu.BehaviourTrees;

public class BehaviourTreeBuilderTest
{
    [Test]
    public void BuilderSuccessTest()
    {   
        Debug.Log($"Builder Success Test");
        BehaviourTreeBuilder builder = new BehaviourTreeBuilder("BT Test", Policies.RunUntilSuccessOrFailure);
        BehaviourTree behaviourTree = builder
            .Selector("Root")
                .Action("Leaf 1.1", () => Debug.Log("Action 1"))
                .Sequence("Sequence")
                    .Action("Leaf 2.1", () => Debug.Log("Action 1"))
                    .Fail()
                    .Action("Leaf 2.2", () => Debug.Log("Action 2"))
                    .Action("Leaf 2.3", () => Debug.Log("Action 3"))
                .End()
                .PrioritySelector("Priority Selector")
                    .Action("Leaf 3.1", () => Debug.Log("Action 1"))
                    .Delay(1f)
                    .Action("Leaf 3.2", () => Debug.Log("Action 2"))
                .End()
                .RandomSelector("Random Selector")
                    .Action("Leaf 4.1", () => Debug.Log("Action 1"))
                    .Action("Leaf 4.2", () => Debug.Log("Action 2"))
                    .Action("Leaf 4.3", () => Debug.Log("Action 3"))
                .End()
            .End()
            .Build();
        string result = behaviourTree.ToString();
        string pat = "BT Test\n  Root\n    Leaf 1.1\n    Sequence\n      Leaf 2.1\n" + 
                "      Fail\n      Leaf 2.2\n      Leaf 2.3\n" + 
                "    Priority Selector\n      Leaf 3.1\n      Delay\n" + 
                "      Leaf 3.2\n    Random Selector\n      Leaf 4.1\n" + 
                "      Leaf 4.2\n      Leaf 4.3\n";
        Assert.AreEqual(pat, result);
    }
}

