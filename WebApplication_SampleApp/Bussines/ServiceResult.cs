namespace WebApplication_SampleApp.Bussines
{
    public class ServiceResult<T>
    {
        public bool IsError { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
        public void AddError(string ErrorMessage)
        {
            IsError = true;
            Errors.Add(ErrorMessage);

        }
    }
}
