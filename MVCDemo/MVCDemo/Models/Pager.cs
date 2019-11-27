using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDemo.Models {
    /// <summary>
    /// Simple Object for Pagination
    /// </summary>
    public class Pager {
        public int Start;
        public int CountOfItems;
        public int CurrentPageNumber;
        public int CountPerPage;
        
        public Pager(int? page, int? count, int totalCount) {
            // default to first page and 3 items per page.
            if (page == null) page = 1;
            if (count == null) count = 3;

            CountPerPage = (int)count;
            Start = ((int)page - 1) * CountPerPage;
            CurrentPageNumber = (int)page;    
            CountOfItems = totalCount;
        }

        public int CountOfPages {
            get {
                int pagesCount = CountOfItems / CountPerPage;
                if(CountOfItems % CountPerPage > 0) {
                    pagesCount++;
                }
                return pagesCount;
            }
        }
    }
}
