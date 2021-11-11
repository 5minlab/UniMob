using System;
using UnityEngine;

namespace UniMob
{
    internal class ReactionAtom : AtomBase, Reaction
    {
        private readonly Action _reaction;
        private readonly Action<Exception> _exceptionHandler;
        private readonly string _debugName;

        public ReactionAtom(
            Lifetime lifetime,
            string debugName,
            Action reaction,
            Action<Exception> exceptionHandler = null)
            : base(lifetime, debugName, AtomOptions.AutoActualize)
        {
            _reaction = reaction ?? throw new ArgumentNullException(nameof(reaction));
            _exceptionHandler = exceptionHandler ?? Debug.LogException;
        }

        public void Activate()
        {
            Actualize();
        }

        protected override void Evaluate()
        {
            State = AtomState.Actual;

            try
            {
                _reaction();
            }
            catch (Exception exception)
            {
                _exceptionHandler(exception);
            }
        }
    }
}