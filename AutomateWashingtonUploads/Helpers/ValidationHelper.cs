using CommonCode.Models;

namespace AutomateWashingtonUploads.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        ILogger _logger;
        public ValidationHelper(ILogger logger)
        {
            _logger = logger;
        }
        public string ChangeSecondToLastCharacter(string s)
        {
            char[] license = s.ToCharArray();
            license[10] = 'O';
            return new string(license);
        }

        public string CheckForZero(string license)
        {
            int index = 10;
            if(license.Length >= index && license[index] == '0')
            {
                var oldLicense = license;
                license = ChangeSecondToLastCharacter(license);
                _logger.LogLicenseChange(oldLicense, license);
                return license;
            }
            return license;
        }

        public static bool IsLicenseTwelveCharacters(string license) => license.Length == 12;
    }
}