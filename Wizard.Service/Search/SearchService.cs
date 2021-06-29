using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizard.Data.Repository;
using Wizard.Model.Search;
using Wizard.Model.WebWizard;
using Wizard.Models;

namespace Wizard.Service.Search
{
    public interface ISearchService
    {
        List<ShowWebWizardListModel> GetWebWizardListForSearch();
        List<ShowWebWizardListModel> GetClientsListForSearch();
        List<ShowProjectListForSearchModel> GetProjectsListForSearch();
    }
    public class SearchService : ISearchService
    {
        private ISearchRepository _searchRepository;
        public SearchService(SearchRepository searchRepository)
        {
            this._searchRepository = searchRepository;
        }

        public List<ShowWebWizardListModel> GetClientsListForSearch()
        {
            return _searchRepository.GetClientsListForSearch();
        }

        public List<ShowProjectListForSearchModel> GetProjectsListForSearch()
        {
            return _searchRepository.GetProjectsListForSearch();
        }

        public List<ShowWebWizardListModel> GetWebWizardListForSearch()
        {
            return _searchRepository.GetWebWizardListForSearch();
        }
    }
}
