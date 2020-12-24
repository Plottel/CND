using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deft.Input
{
    public class InputSnapshot
    {
        public ActionSnapshot[] actionSnapshots { get; private set; }

        public InputSnapshot(int actionCount)
        {
            actionSnapshots = new ActionSnapshot[actionCount];
        }

        public ActionSnapshot GetActionSnapshot(int action) => actionSnapshots[action];

        public void SetActionSnapshot(int action, ActionSnapshot snapshot)
        {
            actionSnapshots[action] = snapshot;
        }     

        public bool GetActuatedActions(out List<ActionSnapshot> actions)
        {
            actions = new List<ActionSnapshot>();

            foreach (var action in actionSnapshots)
            {
                if (action.state != InputState.None)
                    actions.Add(action);
            }

            return actions.Count > 0;
        }
    }
}

