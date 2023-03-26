using System.Net;

namespace $safeprojectname$.Exceptions;

public sealed record ExceptionResponse(object Response, HttpStatusCode StatusCode);