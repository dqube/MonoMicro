using Micro.Abstractions.Abstractions;

namespace Micro.Abstractions.Pagination;

public interface IPagedQuery<T> : IQuery<T>
{
    int Page { get; set; }
    int Results { get; set; }
}