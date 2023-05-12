using Reso.Core.Custom;

namespace Boek.Service.Commons
{
    public class BoekErrorMessage
    {
        public static void ShowErrorMessage(int errorCode, string[] errorMessages)
        {
            var errorMessage = "";
            foreach (var error in errorMessages)
            {
                errorMessage += error + " ";
            }
            throw new ErrorResponse(errorCode, errorMessage.Trim());
        }
    }
}
