﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.WpfClient.Models
{
  public class AverageModel
  {
    public string BrandName { get; set; }

    public double Average { get; set; }

    public override bool Equals(object obj)
    {
      var other = obj as AverageModel;

      if (other == null)
        return false;

      return other.BrandName == BrandName && other.Average == Average;
    }

    public override string ToString()
    {
      return $"{BrandName} - {Average}";
    }
  }
}