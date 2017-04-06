﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManager.Impl.Domain.Contracts.Commands
{
    [Serializable]
    public class AddStockCommand : ICommand
    {
        public FundId FundId { get; set; }
        public string StockName { get; set; }
        public StockPrice Price { get; set; }
        public int Quantity { get; set; }
    }
}
