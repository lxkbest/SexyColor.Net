using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace SexyColor.Infrastructure
{
    public class PagingDataSet<T> : ReadOnlyCollection<T>, IPagingDataSet
    {
        private int _pageIndex;
        private int _pageSize;
        private long _totalRecords;
        private double queryDuration;

        public PagingDataSet(IEnumerable<T> entities) : base(entities.ToList<T>())
        {
            this._pageSize = 20;
            this._pageIndex = 1;
        }

        public PagingDataSet(IList<T> entities) : base(entities)
        {
            this._pageSize = 20;
            this._pageIndex = 1;
        }

        public int PageCount
        {
            get
            {
                long num = this.TotalRecords / ((long)this.PageSize);
                if ((this.TotalRecords % ((long)this.PageSize)) != 0L)
                {
                    num += 1L;
                }
                return Convert.ToInt32(num);
            }
        }

        public int PageIndex
        {
            get
            {
                return this._pageIndex;
            }
            set
            {
                this._pageIndex = value;
            }
        }

        public int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                this._pageSize = value;
            }
        }

        public double QueryDuration
        {
            get
            {
                return this.queryDuration;
            }
            set
            {
                this.queryDuration = value;
            }
        }

        public long TotalRecords
        {
            get
            {
                return this._totalRecords;
            }
            set
            {
                this._totalRecords = value;
            }
        }
    }
}
