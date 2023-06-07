using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagementApp.Models;

namespace UserManagementApp.Helpers
{
    public static class Sorting
    {
        public static IQueryable<UserModel> SortUsersData(IQueryable<UserModel> data, string sortParameter, string sortOrder)
        {
            switch (sortOrder)
            {
                case "asc":
                    {
                        switch (sortParameter.ToLower())
                        {
                            case "firstname":
                                data = data.OrderBy(x => x.FirstName);
                                break;
                            case "lastname":
                                data = data.OrderBy(x => x.LastName);
                                break;
                            case "username":
                                data = data.OrderBy(x => x.Username);
                                break;
                            case "password":
                                data = data.OrderBy(x => x.Password);
                                break;
                            case "email":
                                data = data.OrderBy(x => x.Email);
                                break;
                            default:
                                data = data.OrderBy(x => x.ID);
                                break;
                        }
                    }
                    break;
                case "desc":
                    {
                        switch (sortParameter.ToLower())
                        {
                            case "firstname":
                                data = data.OrderByDescending(x => x.FirstName);
                                break;
                            case "lastname":
                                data = data.OrderByDescending(x => x.LastName);
                                break;
                            case "username":
                                data = data.OrderByDescending(x => x.Username);
                                break;
                            case "password":
                                data = data.OrderByDescending(x => x.Password);
                                break;
                            case "email":
                                data = data.OrderByDescending(x => x.Email);
                                break;
                            default:
                                data = data.OrderBy(x => x.ID);
                                break;
                        }
                    }
                    break;
                default:
                    data = data.OrderBy(x => x.ID);
                    break;

                   
            }

            return data;
        }
    }
}