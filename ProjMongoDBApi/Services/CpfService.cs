using ProjMongoDBApi.Services;

namespace ProjMongoDBPassenger.Services
{
    public class CpfService
    {

        //public static bool CheckCpf(string cpf, PassengerService _passengerService)
        //{

        //}
        public static bool CheckCpfDB(string cpf, PassengerService _passengerService)
        {
            if (_passengerService.GetCpf(cpf) != null)
                 { return false; }
            else
                 { return true; }

        }
    }
}
