﻿using System;
using Coderr.Server.Abstractions.Boot;

namespace Coderr.Server.ReportAnalyzer.Tagging.Identifiers
{
    /// <summary>
    ///     Identify Razor View Engine.
    /// </summary>
    [ContainerService]
    internal class RazorIdentifier : ITagIdentifier
    {
        /// <summary>
        ///     Check if the wanted tag is supported.
        /// </summary>
        /// <param name="context">Error context providing information to search through</param>
        public void Identify(TagIdentifierContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var propertyValue = context.GetPropertyValue("ExceptionProperties", "Message");
            if (propertyValue != null && propertyValue.Contains(".cshtml"))
                context.AddTag("razor", 0);
        }
    }
}