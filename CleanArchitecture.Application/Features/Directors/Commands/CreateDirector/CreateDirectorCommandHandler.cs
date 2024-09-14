using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
    {
        private readonly ILogger<CreateDirectorCommand> _logger;
        private readonly IMapper _mapper;
        //llamar a la referencia de UnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        //constructor para inyeccion de objetos
        public CreateDirectorCommandHandler(ILogger<CreateDirectorCommand> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            //mapear los datos que esta enviando el cliente a un formato entidad
            var directorEntity = _mapper.Map<Director>(request);
            //insertar este objeto de tipo director en director de la base de datos
            
            //se agrega el record en la memoria, no esta en base ed datos
            _unitOfWork.Repository<Director>().AddEntity(directorEntity);
            //realiza la transaccion y es de tipo asincrono
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                _logger.LogError("No se inserto el record del director");
                throw new Exception("No se pudo insertar el record del director.");
            }
            return directorEntity.Id;
        }
    }
}
