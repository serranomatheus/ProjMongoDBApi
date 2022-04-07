namespace ProjMongoDBUser.Services
{
    public class CpfService
    {
        public static bool CheckCpfDB(string cpf, UserService _passengerService)
        {
            if (_passengerService.GetCpf(cpf) != null)
            { return false; }
            else
            { return true; }
        }
    }
}
