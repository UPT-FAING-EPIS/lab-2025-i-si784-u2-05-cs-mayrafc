/// <summary>
/// Representa una cuenta bancaria con operaciones de débito y crédito.
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Mensaje usado cuando el monto a debitar excede el saldo.
    /// </summary>
    public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";

    /// <summary>
    /// Mensaje usado cuando el monto a debitar es menor que cero.
    /// </summary>
    public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";

    private readonly string m_customerName;
    private double m_balance;

    private BankAccount() { }

    /// <summary>
    /// Inicializa una nueva instancia de la clase BankAccount.
    /// </summary>
    /// <param name="customerName">Nombre del cliente propietario de la cuenta.</param>
    /// <param name="balance">Saldo inicial de la cuenta.</param>
    /// <exception cref="ArgumentException">Si el nombre del cliente es nulo o vacío.</exception>
    public BankAccount(string customerName, double balance)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name cannot be null or empty.", nameof(customerName));
        m_customerName = customerName;
        m_balance = balance;
    }

    /// <summary>
    /// Obtiene el nombre del cliente propietario de la cuenta.
    /// </summary>
    public string CustomerName { get { return m_customerName; } }

    /// <summary>
    /// Obtiene el saldo actual de la cuenta.
    /// </summary>
    public double Balance { get { return m_balance; } }

    /// <summary>
    /// Debita una cantidad especificada del saldo de la cuenta.
    /// </summary>
    /// <param name="amount">Monto a debitar.</param>
    /// <exception cref="ArgumentOutOfRangeException">Si el monto es menor que cero o mayor que el saldo actual.</exception>
    public void Debit(double amount)
    {
        if (amount > m_balance)
            throw new ArgumentOutOfRangeException(nameof(amount), amount, DebitAmountExceedsBalanceMessage);
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), amount, DebitAmountLessThanZeroMessage);
        m_balance -= amount;
    }

    /// <summary>
    /// Abona una cantidad especificada al saldo de la cuenta.
    /// </summary>
    /// <param name="amount">Monto a abonar.</param>
    /// <exception cref="ArgumentOutOfRangeException">Si el monto es menor que cero.</exception>
    public void Credit(double amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        m_balance += amount;
    }
}
