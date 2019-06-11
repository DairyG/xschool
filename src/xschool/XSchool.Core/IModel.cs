using System;

namespace XSchool.Core
{
    public interface IModel<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
