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
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;
        //sobre clase que se trabaja
        private readonly ILogger<CreateStreamerCommandHandler> _logger;
        public CreateStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, IEmailServices emailServices, ILogger<CreateStreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailServices = emailServices;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            //Mapeo de la entidad que secreara a mapper
            var streamerEntity = _mapper.Map<Streamer>(request);
            var newStremaer = await _streamerRepository.AddAsync(streamerEntity);
            _logger.LogInformation($"Streamer {newStremaer.Id} fue creado exitosamente.");
            //llamado del metodo para hacer el envio del correo
            await SendEmail(newStremaer);

            return newStremaer.Id;
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
