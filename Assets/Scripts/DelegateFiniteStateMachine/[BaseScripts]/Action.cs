using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIStateMachine
{
    /// <summary>
    /// -Inherit from this to create an action object.
    /// <para>-These action objects will be run when state is active.</para>
    /// <para>-Do NOT use class variables in these objects.</para>
    /// <para>-Objects are not instanced so any class variables will be changed for
    /// each object in use.</para>
    /// </summary>
    public abstract class Action : ScriptableObject
    {
        /// <summary>
        /// Called once each frame from states fixed update method.
        /// </summary>
        /// <param name="controller">Referacne to instance of controller script.</param>
        public abstract void Act(StateController controller);

        /// <summary>
        /// Called once when the state is first entered.
        /// </summary>
        /// <param name="controller">Referacne to instance of controller script.</param>
        public abstract void FirstCall(StateController controller);

        /// <summary>
        /// Called once after the state is exited.
        /// </summary>
        /// <param name="controller">Referacne to instance of controller script.</param>
        public abstract void LastCall(StateController controller);
    }
}