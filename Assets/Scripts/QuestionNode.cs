//using System.Collections;
//using System.Collections.Generic;
//using TreeEditor;
//using UnityEngine;

//public class QuestionNode : Node
//{
//    public Node trueNode;
//    public Node falseNode;
//    public TypeQuestion typeQuestion;
//    public override void Execute(Boid boid)
//    {
//        switch (typeQuestion)
//        {
//            case TypeQuestion.Gather:

//                if (boid.isGather) trueNode.Execute(boid);
//                else
//                    falseNode.Execute(boid);

//                break;
//            case TypeQuestion.Flocking:

//                if (boid.Flocking)
//                    trueNode.Execute(boid);
//                else
//                    falseNode.Execute(boid);

//                break;
//            case TypeQuestion.Alone:

//                if (boid.isAlone) trueNode.Execute(boid);
//                else
//                    falseNode.Execute(boid);

//                break;
//            case TypeQuestion.Scape:

//                if(boid.isScape) trueNode.Execute(boid);
//                else 
//                    falseNode.Execute(boid);
//                break;
//        }
//    }
//}

//public enum TypeQuestion
//{
//    Gather,
//    Flocking,
//    Alone,
//    Scape
//}