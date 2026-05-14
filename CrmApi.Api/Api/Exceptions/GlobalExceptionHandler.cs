namespace CrmApi.Api.Api.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync
            (HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (status, title) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Recurso não encontrado"),
                BusinessRuleException => (StatusCodes.Status409Conflict, "Regra de negócio violada"),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Não autorizado"),
                ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, "Parâmetro inválido"),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Recurso não encontrado"),
                ValidationException => (StatusCodes.Status400BadRequest, "Validação falhou"),
                _ => (StatusCodes.Status500InternalServerError, "Erro interno do servidor")

            };

            var problemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
