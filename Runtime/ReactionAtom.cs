using System;
using UnityEngine;

namespace UniMob
{
    public class ReactionAtom : AtomBase, IDisposable
    {
        private readonly Action _reaction;
        private readonly Action<Exception> _exceptionHandler;
        private readonly string _debugName;

        internal ReactionAtom(string debugName, Action reaction, Action<Exception> exceptionHandler = null)
            : base(debugName, true, null)
        {
            _reaction = reaction ?? throw new ArgumentNullException(nameof(reaction));
            _exceptionHandler = exceptionHandler ?? Debug.LogException;
        }

        public void Dispose()
        {
            Deactivate();
        }

        public void Get()
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