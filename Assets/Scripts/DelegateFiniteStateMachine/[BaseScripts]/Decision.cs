using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIStateMachine
{
    public abstract class Decision : ScriptableObject
    {
        /// <summary>
        /// Method to be overwritten by inherited classes.
        /// </summary>
        /// <param name="controller">Referacne to instance of controller script.</param>
        public abstract bool Decide(StateController controller);

        public virtual void FirstCall(StateController controller) { }
        public virtual void LastCall(StateController controller) { }

    }
}
