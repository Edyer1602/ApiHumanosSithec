using MediatR;

namespace Application.Features.Operaciones.GetResultOperacion
{
    public class GetResultOperacionQuery : IRequest<decimal>
    {
        public decimal argumento1 { get; set; }
        public decimal argumento2 { get; set; }
        public string operacion { get; set; }
    }

    public class GetResultOperacionQueryHandler : IRequestHandler<GetResultOperacionQuery, decimal>
    {
        public Task<decimal> Handle(GetResultOperacionQuery request, CancellationToken cancellationToken)
        {
            decimal result = 0;
            switch (request.operacion)
            {
                case "*":
                    result= request.argumento1 * request.argumento2;
                    break;
                case "/":
                    result = request.argumento1 / request.argumento2;
                    break;
                case "+":
                    result = request.argumento1 + request.argumento2;
                    break;
                case "-":
                    result = request.argumento1 - request.argumento2;
                    break;
            }

            return Task.FromResult(result);
        }
    }
}
