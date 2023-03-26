namespace $safeprojectname$.Internals;

public interface IBrokerConventions
{
    string GetTopicNamingConvention(Type type);
    string GetSubscriptionNamingConvention(Type type, string? subscriberId);
}