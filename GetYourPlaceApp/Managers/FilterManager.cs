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
        public static List<GYPPropertyInfoItem> Filters = new List<GYPPropertyInfoItem>();
        public event EventHandler<List<GYPPropertyInfoItem>> FilterUpdated;
        public string CurrentFilterSelected;
        public string CustomFilter;
        public event EventHandler<string> FilterChangeOrder;
        public event EventHandler<string> FilterSearchText;

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

        public void ApplyFilters(List<GYPPropertyInfoItem> filters)
        {
            Filters = filters;
            FilterUpdated?.Invoke(null, filters);
        }

        public void RemoveFilter(GYPPropertyInfoItem filter, bool InvokeMethod = false)
        {
            var filterFound = Filters?.FirstOrDefault(f => 
            f.Id == filter?.Id && f.GYPPropertyInfo == filter?.GYPPropertyInfo);

            if(filterFound != null)
            {
                Filters.Remove(filterFound);
                if(InvokeMethod)
                {
                    FilterUpdated?.Invoke(null, Filters);
                }
            }
        }

        public void RemoveFilters(List<GYPPropertyInfoItem> filters)
        {
            if(filters.Count > 0)
            {
                foreach(var filter in filters)
                {
                    RemoveFilter(filter);
                }
            }
            else
                Filters.Clear();

            FilterUpdated?.Invoke(null, Filters);
        }

        public List<GYPPropertyInfoItem> GetFilters()
        {
            return Filters;
        }

        public void RaiseChangeOrderEvent(string filter)
        {
            CurrentFilterSelected = filter;
            FilterChangeOrder?.Invoke(null, filter);
        }

        public void RaiseSearchTextFilter(string filter)
        {
            FilterSearchText?.Invoke(null, filter);
        }
    }
}
