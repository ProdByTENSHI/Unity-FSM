using System;

namespace FSM
{
    // FSM that handles State Transitioning and State Updating
    public class FiniteStateMachine
    {
        public State CurrentState { get; private set; }
        public Action<State> OnStateChange = delegate(State state) { };

        public FiniteStateMachine(State currentState)
        {
            CurrentState = currentState;
            CurrentState.Enter();

            OnStateChange?.Invoke(currentState);
        }

        public void Update()
        {
            EvaluateStates();
            CurrentState.Tick();
        }

        private void EvaluateStates()
        {
            // Iterate through each State and check if the Condition is true
            while (CurrentState.Evaluate(out var nextState))
            {
                CurrentState.Exit();
                CurrentState = nextState;
                CurrentState.Enter();

                OnStateChange?.Invoke(CurrentState);
            }
        }
    }
}