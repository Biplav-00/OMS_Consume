namespace ConsumeOMS.Models
{
    public class ApiErrors
    {
        private Dictionary<int, string> _apiErrors = new Dictionary<int, string>
        {
            { 200, "Success" },
            { 204, "No Content - Cannot accept empyt inputs. " },
            { 400, "Bad Request - The request was malformed or invalid." },
            { 401, "Unauthorized - Authentication credentials were missing or invalid." },
            { 403, "Forbidden - The user is not authorized to access the requested resource." },
            { 404, "Not Found - The requested resource could not be found." },
            { 405, "Method Not Allowed - The requested HTTP method is not allowed for the endpoint." },
            { 408, "Request Timeout - The request timed out before a response was received." },
            { 409, "Conflict - The requested operation conflicts with the current state of the resource." },
            { 422, "Validation Failed - The input data failed validation rules." },
            { 500, "Internal Server Error - An internal server error occurred." },
            { 503, "Service Unavailable - The server is currently unable to handle the request." },
            { 600, "Database Error - An error occurred while interacting with the PostgreSQL database." },
            { 601, "Concurrency Failure - A concurrency conflict occurred when updating the resource." },
            { 602, "External Service Error - An error occurred when interacting with an external service." }
        };

        public string GetStatusCodeMessage(int statusCode)
        {
            return _apiErrors[statusCode];
        }
    }
}
