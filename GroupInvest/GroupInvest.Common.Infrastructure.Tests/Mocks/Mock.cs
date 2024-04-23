using GroupInvest.Microservices.Participantes.Application.Models.Dtos;
using GroupInvest.Microservices.Participantes.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroupInvest.Common.Infrastructure.Tests.Mocks
{
    public static class Mock
    {
        #region Testes Primários
        public static Participante CriarParticipante()
        {
            return new Participante
            {
                Nome = "Daniel Cesar Tostes Santana",
                Email = "daniel.tostes@gmail.com",
                Ativo = true,
                Telefone = "99778-5791"
            };
        }

        public static Periodo CriarPeriodo()
        {
            return new Periodo
            {
                AnoReferencia = 2020,
                DataInicio = new DateTime(2020, 12, 1),
                DataTermino = new DateTime(2020, 11, 30),
                Ativo = true
            };
        }

        public static Adesao CriarAdesao()
        {
            return new Adesao
            {
                Participante = CriarParticipante(),
                Periodo = CriarPeriodo(),
                NumeroCotas = 100
            };
        }
        #endregion

        #region Testes de Negócio
        public static IReadOnlyCollection<ParticipanteDto> CriarParticipantes()
        {
            var participantes = new List<ParticipanteDto>();

            participantes.Add(new ParticipanteDto
            {
                Nome = "Daniel Cesar Tostes Santana",
                Email = "daniel.tostes@gmail.com",
                Telefone = "99778-5791",
                Ativo = true
            });

            participantes.Add(new ParticipanteDto
            {
                Nome = "Michelle Lopes Silva",
                Email = "milopesilva@gmail.com",
                Telefone = "99554-3900",
                Ativo = true
            });

            participantes.Add(new ParticipanteDto
            {
                Nome = "Sheila Malheiros Tostes",
                Email = "sheila.malheiros@yahoo.com.br",
                Telefone = "99467-6423",
                Ativo = true
            });

            participantes.Add(new ParticipanteDto
            {
                Nome = "Leonardo Felipe Tostes Santana",
                Email = "leonardo.tostes2010@gmail.com",
                Telefone = "98171-2186",
                Ativo = true
            });

            return participantes;
        }

        public static PeriodoDto CriarPeriodoDto()
        {
            var periodo = new PeriodoDto
            {
                AnoReferencia = 2020,
                DataInicio = new DateTime(2019, 12, 1),
                DataTermino = new DateTime(2020, 11, 30),
                Ativo = true
            };
            return periodo;
        }
        #endregion
    }
}
