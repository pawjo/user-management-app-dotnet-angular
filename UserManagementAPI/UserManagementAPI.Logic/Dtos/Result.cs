namespace UserManagementAPI.Logic.Dtos
{
    public class Result
    {
        public Result()
        { }

        public Result(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsError { get => ErrorCode != 0; }
    }

    public class Result<T> : Result
    {
        public Result(T response) : base()
        {
            Response = response;
        }

        public Result(int errorCode, string errorMessage) : base(errorCode, errorMessage)
        { }

        public T Response { get; set; }
    }
}
