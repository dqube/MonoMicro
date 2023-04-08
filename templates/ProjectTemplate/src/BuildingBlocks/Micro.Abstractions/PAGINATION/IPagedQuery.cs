using $safeprojectname$.Abstractions;

namespace $safeprojectname$.Pagination;

public interface IPagedQuery : IQuery
{
    int Page { get; set; }
    int Results { get; set; }
}

public interface IPagedQuery<T> : IPagedQuery, IQuery<T>
{
}