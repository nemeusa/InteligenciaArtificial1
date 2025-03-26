using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : Node
{
    public Node trueNode;
    public Node falseNode;
    public TypeAction state;

    public override void Execute(Pj npc)
    {

        switch(state)
        {
            case TypeAction.Steal:
                if (npc.enemy.steal)
                    trueNode.Execute(npc);
                else 
                    falseNode.Execute(npc);
                break;
            
        }
    }
}

public enum TypeAction
{
    Steal,
    dist,
    isArmmed
}
