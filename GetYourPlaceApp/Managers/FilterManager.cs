using GetYourPlaceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetYourPlaceApp.Managers
{
    public sealed class FilterManager
    {
        #region Variables
        public static List<GYPFilterItem> Filters;
        public event EventHandler<List<GYPFilterItem>> FilterUpdated;
        private static FilterManager instance = null;
        #endregion

        public static FilterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FilterManager();
                }
                return instance;
            }
        }
        private FilterManager()
        {
                
        }

        public void ApplyFilters(List<GYPFilterItem> filters)
        {
            Filters = filters;
            FilterUpdated?.Invoke(null, filters);
        }

        public void RemoveFilter(GYPFilterItem filter, bool InvokeMethod = false)
        {
            var filterFound = Filters?.FirstOrDefault(f => 
            f.Id == filter?.Id && f.GYPFilterType == filter?.GYPFilterType);

            if(filterFound != null)
            {
                Filters.Remove(filterFound);
                if(InvokeMethod)
                {
                    FilterUpdated?.Invoke(null, Filters);
                }
            }
        }

        public void RemoveFilters(List<GYPFilterItem> filters)
        {
            if(filters.Count > 0)
            {
                foreach(var filter in filters)
                {
                    RemoveFilter(filter);
                }

                FilterUpdated?.Invoke(null, Filters);
            }
        }

        public List<GYPFilterItem> GetFilters()
        {
            return Filters;
        }
    }
}
