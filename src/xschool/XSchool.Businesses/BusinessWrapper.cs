using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace XSchool.Businesses
{
    public class BusinessWrapper
    {
        public void TransactionExcute(Action action)
        {
            using (var trans = new TransactionScope())
            {
                action.Invoke();
                trans.Complete();
            }
        }
    }
}
