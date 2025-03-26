using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : Node
{
    public Node trueNode;
    public Node falseNode;
    public ThieftState state;

    public override void Execute(Pj npc)
    {

        switch(state)
        {
            case ThieftState.Steal:
                if (npc.enemy.steal)
                    trueNode.Execute(npc);
                else 
                    falseNode.Execute(npc);
                break;
            
        }
    }
}

public enum ThieftState
{
    Steal,
    dist,
    isArmmed
}
