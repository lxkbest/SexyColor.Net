using SexyColor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SexyColor.BusinessComponents
{
    public class OperationLogsService
    {
        public IOperationLogsRepository operationLogsRepository { get; set; }

        /// <summary>
        /// 添加操作日志记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OperationLogs AddOperationLogs(OperationLogs entity)
        {
            if (entity == null)
                return null;
            var obj = operationLogsRepository.AddByCache(entity, true);
            return int.TryParse(obj.ToString(), out var result) ? entity : null;
        }

        /// <summary>
        /// 获取操作日志分页
        /// </summary>
        /// <param name="operationLogsQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagingDataSet<OperationLogs> GetPageOperationLogs(OperationLogsQuery operationLogsQuery, int pageIndex, int pageSize)
        {
            return operationLogsRepository.GetPageOperationLogs(operationLogsQuery, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取操作日志单条记录
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public OperationLogs GetOperationLogs(int operationId)
        {
            return operationLogsRepository.GetByCache(w => w.Id == operationId, operationId);
        }
    }
}
