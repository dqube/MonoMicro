using Micro.Abstractions.Abstractions;

namespace Micro.Abstractions.Pagination;

public interface IPagedQuery : IQuery
{
    int Page { get; set; }
    int Results { get; set; }
}

public interface IPagedQuery<T> : IPagedQuery, IQuery<T>
{
}