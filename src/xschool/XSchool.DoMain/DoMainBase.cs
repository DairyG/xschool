using System;
using System.Transactions;

namespace XSchool.DoMain
{
    public class DoMainBase
    {
        public void Excute(Action action)
        {
            using (var scope = new TransactionScope())
            {
                action.Invoke();
                scope.Complete();
            }
        }
    }
}

