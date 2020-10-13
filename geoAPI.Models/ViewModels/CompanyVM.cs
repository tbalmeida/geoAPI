using geoAPI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace geoAPI.Models.ViewModels
{
  public class CompanyVM
  {
    public CompanyVM(Company src)
    {
            Id = src.Id;
            Name = src.Name;
    }
    public Guid Id { get; set; }

    public String Name { get; set; }

  }
}
