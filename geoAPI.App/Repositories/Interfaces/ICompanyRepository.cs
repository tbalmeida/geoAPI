using geoAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace geoAPI.App.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<CompanyVM> Create(CompanyCreateVM src);

        Task<List<CompanyVM>> GetAll();
    }
}
