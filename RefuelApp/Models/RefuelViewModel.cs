﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefuelApp.Models
{
    public class RefuelViewModel
    {
        public System.DateTime DateOf { get; set; }
        public decimal TotalFuelCostInEuros { get; set; }
        public Nullable<int> TotalDistanceTraveled { get; set; }
    }
}