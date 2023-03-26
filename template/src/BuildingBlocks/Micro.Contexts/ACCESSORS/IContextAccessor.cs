namespace $safeprojectname$.Accessors;

public interface IContextAccessor
{
    IContext? Context { get; set; }
}