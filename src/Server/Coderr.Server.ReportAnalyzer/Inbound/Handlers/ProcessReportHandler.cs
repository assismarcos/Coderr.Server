﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Coderr.Server.Domain.Core.ErrorReports;
using Coderr.Server.ReportAnalyzer.Inbound.Commands;
using DotNetCqs;
using Coderr.Server.Abstractions.Boot;
using log4net;

namespace Coderr.Server.ReportAnalyzer.Inbound.Handlers
{
    public class ProcessReportHandler : IMessageHandler<ProcessReport>
    {
        private readonly Reports.IReportAnalyzer _analyzer;
        private readonly ILog _logger = LogManager.GetLogger(typeof(ProcessReportHandler));

        public ProcessReportHandler(Reports.IReportAnalyzer analyzer)
        {
            _analyzer = analyzer;
        }


        public async Task HandleAsync(IMessageContext context, ProcessReport message)
        {
            try
            {
                ErrorReportException ex = null;
                if (message.Exception != null)
                    ex = ConvertException(message.Exception);
                var contexts = message.ContextCollections.Select(ConvertContext).ToArray();
                var entity = new ErrorReportEntity(message.ApplicationId, message.ReportId, message.CreatedAtUtc, ex,
                    contexts)
                {
                    RemoteAddress = message.RemoteAddress
                };
                await _analyzer.Analyze(context, entity);
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to analyze report ", ex);
            }
        }


        private ErrorReportContextCollection ConvertContext(ProcessReportContextInfoDto arg)
        {
            return new ErrorReportContextCollection(arg.Name, arg.Properties);
        }

        private ErrorReportException ConvertException(ProcessReportExceptionDto dto)
        {
            var entity = new ErrorReportException
            {
                Message = dto.Message,
                FullName = dto.FullName,
                Name = dto.Name,
                AssemblyName = dto.AssemblyName,
                BaseClasses = dto.BaseClasses,
                Everything = dto.Everything,
                Namespace = dto.Namespace,
                StackTrace = dto.StackTrace
            };
            if (dto.InnerExceptionDto != null)
                entity.InnerException = ConvertException(dto.InnerExceptionDto);
            return entity;
        }
    }
}