using AvoidContactCommon.Sign;
using System.Text.RegularExpressions;

namespace AvoidContactCommon.Validation
{
    public class CommonSignValidator
    {
        private readonly Regex DescriptionRegex = new(@"^.{0,495}$");
        private readonly Regex CallSignRegex = new(@"^([A-Za-zÀ-ßà-ÿ ]{2,30})$");
        private readonly Regex LoginRegex = new(@"^(?!.*[-_.]{2})([A-Za-z])([A-Za-z0-9-_.]{2,13})([A-Za-z0-9])$");
        private readonly Regex PasswordRegex = new(@"^(?=(.*[A-ZÀ-ß]){1,})(?=(.*[a-zà-ÿ]){1,})(?=(.*[0-9]){1,})(?=(.*[!@#$%^&*()_+,.\\\/;':""-]){1,}).{8,20}$");
        private readonly Regex EmailRegex = new(@"^([a-zà-ÿA-ZÀ-ß0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        public SignInResult CheckSignIn(string login, string password)
        {
            if (ValidateLogin(login) && ValidatePassword(password))
            {
                return SignInResult.Success;
            }
            return SignInResult.NotValidLoginOrPassword;
        }

        public bool CheckCommonInfo(PlayerInfo commonPlayerInfo)
        {
            return ValidateCallSign(commonPlayerInfo.CallSign) && ValidateDescription(commonPlayerInfo.PlayerDiscription);
        }

        public SignUpResult CheckSignUp(SignInfo signedPlayerInfo)
        {
            bool isLoginValid = ValidateLogin(signedPlayerInfo.Login);
            bool isPasswordValid = ValidatePassword(signedPlayerInfo.Password);
            bool isEmailValid = ValidateEmail(signedPlayerInfo.Email);

            var result = SignUpResult.Success;

            if (!isLoginValid && !isPasswordValid && !isEmailValid)
            {
                result = SignUpResult.NotValidLoginAndPasswordAndEmail;
            }

            if (!isLoginValid && !isPasswordValid)
            {
                result = SignUpResult.NotValidLoginAndPassword;
            }

            if (!isLoginValid && !isEmailValid)
            {
                result = SignUpResult.NotValidLoginAndEmail;
            }

            if (!isPasswordValid && !isEmailValid)
            {
                result = SignUpResult.NotValidEmailAndPassword;
            }

            if (!isLoginValid)
            {
                result = SignUpResult.NotValidLogin;
            }

            if (!isPasswordValid)
            {
                result = SignUpResult.NotValidPassword;
            }

            if (!isEmailValid)
            {
                result = SignUpResult.NotValidEmail;
            }

            if (!ValidateCallSign(signedPlayerInfo.CallSign))
            {
                result = SignUpResult.NotValidCallSign;
            }

            if (!ValidateDescription(signedPlayerInfo.PlayerDiscription))
            {
                result = SignUpResult.NotValidDescription;
            }

            return result;
        }

        private bool ValidateLogin(string login)
        {
            return !string.IsNullOrWhiteSpace(login) && LoginRegex.IsMatch(login);
        }

        private bool ValidatePassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && PasswordRegex.IsMatch(password);
        }

        private bool ValidateEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email);
        }

        private bool ValidateCallSign(string callSign)
        {
            return !string.IsNullOrWhiteSpace(callSign) && CallSignRegex.IsMatch(callSign);
        }

        private bool ValidateDescription(string description)
        {
            return DescriptionRegex.IsMatch(description);
        }
    }
}