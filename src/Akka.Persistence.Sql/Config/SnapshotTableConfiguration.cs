﻿// -----------------------------------------------------------------------
//  <copyright file="SnapshotTableConfiguration.cs" company="Akka.NET Project">
//      Copyright (C) 2013-2023 .NET Foundation <https://github.com/akkadotnet/akka.net>
//  </copyright>
// -----------------------------------------------------------------------

using System;
using Akka.Configuration;

namespace Akka.Persistence.Sql.Config
{
    public class SnapshotTableConfiguration
    {
        public SnapshotTableConfiguration(Configuration.Config config)
        {
            var mappingPath = config.GetString("table-mapping");
            if (string.IsNullOrEmpty(mappingPath))
                throw new ConfigurationException(
                    "The configuration property akka.persistence.journal.sql.table-mapping is null or empty");

            var mappingConfig = config.GetConfig(mappingPath);
            if (mappingConfig is null)
                throw new ConfigurationException(
                    $"The configuration path akka.persistence.journal.sql.{mappingPath} does not exist");

            if (mappingPath != "default")
                mappingConfig.WithFallback(SqlPersistence.DefaultSnapshotMappingConfiguration);

            SchemaName = mappingConfig.GetString("schema-name");

            SnapshotTable = new SnapshotTableConfig(mappingConfig);
        }

        public SnapshotTableConfig SnapshotTable { get; }

        public string SchemaName { get; }

        public override int GetHashCode()
            => HashCode.Combine(SnapshotTable, SchemaName);
    }
}
