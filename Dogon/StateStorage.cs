using System;

namespace Dogon
{
    public class StateStorage
    {
        private readonly Action<BankStorage> _bankStoreStrategy;

        public StateStorage(Func<BankStorage> bankSourceStrategy, Action<BankStorage> bankStoreStrategy)
        {
            State = bankSourceStrategy.Invoke();
            _bankStoreStrategy = bankStoreStrategy;
        }

        public BankStorage State { get; }

        public void Update()
        {
            _bankStoreStrategy.Invoke(State);
        }
    }
}