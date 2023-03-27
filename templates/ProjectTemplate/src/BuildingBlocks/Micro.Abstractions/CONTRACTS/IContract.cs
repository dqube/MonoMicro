using System;
using System.Collections.Generic;

namespace $safeprojectname$.Contracts;

public interface IContract
{
    Type Type { get; }
    public IEnumerable<string> Required { get; }
}