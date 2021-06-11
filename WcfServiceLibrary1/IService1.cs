using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<Employee> GetAllData();

        [OperationContract]
        Employee GetDataUsingId(string email_id);

        [OperationContract]
        List<Employee> GetDataUsingType(string type);

        [OperationContract]
        Boolean AddEmployee(Employee e);

        [OperationContract]
        Boolean EditEmployee(Employee e);

        [OperationContract]
        Boolean DeleteEmployee(string email_id);

        [OperationContract]
        double GetSalary(string email_id);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceLibrary1.ContractType".
    [DataContract]
    public class Employee
    {
        [DataMember]
        public string Email_id
        {
            get;
            set;
        }

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public DateTime DOB
        {
            get;
            set;
        }

        [DataMember]
        public string Gender
        {
            get;
            set;
        }

        [DataMember]
        public double Salary
        {
            get;
            set;
        }

        [DataMember]
        public string Type
        {
            get;
            set;
        }

        [DataMember]
        public char[] Present
        {
            get;
            set;
        }
    }
}
