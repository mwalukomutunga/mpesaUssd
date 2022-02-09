using BimaPimaUssd.Contracts;
using BimaPimaUssd.Helpers;
using BimaPimaUssd.Models;
using BimaPimaUssd.Repository;
using System;
using System.Collections.Generic;

namespace BimaPimaUssd.ViewModels
{
    public class FTMAModel
    {

        private readonly ServerResponse serverResponse;
        private readonly IRepository repository;
        private readonly IMessager messager;
        private readonly Stack<int> levels;
        private readonly Repository<User> _repository;

        public string _Name { get; }
        public FTMAModel(ServerResponse serverResponse, IRepository repository, IMessager messager, IStoreDatabaseSettings settings)
        {
            _Name = serverResponse.mobileNumber;
            this.serverResponse = serverResponse;
            this.repository = repository;
            this.messager = messager;
            levels = repository.levels[serverResponse.session_id];
            _repository =new  Repository<User>(settings, "Farmer"); 
        }
        public string MainMenu => levels.Pop() switch
        {
            0 => PlantingMonth,
            1 => PlantingMonth1,
            2 => PlantingWeek,
            3 => NearBy,
            4 => SendDetails(),
            _ => IFVM.Invalid
        };
        private string SendDetails()
        {  messager.SendMessage("Jambo! Thank you for enrolling with Digifarm. Please dial *800*90# to register location for monitoring of your crops", serverResponse.mobileNumber);
           var user = new User
            {
                Msisdn = serverResponse.mobileNumber,
                Longitude = repository.Data[serverResponse.session_id].longitude.ToString(),
                Latitude = repository.Data[serverResponse.session_id].latitude.ToString(),
                PlantingMonth = GetMonth(Convert.ToInt32(repository.Data[serverResponse.session_id].Month)),
                PlantingWeek = GetWeek(Convert.ToInt32(repository.Data[serverResponse.session_id].Week)),
                NearestPrimarySchool = repository.Requests[serverResponse.session_id].Last.Value.ToString(),
                DateOfQuery = DateTime.Now
            };
           _repository.InsertRecord(user);
            return IFVM.ThankFarmer();
        }
        private string GetWeek(int input)
        {
            return input switch
            {
                1 => "1st Week",
                2 => "2nd Week",
                3 => "3rd Week",
                4 => "4th Week",
                5 => "5th Week",
                _ => "Invalid",
            };
        }
        private string GetMonth(int input)
        {
            return input switch
            {
                1 => "August",
                2 => "September",
                3 => "Octomber",
                4 => "November",
                5 => "December",
                _ => "Invalid",
            };
        }
        private string NearBy
        {
            get
            {
                levels.Push(4);
                var values = new HashSet<int>() { 1, 2, 3, 4, 5 };
                if(!values.Contains(Convert.ToInt32(repository.Requests[serverResponse.session_id].Last.Value)))
                {
                    levels.Pop();
                    return IFVM.Invalid;
                }
                repository.Data[serverResponse.session_id].Week = repository.Requests[serverResponse.session_id].Last.Value;
                return IFVM.NearBy();
            }
        }
        private string PlantingWeek
        {
            get
            {
                var values = new HashSet<int>() { 1, 2, 3, 4, 5 };
                levels.Push(3);
                if (!values.Contains(Convert.ToInt32(repository.Requests[serverResponse.session_id].Last.Value)))
                {
                    levels.Pop();
                    return IFVM.Invalid;
                }
                repository.Data[serverResponse.session_id].Month = Convert.ToInt32(repository.Requests[serverResponse.session_id].Last.Value);
                return IFVM.PlantingWeek();
            }
        }
        private string PlantingMonth
        {
            get
            {
                levels.Push(1);
                return IFVM.PlantingMonth();
            }
        }
        private string PlantingMonth1
        {
            get
            {
                levels.Push(2);
                return IFVM.PlantingMonth();
            }
        }

        private string Name
        {
            get
            {
                levels.Push(1);
                return IFVM.FarmMSg();
            }
        }


    }
}
