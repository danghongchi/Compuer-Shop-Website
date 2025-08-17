namespace ComputerShop.Areas.Admin.Repository
{
    public interface IAppEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
