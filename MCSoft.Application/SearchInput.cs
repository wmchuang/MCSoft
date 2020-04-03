using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application
{
    public class SearchInput : IPagedResultRequest, ISortedResultRequest
    {
        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; }

        private int skipCount = 0;
        /// <summary>
        /// 跳过数量
        /// </summary>
        public int SkipCount
        {
            get { return (this.Page - 1) * this.Limit; }
            set { this.skipCount = value; }
        }

        private int maxResultCount = 10;
        /// <summary>
        /// 页面大小
        /// </summary>
        public int MaxResultCount
        {
            get { return this.Limit; }
            set { this.maxResultCount = value; }
        }


        #region LayUi Table

        public int Page { get; set; }

        public int Limit { get; set; }

        public string Keyword { get; set; }
        #endregion
    }
}
