﻿// -----------------------------------------------------------------------
//  <copyright file="EventTagger.cs" company="Akka.NET Project">
//      Copyright (C) 2013-2023 .NET Foundation <https://github.com/akkadotnet/akka.net>
//  </copyright>
// -----------------------------------------------------------------------

using Akka.Persistence.Journal;

namespace Akka.Persistence.Sql.Benchmarks
{
    public sealed class EventTagger : IWriteEventAdapter
    {
        public string Manifest(object evt) => string.Empty;

        public object ToJournal(object evt)
        {
            if (evt is not int i)
                return evt;

            return i switch
            {
                <= Const.TagLowerBound => evt,
                <= Const.Tag10UpperBound => new Tagged(evt, new[] { Const.Tag10, Const.Tag100, Const.Tag1000, Const.Tag10000 }),
                <= Const.Tag100UpperBound => new Tagged(evt, new[] { Const.Tag100, Const.Tag1000, Const.Tag10000 }),
                <= Const.Tag1000UpperBound => new Tagged(evt, new[] { Const.Tag1000, Const.Tag10000 }),
                <= Const.Tag10000UpperBound => new Tagged(evt, new[] { Const.Tag10000 }),
                _ => evt
            };
        }
    }
}
