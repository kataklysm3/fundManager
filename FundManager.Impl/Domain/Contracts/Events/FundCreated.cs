﻿using System;

namespace FundManager.Impl.Domain.Contracts.Events
{
    public class FundCreated : IEvent
    {
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public FundId Id { get; set; }

        public override string ToString()
        {
            return $"Fund '{Name}' was created";
        }
    }
}