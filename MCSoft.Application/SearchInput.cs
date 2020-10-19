using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application
{
    public class SearchInput : IPagedResultRequest, ISortedResultRequest
    {
        /// <summary>
        /// 排序
        /// </summary>
        public string Sorting { get; set; }

        private int _skipCount = 0;
        /// <summary>
        /// 跳过数量
        /// </summary>
        public int SkipCount
        {
            get => GetSkipCount();
            set => _skipCount = value;
        }

        private int _maxResultCount = 10;
        /// <summary>
        /// 页面大小
        /// </summary>
        public int MaxResultCount
        {
            get => Limit;
            set => _maxResultCount = value;
        }


        #region LayUi Table

        private int Page { get; set; }

        private int Limit { get; set; }

        public string Keyword { get; set; }

        private int GetSkipCount()
        {
            if (Page <= 0)
                throw new UserFriendlyException("页码必须大于0");

            return (Page - 1) * Limit;
        }
        #endregion
    }
}
