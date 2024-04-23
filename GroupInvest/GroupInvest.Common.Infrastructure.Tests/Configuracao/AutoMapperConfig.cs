using AutoMapper;
using AuditoriaCommand = GroupInvest.Microservices.Auditoria.Application.Commands;
using AuditoriaDomain = GroupInvest.Microservices.Auditoria.Domain;
using ParticipantesCommand = GroupInvest.Microservices.Participantes.Application.Commands;
using ParticipantesDomain = GroupInvest.Microservices.Participantes.Domain;
using ParticipantesDto = GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using ParticipantesEvent = GroupInvest.Microservices.Participantes.Application.Events;
using MensalidadesCommand = GroupInvest.Microservices.Mensalidades.Application.Commands;
using MensalidadesDomain = GroupInvest.Microservices.Mensalidades.Domain;
using MensalidadesEvent = GroupInvest.Microservices.Mensalidades.Application.Events;

namespace GroupInvest.Common.Infrastructure.Tests.Configuracao
{
    public class AutoMapperConfig
    {
        private static IMapper mapper;
        public static IMapper Mapper()
        {
            if (mapper == null)
            {

                var config = new MapperConfiguration(cfg =>
                {
                    #region Microsserviço de Participantes
                    cfg.CreateMap<ParticipantesCommand.Participantes.IncluirParticipanteCommand, ParticipantesDomain.Entidades.Participante>();

                    cfg.CreateMap<ParticipantesCommand.Participantes.AlterarParticipanteCommand, ParticipantesDomain.Entidades.Participante>()
                        .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.ParticipanteId));

                    cfg.CreateMap<ParticipantesCommand.Participantes.InativarParticipanteCommand, ParticipantesDomain.Entidades.Participante>()
                        .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.ParticipanteId));

                    cfg.CreateMap<ParticipantesDomain.Entidades.Participante, ParticipantesEvent.Participantes.ParticipanteIncluidoEvent>();
                    cfg.CreateMap<ParticipantesDomain.Entidades.Participante, ParticipantesEvent.Participantes.ParticipanteAlteradoEvent>();
                    cfg.CreateMap<ParticipantesDomain.Entidades.Participante, ParticipantesEvent.Participantes.ParticipanteInativadoEvent>();

                    cfg.CreateMap<ParticipantesDomain.Entidades.Participante, ParticipantesDto.ParticipanteDto>();

                    cfg.CreateMap<ParticipantesDomain.Entidades.Adesao, ParticipantesEvent.Participantes.AdesaoRealizadaEvent>();

                    cfg.CreateMap<ParticipantesDomain.Entidades.Adesao, ParticipantesDto.AdesaoDto>()
                        .ForMember(dto => dto.ParticipanteId, ades => ades.MapFrom(ad => ad.Participante.Id))
                        .ForMember(dto => dto.PeriodoId, ades => ades.MapFrom(ad => ad.Periodo.Id));

                    cfg.CreateMap<ParticipantesCommand.Periodos.IncluirPeriodoCommand, ParticipantesDomain.Entidades.Periodo>();

                    cfg.CreateMap<ParticipantesCommand.Periodos.AlterarPeriodoCommand, ParticipantesDomain.Entidades.Periodo>()
                        .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.PeriodoId));

                    cfg.CreateMap<ParticipantesCommand.Periodos.EncerrarPeriodoCommand, ParticipantesDomain.Entidades.Periodo>()
                        .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.PeriodoId));

                    cfg.CreateMap<ParticipantesDomain.Entidades.Periodo, ParticipantesEvent.Periodos.PeriodoIncluidoEvent>();
                    cfg.CreateMap<ParticipantesDomain.Entidades.Periodo, ParticipantesEvent.Periodos.PeriodoAlteradoEvent>();
                    cfg.CreateMap<ParticipantesDomain.Entidades.Periodo, ParticipantesEvent.Periodos.PeriodoEncerradoEvent>();

                    cfg.CreateMap<ParticipantesDomain.Entidades.Periodo, ParticipantesDto.PeriodoDto>();
                    #endregion

                    #region Microsserviço de Mensalidades
                    cfg.CreateMap<MensalidadesEvent.Adesoes.IntegracaoAdesaoRealizadaEvent, MensalidadesCommand.Periodos.IncluirPeriodoCommand>()
                                  .ForMember(cmd => cmd.PeriodoId, eve => eve.MapFrom(ev => ev.Periodo.Id))
                                  .ForMember(cmd => cmd.AnoReferencia, eve => eve.MapFrom(ev => ev.Periodo.AnoReferencia))
                                  .ForMember(cmd => cmd.DataInicioPeriodo, eve => eve.MapFrom(ev => ev.Periodo.DataInicio))
                                  .ForMember(cmd => cmd.DataTerminoPeriodo, eve => eve.MapFrom(ev => ev.Periodo.DataTermino));

                    cfg.CreateMap<MensalidadesEvent.Adesoes.IntegracaoAdesaoRealizadaEvent, MensalidadesCommand.Participantes.IncluirParticipanteCommand>()
                                  .ForMember(cmd => cmd.ParticipanteId, eve => eve.MapFrom(ev => ev.Participante.Id))
                                  .ForMember(cmd => cmd.Nome, eve => eve.MapFrom(ev => ev.Participante.Nome));

                    cfg.CreateMap<MensalidadesCommand.Periodos.IncluirPeriodoCommand, MensalidadesDomain.Entidades.Periodo>()
                                  .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.PeriodoId))
                                  .ForMember(p => p.AnoReferencia, cmd => cmd.MapFrom(c => c.AnoReferencia))
                                  .ForMember(p => p.DataInicioPeriodo, cmd => cmd.MapFrom(c => c.DataInicioPeriodo))
                                  .ForMember(p => p.DataTerminoPeriodo, cmd => cmd.MapFrom(c => c.DataTerminoPeriodo));

                    cfg.CreateMap<MensalidadesCommand.Periodos.AlterarPeriodoCommand, MensalidadesDomain.Entidades.Periodo>()
                                  .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.PeriodoId))
                                  .ForMember(p => p.AnoReferencia, cmd => cmd.MapFrom(c => c.AnoReferencia))
                                  .ForMember(p => p.DataInicioPeriodo, cmd => cmd.MapFrom(c => c.DataInicioPeriodo))
                                  .ForMember(p => p.DataTerminoPeriodo, cmd => cmd.MapFrom(c => c.DataTerminoPeriodo));

                    cfg.CreateMap<MensalidadesCommand.Participantes.IncluirParticipanteCommand, MensalidadesDomain.Entidades.Participante>()
                                  .ForMember(p => p.Id, cmd => cmd.MapFrom(c => c.ParticipanteId))
                                  .ForMember(p => p.Nome, cmd => cmd.MapFrom(c => c.Nome));
                    #endregion

                    #region Microsserviço de Auditoria
                    cfg.CreateMap<AuditoriaCommand.AuditoriaBase.IncluirAuditoriaBaseCommand, AuditoriaDomain.Entidades.AuditoriaBase>();
                    #endregion
                });
                mapper = config.CreateMapper();
            }
            return mapper;
        }
    }
}
