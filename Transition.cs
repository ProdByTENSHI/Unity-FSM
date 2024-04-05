using System;

namespace FSM
{
    // A Class that defines where and when to Transition
    public class Transition
    {
        // State to Transition to
        public State NextState;
        
        // Delegate that takes in a (anonymous) bool function to check wether to Transition or not
        public Func<bool> Condition;

        public Transition(State nextState, Func<bool> condition)
        {
            NextState = nextState;
            Condition = condition;
        }
    }
}