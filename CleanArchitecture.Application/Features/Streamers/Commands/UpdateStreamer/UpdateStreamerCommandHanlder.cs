using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHanlder : IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        //sobre clase que se trabaja
        private readonly ILogger<UpdateStreamerCommandHanlder> _logger;

        public UpdateStreamerCommandHanlder(IStreamerRepository streamerRepository, IMapper mapper, ILogger<UpdateStreamerCommandHanlder> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _streamerRepository.GetByIdAsync(request.Id);
            if (streamerToUpdate == null)
            {
                _logger.LogError($"No se encontro el streamer Id {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }
            // request(por donde usuario envia los nuevos datos) -->
            // streamerToUpdate = objecto que ya viene de la base de datos
            _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));

            await _streamerRepository.UpdateAsync(streamerToUpdate);
            _logger.LogInformation($"La operacion fue exitosa actualizando el streamer");

            return Unit.Value;
        }
    }
}
