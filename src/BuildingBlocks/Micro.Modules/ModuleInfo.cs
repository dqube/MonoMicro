using System.Collections.Generic;

namespace Micro.Modules;

public record ModuleInfo(string Name, IEnumerable<string> Policies);