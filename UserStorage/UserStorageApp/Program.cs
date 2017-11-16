using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using UserStorageServices;
using UserStorageServices.Enum;
using ServiceConfiguration = ServiceConfigurationSection.ServiceConfigurationSection;

namespace UserStorageApp
{
    // In case of AddressAccessDeniedException run the command below as an administrator:
    //   netsh http add urlacl url=<endpoint> user=<username>
    public class Program
    {
        public static void Main(string[] args)
        {
            // Loading configuration from the application configuration file. This configuration is not used yet.
            var serviceConfiguration = (ServiceConfiguration)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");

            using (var host = new ServiceHost(MyDiagnostics.Create(serviceConfiguration)))
            {
                host.SmartOpen();

                var slaveNode1 = new UserStorageService(new UserIdGenerator(), new UserValidator(), UserStorageServiceMode.SlaveNode);
                var slaveNode2 = new UserStorageService(new UserIdGenerator(), new UserValidator(), UserStorageServiceMode.SlaveNode);
                var services = new List<UserStorageService>() { slaveNode1, slaveNode2 };

                var storage = new UserStorageService(new UserIdGenerator(), new UserValidator(), UserStorageServiceMode.MasterNode, services);
                var storageLog = new UserStorageServiceLog(storage);
                var client = new Client(storageLog);

                client.Run();

                Console.WriteLine("Service \"{0}\" that is implemented by \"{1}\" class is available on \"{2}\" endpoint.", host.Description.Name, host.Description.ServiceType.FullName, host.BaseAddresses.First());
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
