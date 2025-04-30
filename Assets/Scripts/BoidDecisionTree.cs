//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BoidDecisionTree
//{
    //private Node _root;

    //public BoidDecisionTree()
    //{
    //    var flee = new ActionNode(b => b.FleeFromHunter(b));
    //    var flock = new ActionNode(b => b.DoFlocking(b));
    //    var idle = new ActionNode(b => b.Idle(b));

    //    var scapeQ = new QuestionNode
    //    {
    //        typeQuestion = TypeQuestion.Scape,
    //        trueNode = flee,
    //        falseNode = flock
    //    };

    //    var aloneQ = new QuestionNode
    //    {
    //        typeQuestion = TypeQuestion.Alone,
    //        trueNode = idle,
    //        falseNode = scapeQ
    //    };

    //    _root = aloneQ;
    //}

    //public void Execute(Boid boid)
    //{
    //    _root.Execute(boid);
    //}
//}
