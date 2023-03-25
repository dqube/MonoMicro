﻿namespace Micro.Abstractions.Pagination;

public abstract class PagedQuery<T> : IPagedQuery<Paged<T>>
{
    public int Page { get; set; }
    public int Results { get; set; }
}