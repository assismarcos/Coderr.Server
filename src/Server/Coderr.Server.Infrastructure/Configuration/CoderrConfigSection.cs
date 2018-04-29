﻿using System.Collections.Generic;
using Coderr.Server.Abstractions.Config;

namespace Coderr.Server.Infrastructure.Configuration
{
    /// <summary>
    ///     We'll want to track all exceptions for all OTE users so that we can correct bugs in OTE.
    /// </summary>
    public sealed class CoderrConfigSection : IConfigurationSection
    {
        public CoderrConfigSection()
        {
            ActivateTracking = true;
        }

        /// <summary>
        ///     Allow us to track exceptions in OTE.
        /// </summary>
        public bool ActivateTracking { get; set; }

        /// <summary>
        ///     Email address that we may contact if we need any further information (will also receive notifications when the
        ///     errors are corrected).
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        ///     A fixed identity which identifies this specific installation. You can generate a GUID and then store it.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Used to identify the number of installations that have the same issue.
        ///     </para>
        /// </remarks>
        public string InstallationId { get; set; }

        /// <summary>
        /// Estimate of how many unique errors the admin think it has.
        /// </summary>
        public int NumberOfUniqueErrors { get; set; }


        string IConfigurationSection.SectionName
        {
            get { return "ErrorTracking"; }
        }

        IDictionary<string, string> IConfigurationSection.ToDictionary()
        {
            return this.ToConfigDictionary();
        }

        void IConfigurationSection.Load(IDictionary<string, string> settings)
        {
            this.AssignProperties(settings);
        }
    }
}