namespace WlChallenge.Domain.Errors;

public static class ErrorMessages
{
    #region Properties

    public static AccountErrorMessages Account { get; set; } = new();
    public static EmailErrorMessages Email { get; set; } = new();
    public static PasswordErrorMessages Password { get; set; } = new();
    public static VerificationCodeErrorMessages VerificationCode { get; set; } = new();
    public static DocumentErrorMessages Document { get; set; } = new();
    public static CpfErrorMessages Cpf { get; set; } = new();
    public static CnpjErrorMessages Cnpj { get; set; } = new();
    public static UserErrorMessages User { get; set; } = new();
    public static WalletErrorMessages Wallet { get; set; } = new();

    public static TransactionMessages Transaction { get; set; } = new();

    #endregion

    #region Error Messages

    public class AccountErrorMessages
    {
        public string EmailInUse { get; set; } = "Este E-mail já está sendo utilizado.";
        public string EmailDenied { get; set; } = "Este E-mail está bloqueado no sistema.";
        public string DomainDenied { get; set; } = "Este domínio está bloqueado no sistema.";
        public string NotFound { get; set; } = "Conta não encontrada.";
        public string PasswordIsInvalid { get; set; } = "Usuário ou senha inválidos.";
        public string IsInactive { get; set; } = "Esta conta ainda não foi ativada.";
        public string IsAlreadyActive { get; set; } = "Esta conta já foi ativada.";
        public string IsLockedOut { get; set; } = "Esta conta está bloqueada.";
        public string EmailIsDifferent { get; set; } = "O E-mail informado é diferente do E-mail da conta.";
        public string DocumentIsDifferent { get; set; } = "O document informado é diferente do documento da conta.";
    }

    public class EmailErrorMessages
    {
        public string Invalid { get; set; } = "E-mail inválido";
        public string NullOrEmpty { get; set; } = "E-mail não pode ser nulo ou vazio.";
    }

    public class PasswordErrorMessages
    {
        public string Invalid { get; set; } = "A senha informada é inválida.";
        public string InvalidLength { get; set; } = "A senha deve conter pelo menos 12 caracteres.";
        public string PasswordNull { get; set; } = "A senha não pode ser nula.";
    }

    public class VerificationCodeErrorMessages
    {
        public string InvalidCode { get; set; } = "Código de verificação inválido.";
        public string NullOrEmpty { get; set; } = "Nenhum código de verificação foi informado.";
        public string NullOrWhiteSpace { get; set; } = "O código de verificação informado está vazio.";
        public string InvalidLenght { get; set; } = "O código de verificação deve conter 6 caracteres.";
        public string Expired { get; set; } = "Este código de verificação expirou.";
        public string AlreadyActive { get; set; } = "Este código de verificação já está ativo.";
    }

    public class TransactionMessages
    {
        public string InvalidAmount { get; set; } = "Valor de transferência não pode ser 0";
        public string InvalidSenderId { get; set; } = "Id do remetente inválido";
        public string InvalidReceiverId { get; set; } = "Id do destinátario inválido";
    }

    public class DocumentErrorMessages
    {
        public string Invalid { get; set; } = "O documento informado é inválido.";
        public string Null { get; set; } = "Nenhum documento foi informado.";
    }

    public class CpfErrorMessages
    {
        public string Invalid { get; set; } = "O CPF informado é inválido.";
        public string InvalidLength { get; set; } = "O CPF deve conter 11 dígitos.";
        public string InvalidNumber { get; set; } = "O número do CPF informado é inválido.";
    }

    public class CnpjErrorMessages
    {
        public string Invalid { get; set; } = "O CNPJ informado é inválido.";
        public string InvalidLength { get; set; } = "O CNPJ deve conter 14 dígitos.";
        public string InvalidNumber { get; set; } = "O número do CNPJ informado é inválido.";
    }

    public class UserErrorMessages
    {
        public string InvalidNullOrEmpty { get; set; } = "O Nome informado não pode ser vazio ou nulo.";
        public string InvalidLength { get; set; } = "O Nome deve conter pelo menos 3 e no máximo 40 caracteres.";
    }

    public class WalletErrorMessages
    {
        public string NoBalance { get; set; } = "Não tem saldo suficiente.";
        public string InvalidSenderId { get; set; } = "Id do remetente inválido";

        public string InvalidReceiverId { get; set; } = "Id do destinátario inválido";
    }

    #endregion
}