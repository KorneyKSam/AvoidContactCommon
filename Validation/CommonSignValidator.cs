using AvoidContactCommon.Sign;
using System.Text.RegularExpressions;

namespace AvoidContactCommon.Validation
{
    public class CommonSignValidator
    {
        private readonly Regex LoginRegex = new(@"^(?!.*[-_.]{2})([A-Za-z]).([A-Za-z0-9-_.]{2,18})([A-Za-z0-9])$");
        private readonly Regex PasswordRegex = new(@"^(?=(.*[A-Z]){1,})(?=(.*[a-z]){1,})(?=(.*[0-9]){3,})(?=(.*[!@#$%^&*()_+,.\\\/;':""-]){3,}).{8,20}$");
        private readonly Regex EmailRegex = new(@"^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        public SignInResult CheckSignIn(string login, string password)
        {
            if (ValidateLogin(login) && ValidatePassword(password))
            {
                return SignInResult.Success;
            }
            return SignInResult.NotValidLoginOrPassword;
        }

        public SignUpResult CheckSignUp(SignedPlayerInfo signedPlayerInfo)
        {
            bool isLoginValid = ValidateLogin(signedPlayerInfo.Login);
            bool isPasswordValid = ValidatePassword(signedPlayerInfo.Password);
            bool isEmailValid = ValidateEmail(signedPlayerInfo.Email);

            if (!isLoginValid && !isPasswordValid && !isEmailValid)
            {
                return SignUpResult.NotValidLoginAndPasswordAndEmail;
            }

            if (!isLoginValid && !isPasswordValid)
            {
                return SignUpResult.NotValidLoginAndPassword;
            }

            if (!isLoginValid && !isEmailValid)
            {
                return SignUpResult.NotValidLoginAndEmail;
            }

            if (!isPasswordValid && !isEmailValid)
            {
                return SignUpResult.NotValidEmailAndPassword;
            }

            if (!isLoginValid)
            {
                return SignUpResult.NotValidLogin;
            }

            if (!isPasswordValid)
            {
                return SignUpResult.NotValidPassword;
            }

            if (!isEmailValid)
            {
                return SignUpResult.NotValidEmail;
            }

            return SignUpResult.Success;
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
    }
}