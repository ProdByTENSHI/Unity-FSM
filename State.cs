using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    // Base Class that all States derive from
    public abstract class State : MonoBehaviour
    {
        private List<Transition> transitions = new();
        [field: SerializeField] public string Anim { get; protected set; }
        [field: SerializeField] protected float LockTime { get; set; }

        private float lockTimeCounter = 0f;

        // Gets called when this State is transitioned to
        public virtual void Enter()
        {
            lockTimeCounter = LockTime;
        }

        // Gets called every Frame while the State is active
        public virtual void Tick()
        {
            lockTimeCounter -= Time.deltaTime;
        }

        // Gets called when this State is transitioned from
        public virtual void Exit()
        {
        }

        // Adds a Transition to the transitions list
        public void AddTransition(Transition transition)
        {
            transitions.Add(transition);
        }

        // Iterates through each Transition to check if the Condition is met
        //      -> If Yes: Transition to the State
        public bool Evaluate(out State nextState)
        {
            if (lockTimeCounter > 0f)
            {
                nextState = null;
                return false;
            }
            
            foreach (var transition in transitions)
            {
                if (!transition.Condition())
                    continue;

                nextState = transition.NextState;
                return true;
            }

            nextState = null;
            return false;
        }
    }
}