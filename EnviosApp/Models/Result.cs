namespace EnviosApp.Models {
    public class Result<T> {
        public T Value { get; set; }
        public bool IsSuccess { get;  }
        public string Error { get;  }

        public Result(T value, bool isSuccess, string error) {
            Value = value;
            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value, true, null);
        public static Result<T> Failure(string error) => new Result<T>(default, false, error);
    }
}
