using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EuroBuld.Assets
{
	public class UserData
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public bool IsEmployee { get; set; }
	}


	public class UserSession
	{

		private static UserSession _instance;
		public static UserSession Instance => _instance ?? (_instance = new UserSession());

		public UserData CurrentUser { get; private set; }

		private UserSession() { }

		public void SetUserData(int userId, string firstName, string lastName, bool isEmployee, string email, string adress, string phoneNumber)
		{
			CurrentUser = new UserData
			{
				Id = userId,
				FirstName = firstName,
				LastName = lastName,
				IsEmployee = isEmployee,
				Address = adress,
				PhoneNumber = phoneNumber,
				Email = email
			};
		}

		public void ClearSession()
		{
			CurrentUser = null;
		}

		public bool IsLoggedIn => CurrentUser != null;
	}
}
