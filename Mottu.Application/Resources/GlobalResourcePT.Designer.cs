
namespace Desafio.Application.Resources
{
    internal class GlobalMessages
    {
        #region Address

        internal static string Address_AdNumber_CannotEmpty
        {
            get
            {
                return "O número do endereço é obrigatório.";
            }
        }

        internal static string Address_City_CannotEmpty
        {
            get
            {
                return "A cidade do endereço é obrigatória.";
            }
        }

        internal static string Address_NeighborhoodName_CannotEmpty
        {
            get
            {
                return "O bairro do endereço é obrigatório.";
            }
        }

        internal static string Address_State_CannotEmpty
        {
            get
            {
                return "O estado do endereço é obrigatório.";
            }
        }

        internal static string Address_StreetName_CannotEmpty
        {
            get
            {
                return "O logradouro do endereço é obrigatório.";
            }
        }

        internal static string Address_ZipCode_CannotEmpty
        {
            get
            {
                return "O CEP do endereço é obrigatório.";
            }
        }

        #endregion

        #region ProfilePhoto

        internal static string ProfilePhoto_MaximumLenght
        {
            get
            {
                return "A foto não pode exceder 10 MB.";
            }
        }

        #endregion

        #region Other

        internal static string InvalidEmail
        {
            get
            {
                return "E-mail em formato inválido.";
            }
        }

        internal static string CnhInvalid
        {
            get
            {
                return "Tipo de CNH inválida.";
            }
        }

        internal static string CnpjOrCnhAlreadyExist
        {
            get
            {
                return "Cnh ou Cnpj já cadastrados.";
            }
        }

        internal static string InitialDateTimeInvalid
        {
            get
            {
                return "O inicio da locação obrigatóriamente é o primeiro dia após a data de criação.";
            }
        }

        internal static string NotFoundException
        {
            get
            {
                return "Registro não existe.";
            }
        }

        #endregion

        #region PreRegistration

        internal static string PreRegistration_ConfirmToken_Invalid
        {
            get
            {
                return "Código inválido, por favor verifique o último e-mail enviado.";
            }
        }

        internal static string PreRegistration_Email_CannotEmpty
        {
            get
            {
                return "O e-mail não pode estar vazio.";
            }
        }

        internal static string PreRegistration_Password_CannotEmpty
        {
            get
            {
                return "A senha não pode estar vazia.";
            }
        }

        internal static string PreRegistration_Password_CapitalLetter
        {
            get
            {
                return "A senha deve conter pelo menos uma letra maiúscula.";
            }
        }

        internal static string PreRegistration_Password_LeastOneNumber
        {
            get
            {
                return "A senha deve conter pelo menos um número.";
            }
        }

        internal static string PreRegistration_Password_LowerCaseLetter
        {
            get
            {
                return "A senha deve conter pelo menos uma letra minúscula.";
            }
        }

        internal static string PreRegistration_Password_MinimumLenght
        {
            get
            {
                return "A senha deve conter pelo menos 8 letras, caracteres especiais ou números.";
            }
        }

        internal static string PreRegistration_Password_SpecialChar
        {
            get
            {
                return "A senha deve conter pelo menos um caractere especial.";
            }
        }

        internal static string PreRegistration_Token_LimitTime
        {
            get
            {
                return "Espere pelo menos 1 minuto para enviar estes dados.";
            }
        }

        internal static string PreRegistration_EmailExist
        {
            get
            {
                return "E-mail já cadastrado, deseja realizar o login?";
            }
        }

        #endregion

        #region SendMail

        internal static string SendMail_UserEmail_CannotEmpty
        {
            get
            {
                return "O e-mail não pode estar vazio.";
            }
        }

        #endregion

        #region Login

        internal static string Login_CannotAccess
        {
            get
            {
                return "E-mail ou senha incorretos.";
            }
        }

        internal static string Login_WrongPassword
        {
            get
            {
                return "Senha incorreta.";
            }
        }

        #endregion

        #region UpdateEmail

        internal static string UpdateEmail_NewEmail_CannotEmpty
        {
            get
            {
                return "O e-mail não pode estar vazio.";
            }
        }

        internal static string UpdateEmail_NewEmail_MaximumLenght
        {
            get
            {
                return "O e-mail não pode exceder 200 caracteres.";
            }
        }
        #endregion

        internal static string Field_CannotEmpty
        {
            get
            {
                return "O campo não pode estar vazio ou nulo.";
            }
        }

        internal static string Field_Invalid
        {
            get
            {
                return "O campo é inválido";
            }
        }
    }
}
