namespace TestProject.ZipPay.Contract.Response
{
    /// <summary>
    /// Generic class used to send response data when client requests for
    /// all valid data available in database table
    /// Response of api request
    /// </summary>
    public class PaginationResponse<T> where T : class, new()
    {
        /// <summary>
        /// Currenr page number
        /// </summary>
        public int PageNum { get; set; }

        /// <summary>
        /// Number of records to display in current page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total count of record
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public List<T> Data { get; set; }
    }
}
