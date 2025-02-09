using System;

namespace CoreLayer.Utilities
{
    public class OperationResult
    {
        public string Message { get; set; }
        public OperationResultStatus Status { get; set; }
        public bool IsSuccess { get; set; }

        // ساخت یک متد عمومی برای ایجاد نتیجه
        private static OperationResult CreateResult(OperationResultStatus status, string message)
        {
            return new OperationResult
            {
                Status = status,
                Message = message
            };
        }

        #region Errors
        public static OperationResult Error()
        {
            return CreateResult(OperationResultStatus.Error, "عملیات ناموفق");
        }

        public static OperationResult Error(string message)
        {
            return CreateResult(OperationResultStatus.Error, message);
        }
        #endregion

        #region NotFound
        public static OperationResult NotFound()
        {
            return CreateResult(OperationResultStatus.NotFound, "اطلاعات درخواستی یافت نشد");
        }

        public static OperationResult NotFound(string message)
        {
            return CreateResult(OperationResultStatus.NotFound, message);
        }
        #endregion

        #region Success
        public static OperationResult Success()
        {
            return CreateResult(OperationResultStatus.Success, "عملیات با موفقیت انجام شد");
        }

        public static OperationResult Success(string message)
        {
            return CreateResult(OperationResultStatus.Success, message);
        }
        #endregion
    }

    public enum OperationResultStatus
    {
        Error = 10,
        Success = 200,
        NotFound = 404
    }
}
