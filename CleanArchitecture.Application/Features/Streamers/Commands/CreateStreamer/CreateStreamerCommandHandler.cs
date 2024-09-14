using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        //se retira este llamdo y se usa UnitOfWOrk
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;
        //sobre clase que se trabaja
        private readonly ILogger<CreateStreamerCommandHandler> _logger;
        public CreateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailServices emailServices, ILogger<CreateStreamerCommandHandler> logger)
        {
            //_streamerRepository = unitOfWork;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailServices = emailServices;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            //Mapeo de la entidad que secreara a mapper
            var streamerEntity = _mapper.Map<Streamer>(request);
            //ya no se estara usano streaerRepository
            //var newStremaer = await _streamerRepository.AddAsync(streamerEntity);
            //ahora se usara la instancia de unitOfWork
            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);
            //confirmar la transaccion --si es menor o igual a 0 dara error
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                throw new Exception($"No se pudo insertar el record de streaemr");
            }
            //_logger.LogInformation($"Streamer {newStremaer.Id} fue creado exitosamente.");
            _logger.LogInformation($"Streamer {streamerEntity.Id} fue creado exitosamente.");
            //llamado del metodo para hacer el envio del correo
            //await SendEmail(newStremaer);
            await SendEmail(streamerEntity);

            //return newStremaer.Id;
            return streamerEntity.Id;
        }
        //Metodo para el envio de correo al realizar la creacion de un streamer
        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "joseayala45@outlook.es",
                Body = "Mensaje de prueba, creación de stremaer",
                Subject = "Mensaje de alerta"
            };
            try
            {
                await _emailServices.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errores al enviar el correo al crear {streamer.Id}");
            }
        }
    }
}
